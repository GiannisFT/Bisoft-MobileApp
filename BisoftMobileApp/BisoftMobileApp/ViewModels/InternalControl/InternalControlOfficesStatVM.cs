using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using BisoftMobileApp.Classes.InternalControl;
using ServiceReference1;
using Xamarin.Forms;

namespace BisoftMobileApp.ViewModels.InternalControl
{
    class InternalControlOfficesStatVM: ViewModelBase
    {
        #region Properties

        #region ServiceReference
        public ServiceReference1.Service1Client Dbcontext { get; set; }
        #endregion

        #region Offices
        private ObservableCollection<InternalControlOfficeResult> allOffices;
        public ObservableCollection<InternalControlOfficeResult> AllOffices
        {
            get
            {
                return allOffices;
            }
            set
            {
                if (allOffices == value)
                    return;
                allOffices = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AllOffices"));
            }
        }
        #endregion

        #endregion

        #region Commands
        #endregion

        #region Constructors

        public InternalControlOfficesStatVM()
        {
            GetOffices();
        }

        #endregion

        #region Methods

        #region Get Offices
        public async void GetOffices()
        {
            try
            {
                IsBusy = true;
                InternalControlOfficeResult off;

                Dbcontext = new ServiceReference1.Service1Client(ServiceReference1.Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
                AllOffices = new ObservableCollection<InternalControlOfficeResult>();
                var result = Dbcontext.GetOfficeInternalControlsGoalVsResult(Application.Current.Properties["UN"].ToString(),
                    Application.Current.Properties["PW"].ToString(), Application.Current.Properties["Ucid"].ToString(),
                    Convert.ToInt32(Application.Current.Properties["CompanyId"].ToString()));

                foreach(InternalControlOfficeResultsData data in result)
                {
                    off = new InternalControlOfficeResult
                    {
                        Id = data.OfficeData.Id,
                        OfficeName = data.OfficeData.Name
                    };

                    AllOffices.Add(off);
                }

                IsBusy = false;
            }
            catch(Exception e)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Fel", e.Message, "Stäng");
            }


        }

        #endregion

        #endregion
    }
}
