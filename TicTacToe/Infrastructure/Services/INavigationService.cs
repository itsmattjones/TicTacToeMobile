using System;
using TicTacToe.Infrastructure.Enums;
using Xamarin.Forms;

namespace TicTacToe.Infrastructure.Services
{
    public interface INavigationService
    {
        void GoBack();
        void NavigateTo(AppPages pageKey);
        void NavigateTo(AppPages pageKey, object parameter);
        void Configure(AppPages mainMenuPage, Type type);
        void Initialize(NavigationPage firstPage);
    }
}
