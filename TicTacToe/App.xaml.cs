using System;
using TicTacToe.ViewModels;
using TicTacToe.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TicTacToe
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Start application through program class.
            Program application = new Program();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
