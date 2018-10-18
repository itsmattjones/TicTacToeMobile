using System.ComponentModel;
using Xamarin.Forms;

namespace TicTacToe.ViewModels
{
    public class MainMenuViewModel : INotifyPropertyChanged
    {

        #region Properties

        public Command SingleplayerCmd { get; set; }
        public Command MultiplayerCmd { get; set; }
        public Command SettingsCmd { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the MainMenuViewModel class.
        /// </summary>
        public MainMenuViewModel()
        {
            SingleplayerCmd = new Command(CreateSingleplayer);
            MultiplayerCmd = new Command(CreateMultiplayer);
            SettingsCmd = new Command(CreateSettings);
        }

        #region Helper Methods

        /// <summary>
        /// Creates a singleplayer game.
        /// </summary>
        private void CreateSingleplayer() 
        {
            Program.GameManager.CreateSingleplayerGame();
        }

        /// <summary>
        /// Creates a multiplayer game.
        /// </summary>
        private void CreateMultiplayer() 
        { 
            Program.GameManager.CreateMultiplayerGame();
        }

        /// <summary>
        /// Shows the settings menu.
        /// </summary>
        private void CreateSettings() 
        {
            Program.ShowSettingsMenu();
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}