using TicTacToe.Infrastructure.Services;
using TicTacToe.ViewModels;
using Xamarin.Forms;

namespace TicTacToe.Views
{
    public partial class MainMenu : ContentPage
    {
        public MainMenu()
        {
            BindingContext = App.Locator.MainMenuViewModel;
            InitializeComponent();
        }
    }
}
