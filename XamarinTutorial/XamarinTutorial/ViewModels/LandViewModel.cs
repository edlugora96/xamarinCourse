using System;
using System.Collections.Generic;
using System.Text;
using XamarinTutorial.Models;

namespace XamarinTutorial.ViewModels
{
    public class LandViewModel
    {
        #region Properties
        public Land Land
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public LandViewModel(Land land)
        {
            this.Land = land;
        } 
        #endregion
    }
}
