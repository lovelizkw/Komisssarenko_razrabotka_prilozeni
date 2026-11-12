using Avalonia;
using Avalonia.Controls;
using PilotNotes.Core;

namespace NotesAvalonia
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string folder = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "PilotNotes.Avalonia");

            var store = new JsonNoteStore(folder);
            var viewModel = new NotesViewModel(store);
            viewModel.Load();
            DataContext = viewModel;
        }
    }
}
