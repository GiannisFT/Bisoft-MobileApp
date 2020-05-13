using BisoftMobileApp.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace BisoftMobileApp.ViewModels.AppShellVMS
{
    class AppShellVM:ViewModelBase
    {
        #region Properties

        #region Menu

        private ObservableCollection<MenuItem> allmenuItems;
        public ObservableCollection<MenuItem> AllmenuItems
        {
            get
            {
                return allmenuItems;
            }
            set
            {
                if (allmenuItems == value)
                    return;
                allmenuItems = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AllmenuItems"));
            }
        }
        #endregion

        #endregion

        #region Contructors

        public AppShellVM()
        {
            AllmenuItems = new ObservableCollection<MenuItem>();

            MenuItem item = new MenuItem();
            item.Name = "Kontrollera";
            AllmenuItems.Add(item);

            item = new MenuItem();
            item.Name = "Verktyg";
            AllmenuItems.Add(item);

        }
        #endregion
    }
}
