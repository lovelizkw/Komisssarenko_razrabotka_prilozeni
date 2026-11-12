using PilotNotes.Core;

namespace Checks
{
    internal static class Program
    {
        private static int passed;
        private static int failed;

        static void Main()
        {
            CheckCleanText();
            CheckNextId();
            CheckFind();
            CheckValidateCopy();
            CheckJsonStoreRoundtrip();
            CheckJsonStoreRejects();
            CheckViewModelAdd();
            CheckViewModelCommands();
            CheckViewModelDelete();
            CheckViewModelSearch();
            CheckViewModelSaveLoad();
            CheckViewModelLoadBlocked();

            Console.WriteLine();
            Console.WriteLine($"Пройдено: {passed}. Отказано: {failed}");
            Environment.ExitCode = failed == 0 ? 0 : 1;
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

        private static void CheckCleanText()
        {
            Check("CleanText удаляет пробелы по краям", () =>
            {
                Expect(NoteRules.CleanText("  Текст  ") == "Текст", "пробелы не удалены");
            });

            Check("CleanText отклоняет пустой текст", () =>
            {
                bool thrown = false;
                try { NoteRules.CleanText("   "); }
                catch (ArgumentException) { thrown = true; }
                Expect(thrown, "нет исключения ArgumentException");
            });
        }

        private static void CheckNextId()
        {
            Check("NextId пустого списка равен 1", () =>
            {
                Expect(NoteRules.NextId(new List<Note>()) == 1, "получено не 1");
            });

            Check("NextId равен максимум плюс один", () =>
            {
                var notes = new List<Note>
                {
                    new Note { Id = 2, Text = "a" },
                    new Note { Id = 7, Text = "b" }
                };
                Expect(NoteRules.NextId(notes) == 8, "получено не 8");
            });
        }

        private static void CheckFind()
        {
            var notes = new List<Note>
            {
                new Note { Id = 1, Text = "На учебном овале тормозить раньше." },
                new Note { Id = 2, Text = "На спринте проверить давление шин." },
                new Note { Id = 3, Text = "На учебном овале держать ровный газ." }
            };

            Check("Find ищет без учёта регистра", () =>
            {
                var found = NoteRules.Find(notes, "ОВАЛ");
                Expect(found.Count == 2, "найдено не 2");
            });

            Check("Find по пустому запросу возвращает все", () =>
            {
                Expect(NoteRules.Find(notes, "").Count == 3, "найдено не 3");
            });

            Check("Find не меняет исходный список", () =>
            {
                Expect(notes.Count == 3, "список изменён");
            });
        }

        private static void CheckValidateCopy()
        {
            Check("ValidateCopy отклоняет null", () =>
            {
                bool thrown = false;
                try { NoteRules.ValidateCopy(null); }
                catch (ArgumentException) { thrown = true; }
                Expect(thrown, "нет исключения ArgumentException");
            });

            Check("ValidateCopy отклоняет повтор Id", () =>
            {
                var notes = new List<Note>
                {
                    new Note { Id = 1, Text = "a" },
                    new Note { Id = 1, Text = "b" }
                };
                bool thrown = false;
                try { NoteRules.ValidateCopy(notes); }
                catch (ArgumentException) { thrown = true; }
                Expect(thrown, "нет исключения ArgumentException");
            });

            Check("ValidateCopy отклоняет пустой текст", () =>
            {
                var notes = new List<Note> { new Note { Id = 1, Text = "   " } };
                bool thrown = false;
                try { NoteRules.ValidateCopy(notes); }
                catch (ArgumentException) { thrown = true; }
                Expect(thrown, "нет исключения ArgumentException");
            });

            Check("ValidateCopy разрешает пустой список", () =>
            {
                Expect(NoteRules.ValidateCopy(new List<Note>()).Count == 0, "список не принят");
            });
        }

        private static string NewTempFolder()
        {
            string folder = Path.Combine(Path.GetTempPath(), "PilotNotesChecks", Path.GetRandomFileName());
            Directory.CreateDirectory(folder);
            return folder;
        }

        private static void CheckJsonStoreRoundtrip()
        {
            string folder = NewTempFolder();
            Check("JsonNoteStore сохраняет и читает заметки", () =>
            {
                var store = new JsonNoteStore(folder);
                var notes = new List<Note> { new Note { Id = 1, Text = "Овал" } };
                store.Save(notes);
                var loaded = store.Load();
                Expect(loaded.Count == 1 && loaded[0].Id == 1 && loaded[0].Text == "Овал", "данные не совпали");
            });

            Check("JsonNoteStore пустое хранилище даёт пустой список", () =>
            {
                var store = new JsonNoteStore(NewTempFolder());
                Expect(store.Load().Count == 0, "список не пуст");
            });
        }

        private static void CheckJsonStoreRejects()
        {
            string folder = NewTempFolder();
            File.WriteAllText(Path.Combine(folder, "notes.json"), "[{\"Id\":1,\"Text\":\"a\"},{\"Id\":1,\"Text\":\"b\"}]");
            Check("JsonNoteStore отклоняет копию с повтором Id", () =>
            {
                var store = new JsonNoteStore(folder);
                bool thrown = false;
                try { store.Load(); }
                catch (ArgumentException) { thrown = true; }
                Expect(thrown, "нет исключения ArgumentException");
            });

            string folder2 = NewTempFolder();
            File.WriteAllText(Path.Combine(folder2, "notes.json"), "не json");
            Check("JsonNoteStore отклоняет повреждённый файл", () =>
            {
                var store = new JsonNoteStore(folder2);
                bool thrown = false;
                try { store.Load(); }
                catch (Exception ex) { thrown = ex is not ArgumentException; }
                Expect(thrown, "исключения нет");
            });
        }

        private static void CheckViewModelAdd()
        {
            Check("ViewModel добавляет заметку и очищает Draft", () =>
            {
                var vm = new NotesViewModel(new JsonNoteStore(NewTempFolder()));
                vm.Draft = "  Учебный овал  ";
                vm.Add();
                Expect(vm.VisibleNotes.Count == 1, "заметка не добавлена");
                Expect(vm.VisibleNotes[0].Text == "Учебный овал", "пробелы не удалены");
                Expect(vm.Draft.Length == 0, "Draft не очищен");
                Expect(vm.IsDirty, "IsDirty не установлен");
            });

            Check("ViewModel не добавляет пустой текст", () =>
            {
                var vm = new NotesViewModel(new JsonNoteStore(NewTempFolder()));
                vm.Draft = "   ";
                vm.Add();
                Expect(vm.VisibleNotes.Count == 0, "заметка создана");
            });
        }

        private static void CheckViewModelCommands()
        {
            Check("AddCommand доступен после ввода текста", () =>
            {
                var vm = new NotesViewModel(new JsonNoteStore(NewTempFolder()));
                Expect(!vm.AddCommand.CanExecute(null), "команда доступна без текста");
                vm.Draft = "Текст";
                Expect(vm.AddCommand.CanExecute(null), "команда недоступна с текстом");
            });

            Check("DeleteCommand доступен после выбора", () =>
            {
                var vm = new NotesViewModel(new JsonNoteStore(NewTempFolder()));
                vm.Draft = "Текст";
                vm.Add();
                vm.SelectedNote = vm.VisibleNotes[0];
                Expect(vm.DeleteCommand.CanExecute(null), "команда недоступна с выбором");
                vm.SelectedNote = null;
                Expect(!vm.DeleteCommand.CanExecute(null), "команда доступна без выбора");
            });
        }

        private static void CheckViewModelDelete()
        {
            Check("Delete удаляет выбранный Id", () =>
            {
                var vm = new NotesViewModel(new JsonNoteStore(NewTempFolder()));
                vm.Draft = "Первая";
                vm.Add();
                vm.Draft = "Вторая";
                vm.Add();
                var target = vm.VisibleNotes[1];
                vm.SelectedNote = target;
                vm.Delete();
                Expect(vm.VisibleNotes.Count == 1, "удалено не по одной");
                Expect(vm.VisibleNotes[0].Text == "Первая", "удалён не тот объект");
                Expect(vm.IsDirty, "IsDirty не установлен");
            });
        }

        private static void CheckViewModelSearch()
        {
            Check("Search и ResetSearch меняют только видимый список", () =>
            {
                var vm = new NotesViewModel(new JsonNoteStore(NewTempFolder()));
                vm.Draft = "Овал первый";
                vm.Add();
                vm.Draft = "Спринт";
                vm.Add();
                vm.Query = "овал";
                vm.Search();
                Expect(vm.VisibleNotes.Count == 1, "найдено не 1");
                vm.ResetSearch();
                Expect(vm.VisibleNotes.Count == 2, "после сброса не 2");
                Expect(vm.CountText.Contains("Всего: 2"), "CountText не совпадает");
            });
        }

        private static void CheckViewModelSaveLoad()
        {
            string folder = NewTempFolder();
            Check("Save и Load сохраняют весь список", () =>
            {
                var vm = new NotesViewModel(new JsonNoteStore(folder));
                vm.Draft = "Первая";
                vm.Add();
                vm.Draft = "Вторая";
                vm.Add();
                vm.Query = "Первая";
                vm.Search();
                vm.Save();
                Expect(!vm.IsDirty, "IsDirty не сброшен");
                Expect(vm.SaveState == "Все изменения сохранены", "SaveState не совпадает");

                var second = new NotesViewModel(new JsonNoteStore(folder));
                second.Load();
                Expect(second.VisibleNotes.Count == 2, "загружено не 2");
            });
        }

        private static void CheckViewModelLoadBlocked()
        {
            string folder = NewTempFolder();
            File.WriteAllText(Path.Combine(folder, "notes.json"), "[{\"Id\":5,\"Text\":\"Старая\"}]");
            Check("Load запрещён при несохранённых изменениях", () =>
            {
                var vm = new NotesViewModel(new JsonNoteStore(folder));
                vm.Draft = "Новая";
                vm.Add();
                vm.Load();
                Expect(vm.VisibleNotes.Count == 1, "новая заметка потеряна");
                Expect(vm.Status == "Сначала сохраните изменения", "статус не совпадает");
            });
        }
    }
}
