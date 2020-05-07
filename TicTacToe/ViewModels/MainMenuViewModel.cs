using System.ComponentModel;
using Xamarin.Forms;
using TicTacToe.Services;

namespace TicTacToe.ViewModels
{
    public class MainMenuViewModel : IMainMenuViewModel, INotifyPropertyChanged
    {
        public Command SingleplayerCmd { get; set; }
        public Command MultiplayerCmd { get; set; }
        public Command SettingsCmd { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainMenuViewModel class.
        /// </summary>
        public MainMenuViewModel()
        {
            SingleplayerCmd = new Command(CreateSingleplayer);
            MultiplayerCmd = new Command(CreateMultiplayer);
            SettingsCmd = new Command(() => { App.Locator.NavigationService.NavigateTo(Locator.SettingsMenu); });
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