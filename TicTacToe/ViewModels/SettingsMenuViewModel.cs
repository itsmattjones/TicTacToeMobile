using GalaSoft.MvvmLight;
using TicTacToe.Infrastructure.Services;
using TicTacToe.Infrastructure;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace TicTacToe.ViewModels
{
    public class SettingsMenuViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public Command ChangeDifficultyCommand { get; set; }
        public Command MainMenuCommand { get; set; }

        public SettingsMenuViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            ChangeDifficultyCommand = new Command<string>(ChangeDifficulty);
            MainMenuCommand = new Command(ShowMainMenu);
        }

        public AiDifficulty ChosenDifficulty 
	    {
            get
            {
                switch (Preferences.Get("AiDifficulty", "medium"))
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
                    case AiDifficulty.Easy: Preferences.Set("AiDifficulty", "easy"); break;
                    case AiDifficulty.Medium: Preferences.Set("AiDifficulty", "medium"); break;
                    case AiDifficulty.Hard: Preferences.Set("AiDifficulty", "hard"); break; 
		        }
                RaisePropertyChanged();
	        } 
	    }

        private void ChangeDifficulty(string difficultyType)
        {
            switch (difficultyType)
	        {
                case "easy": ChosenDifficulty = AiDifficulty.Easy; break; 
                case "medium": ChosenDifficulty = AiDifficulty.Medium; break;
                case "hard": ChosenDifficulty = AiDifficulty.Hard; break;
	        }
        }

        private void ShowMainMenu()
        {
            _navigationService.GoBack();
        }
    }
}
