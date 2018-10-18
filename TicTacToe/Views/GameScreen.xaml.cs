using TicTacToe.ViewModels;
using Xamarin.Forms;

namespace TicTacToe.Views
{
    public partial class GameScreen : ContentPage
    {
        public GameScreen(GameScreenViewModel gameScreenViewModel)
        {
            BindingContext = gameScreenViewModel;
            InitializeComponent();
        }
    }
}
