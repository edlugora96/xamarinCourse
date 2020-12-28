namespace XamarinTutorial.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;

    class LoginViewModel
    {
        #region Properties
        public string Email
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public bool isRemembered
        {
            get;
            set;
        }
        public bool IsRunnig
        {
            get;
            set;
        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get;
            set;
        }
        public ICommand RegisterCommand
        {
            get;
            set;
        }
        #endregion
        #region Constructors
        public LoginViewModel()
        {
            this.isRemembered = true;
        }
        #endregion
    }
}
