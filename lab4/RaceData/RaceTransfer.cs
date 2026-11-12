using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace RaceData
{
    public class RaceTransfer
    {
        public Task<List<RaceResult>> LoadAsync(string sourcePath, bool slowMode,
            IProgress<WorkProgress>? progress, CancellationToken token)
        {
            return Task.Run(() => ReadSourceAsync(sourcePath, slowMode, progress, token), token);
        }

        public Task<long> ExportAsync(string sourcePath, string targetPath, bool slowMode,
            IProgress<WorkProgress>? progress, CancellationToken token)
        {
            return Task.Run(() => WriteCopyAsync(sourcePath, targetPath, slowMode, progress, token), token);
        }

        private static async Task<List<RaceResult>> ReadSourceAsync(string sourcePath, bool slowMode,
            IProgress<WorkProgress>? progress, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            if (!File.Exists(sourcePath))
                throw new FileNotFoundException("Файл базы не найден: " + sourcePath);

            var builder = new SqliteConnectionStringBuilder
            {
                DataSource = sourcePath,
                Mode = SqliteOpenMode.ReadOnly
            };

            var rows = new List<RaceResult>();

            using (var connection = new SqliteConnection(builder.ToString()))
            {
                connection.Open();

                long total = CountRows(connection);

                progress?.Report(new WorkProgress("Чтение", 0, total));

                using var transaction = connection.BeginTransaction(deferred: true);

                using var command = connection.CreateCommand();
                command.Transaction = transaction;
                command.CommandText =
                    "SELECT Id, Driver, Car, Track, LapSeconds FROM RaceResults ORDER BY Id";

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    token.ThrowIfCancellationRequested();

                    var row = new RaceResult
                    {
                        Id = reader.GetInt64(0),
                        Driver = reader.GetString(1),
                        Car = reader.GetString(2),
                        Track = reader.GetString(3),
                        LapSeconds = reader.GetInt32(4)
                    };
                    RaceRules.Validate(row);

                    rows.Add(row);
                    progress?.Report(new WorkProgress("Чтение", rows.Count, total));

                    if (slowMode)
                        await Task.Delay(50, token);
                }
            }

            token.ThrowIfCancellationRequested();
            return rows;
        }

        private static long CountRows(SqliteConnection connection)
        {
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM RaceResults";
            return (long)command.ExecuteScalar()!;
        }

        private static async Task<long> WriteCopyAsync(string sourcePath, string targetPath, bool slowMode,
            IProgress<WorkProgress>? progress, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            if (Path.GetFullPath(sourcePath!) == Path.GetFullPath(targetPath!))
                throw new ArgumentException("Путь копии совпадает с путём источника");

            if (Directory.Exists(targetPath))
                throw new ArgumentException("Путь копии является папкой: " + targetPath);

            var rows = await ReadSourceAsync(sourcePath, slowMode, progress, token);

            token.ThrowIfCancellationRequested();

            string directory = Path.GetDirectoryName(Path.GetFullPath(targetPath))!;
            progress?.Report(new WorkProgress("Запись копии", 0, 0));

            string temporary = Path.Combine(directory, Path.GetRandomFileName());
            try
            {
                using (var stream = new FileStream(temporary, FileMode.CreateNew, FileAccess.Write,
                    FileShare.None, 4096, FileOptions.Asynchronous))
                {
                    await JsonSerializer.SerializeAsync(stream, rows,
                        new JsonSerializerOptions { WriteIndented = true }, token);
                    await stream.FlushAsync(token);
                }

                token.ThrowIfCancellationRequested();

                File.Move(temporary, targetPath, overwrite: true);

                progress?.Report(new WorkProgress("Запись копии", rows.Count, rows.Count));
                return rows.Count;
            }
            finally
            {
                if (File.Exists(temporary))
                {
                    try
                    {
                        File.Delete(temporary);
                    }
                    catch (IOException)
                    {
                    }
                    catch (UnauthorizedAccessException)
                    {
                    }
                }
            }
        }
    }
}
