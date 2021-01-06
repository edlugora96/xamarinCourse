using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using XamarinTutorial.Models;
using XamarinTutorial.Helpers;
using XamarinTutorial.BaseViewModels;

namespace XamarinTutorial.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Attributes
        private User user;
        #endregion

        #region Porperties
        public List<Land> LandsList
        {
            get;
            set;
        }
        public TokenResponse Token { get; set; }
        public ObservableCollection<MenuItemViewModel> Menus
        {
            get;
            set;
        }

        public User User
        {
            get { return this.user; }
            set { SetValue(ref this.user, value); }
        }
        #endregion

        #region ViewModels
        public LoginViewModel Login
        {
            get;
            set;
        }
        public ChangePasswordViewModel ChangePassword
        {
            get;
            set;
        }

        public LandsViewModel Lands
        {
            get;
            set;
        }
        public LandViewModel Land
        {
            get;
            set;
        }

        public RegisterViewModel Register
        {
            get;
            set;
        }
        public MyProfileViewModel MyProfile
        {
            get;
            set;
        }

        #endregion

        #region Constructor
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
            this.LoadMenu();
        }
        #endregion

        #region Methods
        private void LoadMenu()
        {
            this.Menus = new ObservableCollection<MenuItemViewModel>();

            this.Menus.Add(new MenuItemViewModel
            { 
                Icon= "ic_settings",
                PageName= "MyProfilePage",
                Title=Languages.MyProfile
            });

            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_insert_chart",
                PageName = "StatisticsPage",
                Title = Languages.Statistics
            });

            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_exit_to_app",
                PageName = "LoginPage",
                Title = Languages.Logout
            });
        } 
        #endregion

        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if(instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
        #endregion
    }
}
