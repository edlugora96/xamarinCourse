using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinTutorial.Views;
using XamarinTutorial.Helpers;

namespace XamarinTutorial.ViewModels
{
    public class MenuItemViewModel
    {
        #region Properties
        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }
        #endregion

        #region Commands
        public ICommand NavigateCommand
        {
            get
            {
                return new RelayCommand(Navigate);
            }
        }
        #endregion

        #region Methods
        private void Navigate()
        {
            App.Master.IsPresented = false;

            if (this.PageName == "LoginPage")
            {
                Settings.IsRemembered = "false";
                Settings.Token = string.Empty;
                Settings.User = string.Empty;
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Token = null;
                mainViewModel.User = null;
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            else if (this.PageName == "MyProfilePage")
            {
                MainViewModel.GetInstance().MyProfile = new MyProfileViewModel();
                App.Navigator.PushAsync(new MyProfilePage());
            }
        } 
        #endregion
    }
}
