using System.ComponentModel;
using TicTacToe.Services;
using Xamarin.Forms;

namespace TicTacToe.ViewModels
{
    public class MainMenuViewModel : INotifyPropertyChanged
    {
        private INavigationService _navigationService;

        public Command SingleplayerCmd { get; set; }
        public Command MultiplayerCmd { get; set; }
        public Command SettingsCmd { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainMenuViewModel class.
        /// </summary>
        public MainMenuViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            SingleplayerCmd = new Command(CreateSingleplayer);
            MultiplayerCmd = new Command(CreateMultiplayer);
            SettingsCmd = new Command(CreateSettings);
        }

        /// <summary>
        /// Creates a singleplayer game.
        /// </summary>
        private void CreateSingleplayer() 
        {
        }

        /// <summary>
        /// Creates a multiplayer game.
        /// </summary>
        private void CreateMultiplayer() 
        { 
        }

        /// <summary>
        /// Shows the settings menu.
        /// </summary>
        private void CreateSettings() 
        {
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}