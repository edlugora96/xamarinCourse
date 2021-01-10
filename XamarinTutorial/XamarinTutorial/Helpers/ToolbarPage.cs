using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using System;

namespace XamarinTutorial.Helpers
{
    public class ToolbarPage : ContentPage
    {
        public ICommand TapToolbarCommand
        {
            get
            {
                return new RelayCommand(TapToolbar);
            }
        }

        private async void TapToolbar()
        {
            var options = await Application.Current.MainPage.DisplayActionSheet(
                    null,
                    null,
                    null,
                    Languages.ShareApp,
                    Languages.RateApp,
                    Languages.About);
            var rateLink = Application.Current.Resources["RateLink"].ToString();
            var shareLink = Application.Current.Resources["ShareLink"].ToString();
            if (options == Languages.ShareApp)
            {
                await Share.RequestAsync(new ShareTextRequest
                {
                    Uri = shareLink
                });
                return;
            }

            if (options == Languages.RateApp)
            {
                try
                {
                    await Browser.OpenAsync(rateLink, BrowserLaunchMode.SystemPreferred);
                }
                catch
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.SomethingWrong, Languages.Accept);
                }
                return;
            }

            if (options == Languages.About)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Menu, Languages.About, Languages.Accept);
                return;
            }

            return;


        }

        public ToolbarPage()
        {
            ToolbarItem item = new ToolbarItem
            {
                IconImageSource = ImageSource.FromFile("ic_more_vert.png"),
                Order = ToolbarItemOrder.Default,
                Priority = 0,
                Command = TapToolbarCommand,
                
            };

            this.ToolbarItems.Add(item);
        }

    }
}
