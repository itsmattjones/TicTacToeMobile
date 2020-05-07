using Xamarin.Forms;
using GalaSoft.MvvmLight;
using TicTacToe.Infrastructure.Services;
using TicTacToe.Infrastructure.Enums;
using TicTacToe.Infrastructure;
using GalaSoft.MvvmLight.Messaging;

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
            IGameEngine gameEngine = new GameEngine(GameType.Singleplayer);
            Messenger.Default.Send(gameEngine);
            _navigationService.NavigateTo(AppPages.GameScreenPage);
        }

        private void CreateMultiplayer() 
        { 
            IGameEngine gameEngine = new GameEngine(GameType.Multiplayer);
            Messenger.Default.Send(gameEngine);
            _navigationService.NavigateTo(AppPages.GameScreenPage);
        }

        private async void OpenSettings() 
        {
            _navigationService.NavigateTo(AppPages.SettingsMenuPage);
        }
    }
}