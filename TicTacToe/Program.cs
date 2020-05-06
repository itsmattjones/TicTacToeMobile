using TicTacToe.ViewModels;
using TicTacToe.Views;
using TicTacToe.Services;
using Xamarin.Forms;
using SimpleInjector;

namespace TicTacToe
{
    static class Program
    {
        static readonly Container container; 

        static Program()
        {
            container = new Container();

            container.Register<INavigationService, NavigationService>();
            container.Register<IGameEngine, GameEngine>();

            container.Register<MainMenu>();
            container.Register<MainMenuViewModel>();

            container.Register<SettingsMenu>();
            container.Register<SettingsMenuViewModel>();

            container.Register<GameScreen>();
            container.Register<GameScreenViewModel>();

            container.Verify();
        }

        static void Main(string[] args)
        {
            var navigationService = container.GetInstance<NavigationService>();

            navigationService.Configure("MainMenu", typeof(Views.MainMenu));
            navigationService.Configure("SettingsMenu", typeof(Views.SettingsMenu));
            navigationService.Configure("GameScreen", typeof(Views.GameScreen));

            Application.Current.MainPage = navigationService.SetRootPage("MainMenu");
        }
    }
}
