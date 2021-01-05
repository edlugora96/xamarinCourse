﻿using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinTutorial.Views;

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
            if(this.PageName == "LoginPage")
            {
                Application.Current.MainPage = new LoginPage();
            }
        } 
        #endregion
    }
}
