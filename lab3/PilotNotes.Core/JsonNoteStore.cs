using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace PilotNotes.Core
{
    public class JsonNoteStore : INoteStore
    {
        private readonly string path;

        public JsonNoteStore(string folder)
        {
            Directory.CreateDirectory(folder);
            path = Path.Combine(folder, "notes.json");
        }

        public List<Note> Load()
        {
            if (!File.Exists(path))
                return new List<Note>();

            string json = File.ReadAllText(path);
            var notes = JsonSerializer.Deserialize<List<Note>>(json);
            return NoteRules.ValidateCopy(notes);
        }

        public void Save(List<Note> notes)
        {
            NoteRules.ValidateCopy(notes);

            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(path, JsonSerializer.Serialize(notes, options));
        }
    }
}
