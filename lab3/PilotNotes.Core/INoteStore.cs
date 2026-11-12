namespace PilotNotes.Core
{
    public interface INoteStore
    {
        List<Note> Load();
        void Save(List<Note> notes);
    }
}
