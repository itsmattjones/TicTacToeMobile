using TicTacToe.Infrastructure.Services;
using TicTacToe.ViewModels;
using Xamarin.Forms;

namespace TicTacToe.Views
{
    public partial class SettingsMenu : ContentPage
    {
        public SettingsMenu()
        {
            BindingContext = App.Locator.SettingsMenuViewModel;
            InitializeComponent();
        }
    }
}
