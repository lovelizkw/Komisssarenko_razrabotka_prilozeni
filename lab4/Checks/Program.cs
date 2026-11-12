using System.Text.Json;
using RaceData;

namespace Checks
{
    internal static class Program
    {
        private static int passed;
        private static int failed;
        private static string dataFolder = "";

        static void Main()
        {
            dataFolder = FindDataFolder();

            Check("Источник Шесть заездов даёт шесть записей", SixSource);
            Check("Источник Заезды даёт 120 записей с Id 1-120", BigSource);
            Check("Прогресс растёт и завершается полным количеством", ProgressGrows);
            Check("Учебная задержка не меняет результат", SlowMode);
            Check("Отмена до старта даёт OperationCanceledException", CancelBeforeStart);
            Check("Отмена в середине прерывает чтение", CancelMidway);
            Check("Пустой журнал даёт пустой список", EmptySource);
            Check("Нечитаемая база даёт ошибку", BrokenSource);
            Check("Отсутствующий файл даёт отказ", MissingSource);
            Check("Фильтр Спринт показывает 40 из 120", FilterSprint);
            Check("Экспорт шести записей создаёт копию", ExportSix);
            Check("Экспорт при фильтре сохраняет весь источник", ExportUnderFilter);
            Check("Экспорт пустого журнала это успех", ExportEmpty);
            Check("Экспорт в путь источника отклоняется", ExportSamePath);
            Check("Экспорт в папку отклоняется", ExportToFolder);
            Check("Отмена экспорта сохраняет прежнюю копию", ExportCancelKeepsOld);
            Check("Нечитаемая база не перезаписывает копию", ExportBrokenKeepsOld);
            Check("Повторная загрузка после отмены успешна", ReloadAfterCancel);

            Console.WriteLine();
            Console.WriteLine($"Пройдено: {passed}. Отказано: {failed}");
            Environment.ExitCode = failed == 0 ? 0 : 1;
        }

        private static string FindDataFolder()
        {
            var dir = new DirectoryInfo(AppContext.BaseDirectory);
            while (dir != null)
            {
                string candidate = Path.Combine(dir.FullName, "Данные");
                if (Directory.Exists(candidate))
                    return candidate;
                dir = dir.Parent;
            }
            throw new InvalidOperationException("Папка Данные не найдена");
        }

        private static void Check(string name, Action action)
        {
            try
            {
                action();
                passed++;
                Console.WriteLine("[OK] " + name);
            }
            catch (Exception ex)
            {
                failed++;
                Console.WriteLine("[FAIL] " + name + " -> " + ex.Message);
            }
        }

        private static void Expect(bool condition, string message)
        {
            if (!condition)
                throw new Exception(message);
        }

        private class CollectProgress : IProgress<WorkProgress>
        {
            public List<WorkProgress> Reports { get; } = new List<WorkProgress>();

            public void Report(WorkProgress value)
            {
                Reports.Add(value);
            }
        }

        private class CancelAfterCount : IProgress<WorkProgress>
        {
            private readonly CancellationTokenSource cts;
            private readonly long trigger;

            public CancelAfterCount(CancellationTokenSource cts, long trigger)
            {
                this.cts = cts;
                this.trigger = trigger;
            }

            public List<WorkProgress> Reports { get; } = new List<WorkProgress>();

            public void Report(WorkProgress value)
            {
                Reports.Add(value);
                if (value.Done >= trigger)
                    cts.Cancel();
            }
        }

        private static string BigSourcePath => Path.Combine(dataFolder, "Заезды.db");
        private static string SixSourcePath => Path.Combine(dataFolder, "Шесть заездов.db");
        private static string EmptySourcePath => Path.Combine(dataFolder, "Пустой журнал.db");
        private static string BrokenSourcePath => Path.Combine(dataFolder, "Нечитаемая база.db");

        private static async Task<List<RaceResult>> LoadAsyncChecked(string path, bool slowMode,
            IProgress<WorkProgress>? progress, CancellationToken token)
        {
            var transfer = new RaceTransfer();
            return await transfer.LoadAsync(path, slowMode, progress, token);
        }

        private static void SixSource()
        {
            var rows = LoadAsyncChecked(SixSourcePath, false, null, CancellationToken.None)
                .GetAwaiter().GetResult();
            Expect(rows.Count == 6, "записей не 6");
            Expect(rows[0].Driver == "Анна" && rows[0].LapSeconds == 94, "первая запись не Анна 94");
            Expect(rows[0].Id == 1, "первый Id не 1");
        }

