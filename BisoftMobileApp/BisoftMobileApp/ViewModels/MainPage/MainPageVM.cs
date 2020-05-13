using BisoftMobileApp.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BisoftMobileApp.ViewModels.MainPage
{
    class MainPageVM:ViewModelBase
    {
        #region Properties

       // private ObservableCollection<MenuItem> allMenuItems;
        public ObservableCollection<MenuItem> MyProperty { get; set; }

        #endregion
    }
}
