using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace PilotNotes.Core
{
    public class NotesViewModel : INotifyPropertyChanged
    {
        private readonly INoteStore store;
        private readonly List<Note> allNotes = new List<Note>();
        private string draft = "";
        private string query = "";
        private Note? selectedNote;
        private string status = "Готово";
        private bool isDirty;

        public NotesViewModel(INoteStore store)
        {
            this.store = store;

            AddCommand = new RelayCommand(Add, () => !string.IsNullOrWhiteSpace(Draft));
            DeleteCommand = new RelayCommand(Delete, () => SelectedNote is not null);
            SearchCommand = new RelayCommand(Search);
            ResetSearchCommand = new RelayCommand(ResetSearch);
            SaveCommand = new RelayCommand(Save);
            LoadCommand = new RelayCommand(Load, () => !IsDirty);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public RelayCommand AddCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetSearchCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand LoadCommand { get; }

        public ObservableCollection<Note> VisibleNotes { get; } = new ObservableCollection<Note>();

        public string Draft
        {
            get { return draft; }
            set
            {
                if (draft == value)
                    return;
                draft = value ?? "";
                Changed(nameof(Draft));
                AddCommand.Refresh();
            }
        }

        public string Query
        {
            get { return query; }
            set
            {
                if (query == value)
                    return;
                query = value ?? "";
                Changed(nameof(Query));
            }
        }

        public Note? SelectedNote
        {
            get { return selectedNote; }
            set
            {
                if (selectedNote == value)
                    return;
                selectedNote = value;
                Changed(nameof(SelectedNote));
                DeleteCommand.Refresh();
            }
        }

        public string Status
        {
            get { return status; }
            set
            {
                if (status == value)
                    return;
                status = value ?? "";
                Changed(nameof(Status));
            }
        }

        public bool IsDirty
        {
            get { return isDirty; }
            private set
            {
                if (isDirty == value)
                    return;
                isDirty = value;
                Changed(nameof(IsDirty));
                Changed(nameof(SaveState));
                LoadCommand.Refresh();
            }
        }

        public string CountText
        {
            get { return $"Показано: {VisibleNotes.Count}. Всего: {allNotes.Count}"; }
        }

        public string SaveState
        {
            get { return IsDirty ? "Есть несохранённые изменения" : "Все изменения сохранены"; }
        }

        public void Add()
        {
            try
            {
                string text = NoteRules.CleanText(Draft);
                var note = new Note
                {
                    Id = NoteRules.NextId(allNotes),
                    Text = text
                };
                allNotes.Add(note);
                IsDirty = true;
                Draft = "";
                RebuildVisible();
                Status = "Заметка добавлена";
            }
            catch (Exception ex)
            {
                Status = ex.Message;
            }
        }

        public void Delete()
        {
            if (SelectedNote is not Note note)
            {
                Status = "Выберите заметку";
                return;
            }

            var found = allNotes.FirstOrDefault(n => n.Id == note.Id);
            if (found == null)
            {
                Status = "Заметка не найдена";
                return;
            }

            allNotes.Remove(found);
            IsDirty = true;
            RebuildVisible();
            Status = "Заметка удалена";
        }

        public void Search()
        {
            RebuildVisible();
            Status = "Поиск выполнен";
        }

        public void ResetSearch()
        {
            Query = "";
            RebuildVisible();
            Status = "Показаны все заметки";
        }

        public void Save()
        {
            try
            {
                store.Save(allNotes);
                IsDirty = false;
                Status = "Заметки сохранены";
            }
            catch (Exception ex)
            {
                Status = "Ошибка сохранения: " + ex.Message;
            }
        }

        public void Load()
        {
            if (IsDirty)
            {
                Status = "Сначала сохраните изменения";
                return;
            }

            try
            {
                var notes = store.Load();
                allNotes.Clear();
                allNotes.AddRange(notes);
                Draft = "";
                Query = "";
                RebuildVisible();
                Status = $"Загружено заметок: {allNotes.Count}";
            }
            catch (Exception ex)
            {
                Status = "Ошибка загрузки: " + ex.Message;
            }
        }

        private void RebuildVisible()
        {
            SelectedNote = null;
            VisibleNotes.Clear();
            foreach (var note in NoteRules.Find(allNotes, Query))
                VisibleNotes.Add(note);
            Changed(nameof(CountText));
        }

        private void Changed(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
