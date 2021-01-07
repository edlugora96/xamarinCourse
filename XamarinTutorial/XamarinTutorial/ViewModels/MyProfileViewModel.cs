﻿//using FFImageLoading.Work;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinTutorial.BaseViewModels;
using XamarinTutorial.Helpers;
using XamarinTutorial.Models;
using XamarinTutorial.Services;
using XamarinTutorial.Views;

namespace XamarinTutorial.ViewModels
{
    public class MyProfileViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

       #region Attributes
        private bool isRunning;
        private bool isEnabled;
        private ImageSource imageSource;
        private MediaFile file;
        #endregion

        #region Properties
        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { SetValue(ref this.imageSource, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public User User
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public MyProfileViewModel()
        {
            this.apiService = new ApiService();

            this.User = MainViewModel.GetInstance().User;
            this.ImageSource = User.ImageFullPath;
            this.IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand ChangePasswordCommand
        {
            get
            {
                return new RelayCommand(ChangePassword);
            }
        }

        private async void ChangePassword()
        {
            MainViewModel.GetInstance().ChangePassword = new ChangePasswordViewModel();
            await App.Navigator.PushAsync(new ChangePasswordPage());
        }

        public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }

        private async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();

            if (CrossMedia.Current.IsCameraAvailable &&
                CrossMedia.Current.IsTakePhotoSupported)
            {
                var source = await Application.Current.MainPage.DisplayActionSheet(
                    Languages.SourceImageQuestion,
                    Languages.Cancel,
                    null,
                    Languages.FromGallery,
                    Languages.FromCamera);

                if (source == Languages.Cancel)
                {
                    this.file = null;
                    return;
                }

                if (source == Languages.FromCamera)
                {
                    this.file = await CrossMedia.Current.TakePhotoAsync(
                        new StoreCameraMediaOptions
                        {
                            Directory = "Sample",
                            Name = "test.jpg",
                            PhotoSize = PhotoSize.Small,
                        }
                    );
                }
                else
                {
                    this.file = await CrossMedia.Current.PickPhotoAsync();
                }
            }
            else
            {
                this.file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (this.file != null)
            {
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(this.User.FirstName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.FirstNameValidation,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.User.LastName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.LastNameValidation,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.User.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation,
                    Languages.Accept);
                return;
            }

            if (!RegexUtilities.IsValidEmail(this.User.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation2,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.User.Telephone))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PhoneValidation,
                    Languages.Accept);
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var checkConnetion = await this.apiService.CheckConnection();
            if (!checkConnetion.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    checkConnetion.Message,
                    Languages.Accept);
                return;
            }
            this.User.ImageArray = null;
            if (this.file != null)
            {
                this.User.ImageArray = FilesHelper.ReadFully(this.file.GetStream());
            }

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var response = await this.apiService.PutUser(
                apiSecurity,
                this.User,
                MainViewModel.GetInstance().Token.TokenType,
                MainViewModel.GetInstance().Token.AccessToken);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            var userApi = await this.apiService.GetUserByEmail(
                apiSecurity,
                this.User.Email);

            MainViewModel.GetInstance().User = userApi;
            Settings.User = JsonConvert.SerializeObject(userApi);

            this.IsRunning = false;
            this.IsEnabled = true;

            await App.Navigator.PopAsync();
        }
        #endregion
    }
}
