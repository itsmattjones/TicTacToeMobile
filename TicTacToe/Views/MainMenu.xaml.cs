using System;
using System.Collections.Generic;
using TicTacToe.ViewModels;
using Xamarin.Forms;

namespace TicTacToe.Views
{
    public partial class MainMenu : ContentPage
    {
        public MainMenu(MainMenuViewModel _mainMenuViewModel)
        {
            BindingContext = _mainMenuViewModel;
            InitializeComponent();
        }
    }
}
