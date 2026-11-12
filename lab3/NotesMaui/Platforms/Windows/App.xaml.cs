using Microsoft.UI.Xaml;

namespace NotesMaui.WinUI
{
    public partial class App : MauiWinApplication
    {
        public App()
        {
            this.InitializeComponent();
        }

        protected override MauiApp CreateMauiApp() => NotesMaui.MauiProgram.CreateMauiApp();
    }
}
