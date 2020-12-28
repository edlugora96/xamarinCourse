namespace XamarinTutorial.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    class MainViewModel
    {
        #region Views
        public LoginViewModel Login
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            this.Login = new LoginViewModel();
        }
        #endregion
    }
}
