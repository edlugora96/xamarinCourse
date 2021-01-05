namespace XamarinTutorial.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using XamarinTutorial.BaseViewModels;
    using System.Windows.Input;
    using Xamarin.Forms;
    using XamarinTutorial.Views;
    using XamarinTutorial.Services;
    using Helpers;

    public class LoginViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private string email;
        private string password;
        private bool isRunnig;
        private bool isEnabled;
        #endregion

        #region Properties
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
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
                    Languages.Error,
                    Languages.EmailValidation,
                    Languages.Accept);
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordValidation,
                    Languages.Accept);
                this.IsRunnig = false;
                this.IsEnabled = true;
                return;
            }

            var connection = await this.apiService.CheckConnection();

            if(!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                this.Password = string.Empty;
                this.IsRunnig = false;
                this.IsEnabled = true;
                return;
            }

            if (this.Email != "edl" || this.Password != "123")
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "Login Error",
                    Languages.Accept);
                this.Password = string.Empty;
                this.IsRunnig = false;
                this.IsEnabled = true;
                return;
            }

            this.IsRunnig = false;
            this.IsEnabled = true;

            this.Email = string.Empty;
            this.Password = string.Empty;

            MainViewModel.GetInstance().Lands = new LandsViewModel();

            Application.Current.MainPage = new MasterPage();
        }

        public ICommand RegisterCommand
        {
            get;
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.IsRemembered = true;
            this.IsEnabled = true;
            this.Email = "edl";
            this.Password = "123";
        }
        #endregion
    }
}
