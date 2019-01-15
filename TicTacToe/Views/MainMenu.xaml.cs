using System.Threading;
using System.Threading.Tasks;
using TicTacToe.ViewModels;
using Xamarin.Forms;

namespace TicTacToe.Views
{
    public partial class MainMenu : ContentPage
    {
        private static CancellationTokenSource _cancelToken;

        /// <summary>
        /// Initializes a new instance of the Main Menu View class.
        /// </summary>
        public MainMenu()
        {
            BindingContext = new MainMenuViewModel();
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the Main Menu View class.
        /// </summary>
        /// <param name="mainMenuViewModel">Main menu view model.</param>
        public MainMenu(MainMenuViewModel mainMenuViewModel)
        {
            BindingContext = mainMenuViewModel;
            _cancelToken = new CancellationTokenSource();
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.WhenAll(RotateElement(AppLogoImage, _cancelToken.Token));
        }
        
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _cancelToken.Cancel();
        }
        
        private async Task RotateElement(VisualElement element, CancellationToken cancellation)
        {
            while (!cancellation.IsCancellationRequested)
            {
                await element.RotateTo(15, 1200, Easing.Linear);
                await element.RotateTo(-15, 1200, Easing.Linear);
            }
        }
    }
}