        private static void BigSource()
        {
            var rows = LoadAsyncChecked(BigSourcePath, false, null, CancellationToken.None)
                .GetAwaiter().GetResult();
            Expect(rows.Count == 120, "записей не 120");
            Expect(rows[0].Id == 1 && rows[^1].Id == 120, "Id идут не с 1 по 120");
            for (int i = 0; i < rows.Count; i++)
                Expect(rows[i].Id == i + 1, "нарушен порядок Id");
        }

        private static void ProgressGrows()
        {
            var progress = new CollectProgress();
            var rows = LoadAsyncChecked(BigSourcePath, false, progress, CancellationToken.None)
                .GetAwaiter().GetResult();
            Expect(rows.Count == 120, "записей не 120");
            Expect(progress.Reports.Count > 0, "нет сообщений прогресса");
            for (int i = 1; i < progress.Reports.Count; i++)
                Expect(progress.Reports[i].Done >= progress.Reports[i - 1].Done, "прогресс убывает");
            var last = progress.Reports[^1];
            Expect(last.Done == 120 && last.Total == 120, "последнее сообщение не 120 из 120");
        }

        private static void SlowMode()
        {
            var rows = LoadAsyncChecked(BigSourcePath, true, null, CancellationToken.None)
                .GetAwaiter().GetResult();
            Expect(rows.Count == 120, "записей не 120");
        }

        private static void CancelBeforeStart()
        {
            using var cts = new CancellationTokenSource();
            cts.Cancel();
            bool cancelled = false;
            try
            {
                LoadAsyncChecked(BigSourcePath, false, null, cts.Token).GetAwaiter().GetResult();
            }
            catch (OperationCanceledException)
            {
                cancelled = true;
            }
            Expect(cancelled, "исключение отмены не получено");
        }

        private static void CancelMidway()
        {
            using var cts = new CancellationTokenSource();
            var progress = new CancelAfterCount(cts, 5);
            bool cancelled = false;
            try
            {
                LoadAsyncChecked(BigSourcePath, true, progress, cts.Token).GetAwaiter().GetResult();
            }
            catch (OperationCanceledException)
            {
                cancelled = true;
            }
            Expect(cancelled, "операция не была отменена");
            Expect(progress.Reports.Count >= 5, "чтение не дошло до отмены");
            Expect(progress.Reports.Count < 120, "чтение не было прервано");
        }

        private static void EmptySource()
        {
            var progress = new CollectProgress();
            var rows = LoadAsyncChecked(EmptySourcePath, false, progress, CancellationToken.None)
                .GetAwaiter().GetResult();
            Expect(rows.Count == 0, "список не пуст");
            Expect(progress.Reports[^1].Total == 0, "Total не 0");
        }

        private static void BrokenSource()
        {
            bool thrown = false;
            try
            {
                LoadAsyncChecked(BrokenSourcePath, false, null, CancellationToken.None)
                    .GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                thrown = ex is not OperationCanceledException;
            }
            Expect(thrown, "ошибки чтения нет");
        }

        private static void MissingSource()
        {
            bool thrown = false;
            try
            {
                LoadAsyncChecked(Path.Combine(dataFolder, "Нет такой базы.db"), false, null,
                    CancellationToken.None).GetAwaiter().GetResult();
            }
            catch (FileNotFoundException)
            {
                thrown = true;
            }
            Expect(thrown, "исключения о файле нет");
        }

        private static void FilterSprint()
        {
            var rows = LoadAsyncChecked(BigSourcePath, false, null, CancellationToken.None)
                .GetAwaiter().GetResult();
            var sprint = RaceQuery.Apply(rows, "Спринт");
            Expect(sprint.Count == 40, "спринта не 40");
            var all = RaceQuery.Apply(rows, "Все трассы");
            Expect(all.Count == 120, "всех не 120");
        }

        private static string NewTempFile()
        {
            string folder = Path.Combine(Path.GetTempPath(), "RaceLogChecks");
            Directory.CreateDirectory(folder);
            return Path.Combine(folder, Path.GetRandomFileName() + ".json");
        }

        private static void ExportSix()
        {
            string target = NewTempFile();
            var transfer = new RaceTransfer();
            long count = transfer.ExportAsync(SixSourcePath, target, false, null,
                CancellationToken.None).GetAwaiter().GetResult();
            Expect(count == 6, "экспортировано не 6");
            var json = File.ReadAllText(target);
            var copy = JsonSerializer.Deserialize<List<RaceResult>>(json);
            Expect(copy != null && copy.Count == 6, "в копии не 6 записей");
            Expect(copy![0].LapSeconds == 94, "время в копии не 94");
            File.Delete(target);
        }

