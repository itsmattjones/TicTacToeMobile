using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using TicTacToe.ViewModels;

namespace TicTacToe.Services
{
    public class Locator
    {
        public const string MainMenu = "MainMenu";
        public const string SettingsMenu = "SettingsMenu";
        public const string GameScreen = "GameScreen";

        static Locator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var navigation = new NavigationService();
            navigation.Configure(MainMenu, typeof(Views.MainMenu));
            navigation.Configure(SettingsMenu, typeof(Views.SettingsMenu));
            navigation.Configure(GameScreen, typeof(Views.GameScreen));

            SimpleIoc.Default.Register<MainMenuViewModel>();
            SimpleIoc.Default.Register<SettingsMenuViewModel>();
            SimpleIoc.Default.Register<GameScreenViewModel>();
            SimpleIoc.Default.Register(() => navigation);
            SimpleIoc.Default.Register<IGameEngine, GameEngine>();
        }

        public NavigationService NavigationService
        {
            get => ServiceLocator.Current.GetInstance<NavigationService>();
        }

        public GameEngine GameEngine
        {
            get => ServiceLocator.Current.GetInstance<GameEngine>(); 
	    }

        public MainMenuViewModel MainMenuViewModel
        {
            get => ServiceLocator.Current.GetInstance<MainMenuViewModel>();
        }

        public SettingsMenuViewModel SettingsMenuViewModel
        {
            get => ServiceLocator.Current.GetInstance<SettingsMenuViewModel>();
        }

        public GameScreenViewModel GameScreenViewModel
        {
            get => ServiceLocator.Current.GetInstance<GameScreenViewModel>();
        }
    }
}
