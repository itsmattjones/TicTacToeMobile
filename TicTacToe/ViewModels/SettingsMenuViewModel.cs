using GalaSoft.MvvmLight;
using TicTacToe.Infrastructure.Services;
using TicTacToe.Infrastructure;
using Xamarin.Forms;
using Xamarin.Essentials;
using GalaSoft.MvvmLight.Messaging;
using TicTacToe.Infrastructure.Enums;

namespace TicTacToe.ViewModels
{
    public class SettingsMenuViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private bool _player1AvatarDialogActive;
        private bool _player2AvatarDialogActive;

        public Command ChangeDifficultyCommand { get; set; }
        public Command ShowAvatarSelectionDialogCommand { get; set; }
        public Command SelectAvatarCommand { get; set; }
        public Command MainMenuCommand { get; set; }

        public SettingsMenuViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            ChangeDifficultyCommand = new Command<string>(ChangeDifficulty);
            SelectAvatarCommand = new Command<string>(SelectAvatar);
            ShowAvatarSelectionDialogCommand = new Command<string>(ShowAvatarSelectionDialog);
            MainMenuCommand = new Command(() => { _navigationService.GoBack(); });
        }

        public AiDifficulty Difficulty 
	    {
            get
            {
                switch (Preferences.Get("Difficulty", "medium"))
                {
                    case "easy": return AiDifficulty.Easy; 
                    case "medium": return AiDifficulty.Medium; 
                    case "hard": return AiDifficulty.Hard;
                    default: return AiDifficulty.Medium;
		        }
	        }
            set 
	        {
                switch (value)
                {
                    case AiDifficulty.Easy: Preferences.Set("Difficulty", "easy"); break;
                    case AiDifficulty.Medium: Preferences.Set("Difficulty", "medium"); break;
                    case AiDifficulty.Hard: Preferences.Set("Difficulty", "hard"); break; 
		        }
                RaisePropertyChanged();
	        } 
	    }

        public bool Player1AvatarDialogActive
        {
            get => _player1AvatarDialogActive;
            set
            {
                _player1AvatarDialogActive = value;
                RaisePropertyChanged();
            }
        }

        public bool Player2AvatarDialogActive
        {
            get => _player2AvatarDialogActive;
            set
            {
                _player2AvatarDialogActive = value;
                RaisePropertyChanged();
            }
        }

        public int Player1AvatarId
        {
            get => Preferences.Get("Player1AvatarId", 1);
            set
            {
                Preferences.Set("Player1AvatarId", value);
                RaisePropertyChanged();
            }
	    }

        public int Player2AvatarId
        { 
            get => Preferences.Get("Player2AvatarId", 2);
            set
            {
                Preferences.Set("Player2AvatarId", value);
                RaisePropertyChanged(); 
            }
	    }

        #region Helpers
        private void ShowAvatarSelectionDialog(string forPlayerId)
        {
            if (int.Parse(forPlayerId) == 1)
                Player1AvatarDialogActive = true;
            else if (int.Parse(forPlayerId) == 2)
                Player2AvatarDialogActive = true;
	    }

        private void SelectAvatar(string parameters)
        {
            string[] param = parameters.Split(';');

            if (int.Parse(param[0]) == 1)
            {
                Player1AvatarId = int.Parse(param[1]);
                Player1AvatarDialogActive = false;
            }
            else if (int.Parse(param[0]) == 2)
            {
                Player2AvatarId = int.Parse(param[1]);
                Player2AvatarDialogActive = false;
            }
        }

        private void ChangeDifficulty(string difficultyType)
        {
            switch (difficultyType)
	        {
                case "easy": Difficulty = AiDifficulty.Easy; break; 
                case "medium": Difficulty = AiDifficulty.Medium; break;
                case "hard": Difficulty = AiDifficulty.Hard; break;
	        }
        }
        #endregion
    }
}
