using PilotNotes.Core;

namespace NotesMaui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            string folder = Path.Combine(FileSystem.Current.AppDataDirectory, "PilotNotes.Maui");
            var store = new JsonNoteStore(folder);
            var viewModel = new NotesViewModel(store);
            viewModel.Load();
            BindingContext = viewModel;
        }
    }
}
