using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TicTacToe.Services;
using TicTacToe.Views;
using GalaSoft.MvvmLight.Ioc;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TicTacToe
{
    public partial class App : Application
    {
        private static Locator _locator;
        public static Locator Locator { get{ return _locator ?? (_locator = new Locator()); } }

        public App()
        {
            var navigationPage = new NavigationPage(new MainMenu());
            Locator.NavigationService.Initialize(navigationPage);
            MainPage = navigationPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
