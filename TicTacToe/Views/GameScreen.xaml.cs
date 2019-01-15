using TicTacToe.ViewModels;
using Xamarin.Forms;

namespace TicTacToe.Views
{
    public partial class GameScreen : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the Game Screen View class.
        /// </summary>
        public GameScreen()
        {
            BindingContext = new GameScreenViewModel();
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the Game Screen View class.
        /// </summary>
        /// <param name="gameScreenViewModel">Game screen view model.</param>
        public GameScreen(GameScreenViewModel gameScreenViewModel)
        {
            BindingContext = gameScreenViewModel;
            InitializeComponent();
        }
    }
}
