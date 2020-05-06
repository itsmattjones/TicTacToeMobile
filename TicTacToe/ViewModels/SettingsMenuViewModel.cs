using System.ComponentModel;
using TicTacToe.Models;
using Xamarin.Forms;

namespace TicTacToe.ViewModels
{
    public class SettingsMenuViewModel : INotifyPropertyChanged
    {
        private AiDifficulty _chosenDifficulty;

        public Command ChangeDifficultyCommand { get; set; }
        public Command MainMenuCommand { get; set; }

        public AiDifficulty ChosenDifficulty
        {
            get => _chosenDifficulty;
            set
            {
                _chosenDifficulty = value;
                NotifyPropertyChanged("ChosenDifficulty");
            }
        }

        public SettingsMenuViewModel()
        {
            ChangeDifficultyCommand = new Command<string>(ChangeDifficulty);
            MainMenuCommand = new Command(ShowMainMenu);
        }

        private void ChangeDifficulty(string difficultyType)
        {
        }

        private void ShowMainMenu()
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
