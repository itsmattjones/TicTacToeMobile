using TicTacToe.ViewModels;
using Xamarin.Forms;

namespace TicTacToe.Views
{
    public partial class SettingsMenu : ContentPage
    {
        public SettingsMenu(SettingsMenuViewModel settingsViewModel)
        {
            BindingContext = settingsViewModel;
            InitializeComponent();
        }
    }
}
