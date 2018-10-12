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

        public MainMenuViewModel()
        {
            SingleplayerCmd = new Command(CreateSingleplayer);
            MultiplayerCmd = new Command(CreateMultiplayer);
            SettingsCmd = new Command(CreateSettings);
        }

        #region Helper Methods

        private void CreateSingleplayer() 
        {
            Program.gameManager.CreateSingleplayerGame();
        }
        private void CreateMultiplayer() 
        { 
            Program.gameManager.CreateMultiplayerGame();
        }
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