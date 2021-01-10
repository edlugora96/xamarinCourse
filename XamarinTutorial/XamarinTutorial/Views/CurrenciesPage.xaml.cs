using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinTutorial.Helpers;

namespace XamarinTutorial.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrenciesPage : ToolbarPage
    {
        public CurrenciesPage()
        {
            InitializeComponent();
        }
    }
}