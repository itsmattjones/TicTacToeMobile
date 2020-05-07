using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace TicTacToe.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}
            /// 
            SimpleIoc.Default.Register<MainMenuViewModel>();
            SimpleIoc.Default.Register<SettingsMenuViewModel>();
            SimpleIoc.Default.Register<GameScreenViewModel>(true);
        }


        public MainMenuViewModel MainMenuViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainMenuViewModel>();
            }
        }

        public SettingsMenuViewModel SettingsMenuViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsMenuViewModel>();
            }
        }

        public GameScreenViewModel GameScreenViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GameScreenViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
