using GalaSoft.MvvmLight;
using TicTacToe.Infrastructure.Enums;
using TicTacToe.Infrastructure.Services;
using TicTacToe.Models;
using Xamarin.Forms;

namespace TicTacToe.ViewModels
{
    public class SettingsMenuViewModel : ViewModelBase
    {
        public Command ChangeDifficultyCommand { get; set; }
        public Command MainMenuCommand { get; set; }

        private readonly INavigationService _navigationService;
        public AiDifficulty ChosenDifficulty { get; set; }
        public string NavigationPath { get; }

        public SettingsMenuViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            ChangeDifficultyCommand = new Command<string>(ChangeDifficulty);
            MainMenuCommand = new Command(ShowMainMenu);
        }

        private void ChangeDifficulty(string difficultyType)
        {
        }

        private void ShowMainMenu()
        {
            _navigationService.GoBack();
        }
    }
}
