using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinTutorial.Models;
using XamarinTutorial.Views;

namespace XamarinTutorial.ViewModels
{
    public class LandItemViewModel : Land
    {
        #region Commads
        public ICommand SelectLandCommand
        {
            get
            {
                return new RelayCommand(SelectLand);
            }
        }
        #endregion
        #region Methods
        private async void SelectLand()
        {
            MainViewModel.GetInstance().Land = new LandViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new LandPage());
        }
        #endregion
    }
}
