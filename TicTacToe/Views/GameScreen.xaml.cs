using System;
using System.Collections.Generic;
using TicTacToe.Converters;
using TicTacToe.ViewModels;
using Xamarin.Forms;

namespace TicTacToe.Views
{
    public partial class GameScreen : ContentPage
    {
        GameScreenViewModel gameScreenViewModel;

        public GameScreen(GameScreenViewModel _gameScreenViewModel)
        {
            gameScreenViewModel = _gameScreenViewModel;
            BindingContext = _gameScreenViewModel;
            InitializeComponent();
        }
    }
}
