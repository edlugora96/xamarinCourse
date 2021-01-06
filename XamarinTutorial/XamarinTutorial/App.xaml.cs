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
        public static MasterPage Master { get; internal set; }
        #endregion

        #region Constructor
        public App()
        {
            InitializeComponent();
            var mainViewModel = MainViewModel.GetInstance();
            if (Settings.IsRemembered == "true")
            {
                mainViewModel.Token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
                var token = mainViewModel.Token;
                if (token != null && token.Expires > DateTime.Now)
                {
                    mainViewModel.User = JsonConvert.DeserializeObject<User>(Settings.User);
                    mainViewModel.Lands = new LandsViewModel();
                    MainPage = new MasterPage();
                }
                else
                {
                    Settings.IsRemembered = "false";
                    Settings.Token = string.Empty;
                    Settings.User = string.Empty;
                    mainViewModel.Token = null;
                    mainViewModel.User = null;
                    MainPage = new NavigationPage(new LoginPage());
                }
                
                
            }
            else
            {
                Settings.IsRemembered = "false";
                Settings.Token = string.Empty;
                Settings.User = string.Empty;
                mainViewModel.Token = null;
                mainViewModel.User = null;
                MainPage = new NavigationPage(new LoginPage());
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
