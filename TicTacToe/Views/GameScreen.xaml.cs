using TicTacToe.Infrastructure.Services;
using TicTacToe.ViewModels;
using Xamarin.Forms;

namespace TicTacToe.Views
{
    public partial class GameScreen : ContentPage
    {
        public GameScreen()
        {
            BindingContext = App.Locator.GameScreenViewModel;
            InitializeComponent();
        }
    }
}
