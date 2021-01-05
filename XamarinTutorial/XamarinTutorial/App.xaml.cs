namespace XamarinTutorial
{
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using XamarinTutorial.Views;
    public partial class App : Application
    {
        #region Properties
        public static NavigationPage Navigator { get; internal set; }
        #endregion

        #region Constructor
        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
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
