namespace XamarinTutorial
{
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using XamarinTutorial.Views;
    using XamarinTutorial.Helpers;
    using XamarinTutorial.ViewModels;
    using XamarinTutorial.Models;
    using Newtonsoft.Json;

    public partial class App : Application
    {
        #region Properties
        public static NavigationPage Navigator { get; internal set; }
        #endregion

        #region Constructor
        public App()
        {
            InitializeComponent();

            if(string.IsNullOrEmpty(Settings.Token))
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Token = Settings.Token;
                mainViewModel.TokenType = Settings.TokenType;
                mainViewModel.User = JsonConvert.DeserializeObject<User>(Settings.User);
                mainViewModel.Lands = new LandsViewModel();
                MainPage = new MasterPage();
            }
        }
        #endregion

        #region Methods
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        } 
        #endregion
    }
}
