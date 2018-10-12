using System;
using System.Collections.Generic;
using TicTacToe.ViewModels;
using Xamarin.Forms;

namespace TicTacToe.Views
{
    public partial class SettingsMenu : ContentPage
    {
        public SettingsMenu(SettingsMenuViewModel _settingsViewModel)
        {
            BindingContext = _settingsViewModel;
            InitializeComponent();
        }
    }
}
