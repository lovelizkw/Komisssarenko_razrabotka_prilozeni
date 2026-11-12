using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using PilotNotes.Core;

namespace NotesWinForms
{
    public partial class MainForm : Form
    {
        private List<Note> allNotes = new List<Note>();
        private JsonNoteStore store;
        private bool isDirty;

        public MainForm()
        {
            InitializeComponent();

            lstNotes.DisplayMember = nameof(Note.Text);

            string folder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "PilotNotes.WinForms");
            Directory.CreateDirectory(folder);
            lblPath.Text = folder;

            store = new JsonNoteStore(folder);

            txtDraft.TextChanged += txtDraft_TextChanged;
            lstNotes.SelectedIndexChanged += lstNotes_SelectedIndexChanged;

            LoadNotes();
            UpdateState();
        }

        private void LoadNotes()
        {
            try
            {
                allNotes = store.Load();
                lblStatus.Text = $"Загружено заметок: {allNotes.Count}";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Ошибка загрузки: " + ex.Message;
            }
        }

        private void RenderNotes()
        {
            lstNotes.DataSource = NoteRules.Find(allNotes, txtQuery.Text);
            lstNotes.ClearSelected();
            UpdateState();
        }

        private void UpdateState()
        {
            lblCount.Text = $"Показано: {lstNotes.Items.Count}. Всего: {allNotes.Count}";
            lblSaveState.Text = isDirty ? "Есть несохранённые изменения" : "Все изменения сохранены";
        }

        private void btnAdd_Click(object? sender, EventArgs e)
        {
            try
            {
                string text = NoteRules.CleanText(txtDraft.Text);
                var note = new Note
                {
                    Id = NoteRules.NextId(allNotes),
                    Text = text
                };
                allNotes.Add(note);
                isDirty = true;
                RenderNotes();
                txtDraft.Clear();
                lblStatus.Text = "Заметка добавлена";
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.Message;
            }
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            if (lstNotes.SelectedItem is not Note note)
            {
                lblStatus.Text = "Выберите заметку";
                return;
            }

            var found = allNotes.FirstOrDefault(n => n.Id == note.Id);
            if (found == null)
            {
                lblStatus.Text = "Заметка не найдена";
                return;
            }

            allNotes.Remove(found);
            isDirty = true;
            RenderNotes();
            lblStatus.Text = "Заметка удалена";
        }

        private void btnSearch_Click(object? sender, EventArgs e)
        {
            RenderNotes();
            lblStatus.Text = "Поиск выполнен";
        }

        private void btnReset_Click(object? sender, EventArgs e)
        {
            txtQuery.Clear();
            RenderNotes();
            lblStatus.Text = "Показаны все заметки";
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            try
            {
                store.Save(allNotes);
                isDirty = false;
                UpdateState();
                lblStatus.Text = "Заметки сохранены";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Ошибка сохранения: " + ex.Message;
            }
        }

        private void btnLoad_Click(object? sender, EventArgs e)
        {
            if (isDirty)
            {
                lblStatus.Text = "Сначала сохраните изменения";
                return;
            }

            try
            {
                allNotes = store.Load();
                txtDraft.Clear();
                txtQuery.Clear();
                RenderNotes();
                lblStatus.Text = $"Загружено заметок: {allNotes.Count}";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Ошибка загрузки: " + ex.Message;
            }
        }

        private void txtDraft_TextChanged(object? sender, EventArgs e)
        {
            btnAdd.Enabled = txtDraft.Text.Trim().Length > 0;
        }

        private void lstNotes_SelectedIndexChanged(object? sender, EventArgs e)
        {
            btnDelete.Enabled = lstNotes.SelectedItem is Note;
        }
    }
}
