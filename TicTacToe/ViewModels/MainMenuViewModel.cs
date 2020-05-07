using Xamarin.Forms;
using GalaSoft.MvvmLight;
using TicTacToe.Infrastructure.Services;
using TicTacToe.Infrastructure.Enums;
using TicTacToe.Infrastructure;
using GalaSoft.MvvmLight.Messaging;
using Xamarin.Essentials;

namespace TicTacToe.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public Command SingleplayerCmd { get; set; }
        public Command MultiplayerCmd { get; set; }
        public Command SettingsCmd { get; set; }

        public MainMenuViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            SingleplayerCmd = new Command(CreateSingleplayer);
            MultiplayerCmd = new Command(CreateMultiplayer);
            SettingsCmd = new Command(OpenSettings);
        }

        private async void CreateSingleplayer() 
        {
            IGameEngine gameEngine = new GameEngine(GameType.Singleplayer, GetAiDifficulty());
            Messenger.Default.Send(gameEngine);
            _navigationService.NavigateTo(AppPages.GameScreenPage);
        }

        private void CreateMultiplayer() 
        { 
            IGameEngine gameEngine = new GameEngine(GameType.Multiplayer, GetAiDifficulty());
            Messenger.Default.Send(gameEngine);
            _navigationService.NavigateTo(AppPages.GameScreenPage);
        }

        private async void OpenSettings() 
        {
            _navigationService.NavigateTo(AppPages.SettingsMenuPage);
        }

        private AiDifficulty GetAiDifficulty()
        {
            switch (Preferences.Get("AiDifficulty", "medium"))
            {
                case "easy":
                    return AiDifficulty.Easy;
                case "medium":
                    return AiDifficulty.Medium;
                case "hard":
                    return AiDifficulty.Hard;
                default: // Default to medium
                    return AiDifficulty.Medium;
            }
        }
    }
}