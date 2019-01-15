using TicTacToe.ViewModels;
using Xamarin.Forms;

namespace TicTacToe.Views
{
    public partial class SettingsMenu : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the Settings Menu View class.
        /// </summary>
        public SettingsMenu()
        {
            BindingContext = new SettingsMenuViewModel();
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the Settings Menu View class.
        /// </summary>
        /// <param name="settingsViewModel">Settings view model.</param>
        public SettingsMenu(SettingsMenuViewModel settingsViewModel)
        {
            BindingContext = settingsViewModel;
            InitializeComponent();
        }
    }
}
