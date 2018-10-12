using TicTacToe.Models;
using TicTacToe.ViewModels;
using TicTacToe.Views;
using Xamarin.Forms;

namespace TicTacToe
{
    public class Program
    {
        public static GameManager gameManager;
        public static AIDifficulty aiDifficulty;

        public Program()
        {
            gameManager = new GameManager();
            aiDifficulty = AIDifficulty.Medium;
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
