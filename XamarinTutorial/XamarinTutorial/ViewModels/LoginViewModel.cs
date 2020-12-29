namespace XamarinTutorial.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Lands.ViewModels;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {

        #region Attributes
        private string password;
        private bool isRunnig;
        private bool isEnabled;
        #endregion

        #region Properties
        public string Email
        {
            get;
            set;
        }
        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }
        public bool IsRemembered
        {
            get;
            set;
        }
        public bool IsRunnig
        {
            get { return this.isRunnig; }
            set { SetValue(ref this.isRunnig, value); }
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }


        private async void Login()
        {
            this.IsRunnig = true;
            this.IsEnabled = false;

            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an Email",
                    "OK");
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an Password",
                    "OK");
                this.IsRunnig = false;
                this.IsEnabled = true;
                return;
            }

            if (this.Email != "edl" || this.Password != "123")
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Login Error",
                    "OK");
                this.Password = string.Empty;
                this.IsRunnig = false;
                this.IsEnabled = true;
                return;
            }

            this.IsRunnig = false;
            this.IsEnabled = true;

            await Application.Current.MainPage.DisplayAlert(
                "OK",
                "You are in",
                "Accept");
            

        }

        public ICommand RegisterCommand
        {
            get;
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.IsRemembered = true;
            this.IsEnabled = true;
        }
        #endregion
    }
}
