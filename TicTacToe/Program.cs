using TicTacToe.Models.Player;
using TicTacToe.ViewModels;
using TicTacToe.Views;
using Xamarin.Forms;

namespace TicTacToe
{
    public class Program
    {
        public static GameManager GameManager;
        public static AiDifficulty AiDifficulty;

        public Program()
        {
            GameManager = new GameManager();
            AiDifficulty = AiDifficulty.Medium;
            
            ReturnToMainMenu();
        }

        public static void ReturnToMainMenu()
        {
            Application.Current.MainPage = new NavigationPage(new MainMenu(new MainMenuViewModel()));
        }

        public static void ShowSettingsMenu()
        {
            Application.Current.MainPage = new NavigationPage(new SettingsMenu(new SettingsMenuViewModel()));
        }
    }
}
