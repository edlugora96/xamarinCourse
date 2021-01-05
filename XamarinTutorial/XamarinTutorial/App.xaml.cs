namespace XamarinTutorial
{
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using XamarinTutorial.Views;
    using XamarinTutorial.Helpers;
    using XamarinTutorial.ViewModels;
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
