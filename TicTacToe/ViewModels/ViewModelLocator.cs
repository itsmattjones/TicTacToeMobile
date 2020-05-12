using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace TicTacToe.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainMenuViewModel>();
            SimpleIoc.Default.Register<GameScreenViewModel>(true);
            SimpleIoc.Default.Register<SettingsMenuViewModel>();
        }

        public MainMenuViewModel MainMenuViewModel
        {
            get => ServiceLocator.Current.GetInstance<MainMenuViewModel>();
        }

        public GameScreenViewModel GameScreenViewModel
        {
            get => ServiceLocator.Current.GetInstance<GameScreenViewModel>();
        }

        public SettingsMenuViewModel SettingsMenuViewModel
        {
            get => ServiceLocator.Current.GetInstance<SettingsMenuViewModel>();
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
