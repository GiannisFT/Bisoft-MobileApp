using BisoftMobileApp.Classes;
using BisoftMobileApp.Classes.CarPreSales;
using BisoftMobileApp.ViewModels.MaintenanceType;
using BisoftMobileApp.Views.CarPreSalesMaintenance;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BisoftMobileApp.ViewModels.CarPreSalesMaintenance.Logs
{
    class MaintenanceLogsVM : ViewModelBase
    {
        #region ServiceReference
        public Service1Client DbContext { get; set; }
        #endregion

        #region Commands

        #endregion

        #region Properties
        public int IniId { get; set; }

        #region Logs
        private ObservableCollection<PreSalesMaintenanceLog> allLogs;
        public ObservableCollection<PreSalesMaintenanceLog> AllLogs
        {
            get { return allLogs; }
            set
            {
                if (allLogs == value)
                    return;
                allLogs = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AllLogs"));
            }
        }
        private PreSalesMaintenanceLog _selectedLog;
        public PreSalesMaintenanceLog SelectedLog
        {
            get { return _selectedLog; }
            set
            {
                if (_selectedLog == value)
                    return;
                _selectedLog = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedLog"));
                OpenLog();
            }
        }
        #endregion

        #endregion

        #region Constructors
        public MaintenanceLogsVM(int id)
        {
            IniId = id;
            GetLogs();
        }
        public MaintenanceLogsVM()
        {

        }
        #endregion

        #region Methods

        #region Get Logs
        private void GetLogs()
        {
            DbContext = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
            var result = DbContext.GetCarPreSalesMaintenanceLog(Application.Current.Properties["UN"].ToString(), Application.Current.Properties["PW"].ToString(),
                    Application.Current.Properties["Ucid"].ToString(), IniId);
            Employee emp;
            ObservableCollection<PreSalesMaintenanceLog> temp = new ObservableCollection<PreSalesMaintenanceLog>();
            foreach (CarPreSalesLogData row in result)
            {
                PreSalesMaintenanceLog psmLog = new PreSalesMaintenanceLog();
                psmLog.BegId = row.CarPreSaleMaintenanceBegId;
                psmLog.LagerId = row.CarPreSaleMaintenanceLagerId;
                psmLog.DateCreated = row.Created;
                psmLog.Description = row.Description;
                psmLog.DocName = row.DocName;
                psmLog.DocPath = row.DocPath;
                psmLog.Header = row.Header;
                psmLog.Id = row.Id;
                psmLog.IsMaintenance = row.IsMaintenance;
                psmLog.PreSalesId = row.CarPreSalesId;

                if(row.CreatedBy != null)
                {
                    emp = new Employee();
                    emp.Id = row.CreatedBy.Id;
                    emp.Name = row.CreatedBy.FName + " " + row.CreatedBy.LName;
                    psmLog.Employee = emp;
                }

                temp.Add(psmLog);
            }
            AllLogs = new ObservableCollection<PreSalesMaintenanceLog>(temp.OrderBy(o => o.DateCreated));
        }
        #endregion

        #region Open Selected Log
        private void OpenLog()
        {
            if (SelectedLog.BegId != null)
            {
                MaintenanceBegPage begPage = new MaintenanceBegPage();
                MaintenanceBegVM context = new MaintenanceBegVM((int)SelectedLog.BegId);
                begPage.BindingContext = context;

                Application.Current.MainPage.Navigation.PushAsync(begPage);
            }
            else if (SelectedLog.LagerId != null)
            {
                MaintenanceLagerPage lagerPage = new MaintenanceLagerPage();
                MaintenanceLagerVM context = new MaintenanceLagerVM((int)SelectedLog.LagerId);
                lagerPage.BindingContext = context;

                Application.Current.MainPage.Navigation.PushAsync(lagerPage);
            }
            else
            {
                MaintenanceStandardPage standPage = new MaintenanceStandardPage();
                MaintenanceStandardVM context = new MaintenanceStandardVM(SelectedLog);
                standPage.BindingContext = context;

                Application.Current.MainPage.Navigation.PushAsync(standPage);
            }
        }
        #endregion

        #endregion
    }
}
