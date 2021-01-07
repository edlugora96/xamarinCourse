namespace XamarinTutorial.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using XamarinTutorial.BaseViewModels;
    using System.Windows.Input;
    using Xamarin.Forms;
    using XamarinTutorial.Views;
    using XamarinTutorial.Services;
    using Helpers;
    using XamarinTutorial.Models;
    using System;
    using Newtonsoft.Json;

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
        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(Register);
            }
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.IsRemembered = true;
            this.IsEnabled = true;
        }
        #endregion

        #region Methods
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

            if (!connection.IsSuccess)
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
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();

            var token = await this.apiService.GetToken(
                apiSecurity,
                this.Email,
                this.Password);

            if (token == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.SomethingWrong,
                    Languages.Accept);
                this.Password = string.Empty;
                this.IsRunnig = false;
                this.IsEnabled = true;
                return;
            }

            if (string.IsNullOrEmpty(token.AccessToken))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.LoginError,
                    Languages.Accept);
                this.Password = string.Empty;
                this.IsRunnig = false;
                this.IsEnabled = true;
                return;
            }

            var user = await this.apiService.GetUserByEmail(apiSecurity, this.Email);

            user.Password = this.Password;
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Lands = new LandsViewModel();
            mainViewModel.User = user;
            mainViewModel.Token = token;

            Settings.IsRemembered = "false";
            if (IsRemembered)
            {
                Settings.IsRemembered = "true";
                Settings.Token = JsonConvert.SerializeObject(mainViewModel.Token);
                Settings.User = JsonConvert.SerializeObject(user);
            }

            this.IsRunnig = false;
            this.IsEnabled = true;

            this.Email = string.Empty;
            this.Password = string.Empty;

            Application.Current.MainPage = new MasterPage();
        }

        private async void Register()
        {
            MainViewModel.GetInstance().Register = new RegisterViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        } 
        #endregion
    }
}