        private static void ExportUnderFilter()
        {
            string target = NewTempFile();
            var transfer = new RaceTransfer();
            long count = transfer.ExportAsync(BigSourcePath, target, false, null,
                CancellationToken.None).GetAwaiter().GetResult();
            Expect(count == 120, "экспортировано не 120");
            var json = File.ReadAllText(target);
            var copy = JsonSerializer.Deserialize<List<RaceResult>>(json);
            Expect(copy != null && copy.Count == 120, "в копии не 120 записей");
            File.Delete(target);
        }

        private static void ExportEmpty()
        {
            string target = NewTempFile();
            var transfer = new RaceTransfer();
            long count = transfer.ExportAsync(EmptySourcePath, target, false, null,
                CancellationToken.None).GetAwaiter().GetResult();
            Expect(count == 0, "экспортировано не 0");
            var json = File.ReadAllText(target);
            var copy = JsonSerializer.Deserialize<List<RaceResult>>(json);
            Expect(copy != null && copy.Count == 0, "в копии не пустой список");
            File.Delete(target);
        }

        private static void ExportSamePath()
        {
            var transfer = new RaceTransfer();
            bool thrown = false;
            try
            {
                transfer.ExportAsync(SixSourcePath, SixSourcePath, false, null,
                    CancellationToken.None).GetAwaiter().GetResult();
            }
            catch (ArgumentException)
            {
                thrown = true;
            }
            Expect(thrown, "замена базы не отклонена");
            var rows = LoadAsyncChecked(SixSourcePath, false, null, CancellationToken.None)
                .GetAwaiter().GetResult();
            Expect(rows.Count == 6, "база повреждена");
        }

        private static void ExportToFolder()
        {
            var transfer = new RaceTransfer();
            bool thrown = false;
            try
            {
                transfer.ExportAsync(SixSourcePath, dataFolder, false, null,
                    CancellationToken.None).GetAwaiter().GetResult();
            }
            catch (ArgumentException)
            {
                thrown = true;
            }
            Expect(thrown, "экспорт в папку не отклонён");
        }

        private static void ExportCancelKeepsOld()
        {
            string folder = Path.Combine(Path.GetTempPath(), "RaceLogChecks",
                Path.GetRandomFileName());
            Directory.CreateDirectory(folder);
            string target = Path.Combine(folder, "old.json");

            var transfer = new RaceTransfer();
            transfer.ExportAsync(SixSourcePath, target, false, null, CancellationToken.None)
                .GetAwaiter().GetResult();

            using var cts = new CancellationTokenSource();
            cts.CancelAfter(200);
            bool cancelled = false;
            try
            {
                transfer.ExportAsync(BigSourcePath, target, true, null, cts.Token)
                    .GetAwaiter().GetResult();
            }
            catch (OperationCanceledException)
            {
                cancelled = true;
            }
            Expect(cancelled, "медленный экспорт успел завершиться, уменьшите таймаут");

            var json = File.ReadAllText(target);
            var copy = JsonSerializer.Deserialize<List<RaceResult>>(json);
            Expect(copy != null && copy.Count == 6, "прежняя копия повреждена");
            Directory.Delete(folder, true);
        }

        private static void ExportBrokenKeepsOld()
        {
            string folder = Path.Combine(Path.GetTempPath(), "RaceLogChecks",
                Path.GetRandomFileName());
            Directory.CreateDirectory(folder);
            string target = Path.Combine(folder, "old.json");

            var transfer = new RaceTransfer();
            transfer.ExportAsync(SixSourcePath, target, false, null, CancellationToken.None)
                .GetAwaiter().GetResult();

            bool thrown = false;
            try
            {
                transfer.ExportAsync(BrokenSourcePath, target, false, null, CancellationToken.None)
                    .GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                thrown = ex is not OperationCanceledException;
            }
            Expect(thrown, "ошибка чтения не получена");

            var json = File.ReadAllText(target);
            var copy = JsonSerializer.Deserialize<List<RaceResult>>(json);
            Expect(copy != null && copy.Count == 6, "прежняя копия повреждена");
            Directory.Delete(folder, true);
        }

        private static void ReloadAfterCancel()
        {
            using var cts = new CancellationTokenSource();
            var progress = new CancelAfterCount(cts, 5);
            try
            {
                LoadAsyncChecked(BigSourcePath, true, progress, cts.Token).GetAwaiter().GetResult();
            }
            catch (OperationCanceledException)
            {
            }

            using var cts2 = new CancellationTokenSource();
            var rows = LoadAsyncChecked(BigSourcePath, false, null, cts2.Token)
                .GetAwaiter().GetResult();
            Expect(rows.Count == 120, "повторная загрузка не удалась");
        }
    }
}
