using TicTacToe.ViewModels;
using Xamarin.Forms;

namespace TicTacToe.Views
{
    public partial class MainMenu : ContentPage
    {
        public MainMenu(MainMenuViewModel mainMenuViewModel)
        {
            BindingContext = mainMenuViewModel;
            InitializeComponent();
        }
    }
}
