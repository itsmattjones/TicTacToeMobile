using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TicTacToe.Views;
using GalaSoft.MvvmLight.Ioc;
using TicTacToe.ViewModels;
using TicTacToe.Infrastructure.Services;
using TicTacToe.Infrastructure.Enums;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TicTacToe
{
    public partial class App : Application
    {
        //ViewModelLocator object to handle ViewModels and bindings between them and Views (Pages):
        private static ViewModelLocator _locator;
        public static ViewModelLocator Locator
        {
            get
            {
                return _locator ?? (_locator = new ViewModelLocator());
            }
        }

        public App()
        {
            InitializeComponent();

            INavigationService navigationService;

            if (!SimpleIoc.Default.IsRegistered<INavigationService>())
            {
                // Setup navigation service:
                navigationService = new NavigationService();

                // Configure pages:
                navigationService.Configure(AppPages.MainMenuPage, typeof(MainMenu));
                navigationService.Configure(AppPages.GameScreenPage, typeof(GameScreen));
                navigationService.Configure(AppPages.SettingsMenuPage, typeof(SettingsMenu));

                // Register NavigationService in IoC container:
                SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            }
            else
                navigationService = SimpleIoc.Default.GetInstance<INavigationService>();

            // Create new Navigation Page and set MainPage as its default page:
            var firstPage = new NavigationPage(new MainMenu());

            // Set Navigation page as default page for Navigation Service:
            navigationService.Initialize(firstPage);

            // You have to also set MainPage property for the app:
            MainPage = firstPage;
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
