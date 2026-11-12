using System;
using System.Collections.Generic;
using System.Linq;

namespace PilotNotes.Core
{
    public static class NoteRules
    {
        public static string CleanText(string text)
        {
            string cleaned = (text ?? "").Trim();
            if (cleaned.Length == 0)
                throw new ArgumentException("Введите текст заметки");
            return cleaned;
        }

        public static long NextId(List<Note> notes)
        {
            long max = 0;
            foreach (var note in notes)
            {
                if (note.Id > max)
                    max = note.Id;
            }
            return max + 1;
        }

        public static List<Note> Find(List<Note> notes, string query)
        {
            string text = (query ?? "").Trim();
            if (text.Length == 0)
                return notes.ToList();

            return notes
                .Where(n => n.Text.Contains(text, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public static List<Note> ValidateCopy(List<Note>? notes)
        {
            if (notes == null)
                throw new ArgumentException("Копия не содержит список заметок");

            var seenIds = new List<long>();

            foreach (var note in notes)
            {
                if (note == null)
                    throw new ArgumentException("Копия содержит пустой объект заметки");

                if (note.Id <= 0)
                    throw new ArgumentException("Id должен быть положительным: " + note.Id);

                if (seenIds.Contains(note.Id))
                    throw new ArgumentException("Повтор Id: " + note.Id);

                if (string.IsNullOrWhiteSpace(note.Text))
                    throw new ArgumentException("Заметка с Id " + note.Id + " не содержит текст");

                seenIds.Add(note.Id);
            }

            return notes.ToList();
        }
    }
}
