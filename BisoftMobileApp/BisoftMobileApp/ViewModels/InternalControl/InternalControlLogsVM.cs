using BisoftMobileApp.Classes;
using BisoftMobileApp.Classes.InternalControl;
using BisoftMobileApp.HelpClasses;
using BisoftMobileApp.Views.InternalControl.PerfomeControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BisoftMobileApp.ViewModels.InternalControl
{
    class InternalControlLogsVM : ViewModelBase
    {    
        #region Properties

        #region ServiceReference
        public ServiceReference1.Service1Client Dbcontext { get; set; }
        #endregion

        #region Command
        //public ICommand PerformControlCommand { get; set; }

        //public ICommand PerformControlCarucelCommand { get; set; }

        //public ICommand OpenLogCommand { get; set; }

        #endregion

        #region Internal Control Id

        public int IniControlId { get; set; }

        private string _internalControlName;
        public string InternalControlName
        {
            get
            {
                return _internalControlName;
            }
            set
            {
                if (_internalControlName == value)
                    return;
                _internalControlName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("InternalControlName"));
            }
        }

        #endregion

        #region Ini Office Id

        public int IniOfficeId { get; set; }
        #endregion

        #region Internal Control Logs

        private ObservableCollection<InternalControlLogsCls> _allLogs;
        public ObservableCollection<InternalControlLogsCls> AllLogs
        {
            get
            {
                return _allLogs;
            }
            set
            {
                if (_allLogs == value)
                    return;
                _allLogs = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AllLogs"));
            }
        }

        private InternalControlLogsCls _selectedLog;
        public InternalControlLogsCls SelectedLog
        {
            get
            {
                return _selectedLog;
            }
            set
            {
                if (_selectedLog == value)
                    return;
                _selectedLog = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedLog"));
                if(_selectedLog != null)
                { 
                    PerformControlCaruCP page = new PerformControlCaruCP();
                    PerformedControlVM context = new PerformedControlVM(_selectedLog.Id, _selectedLog.IsPartSaved, IniOfficeId);
                    page.BindingContext = context;
                    page.Disappearing += Page_Disappearing;
                    SelectedLog = null;

                    Application.Current.MainPage.Navigation.PushAsync(page);
                }
            }
        }

        private void Page_Disappearing(object sender, EventArgs e)
        {
            GetControlLogs();
        }
        #endregion

        #endregion

        #region Constructors

        public InternalControlLogsVM(int controlId, int officeId)
        {
            IsBusy = false;
            IniOfficeId = officeId;
            IniControlId = controlId;
            GetControlLogs();

        }

        #endregion

        #region Methods  

        #region GetControlLogs

        public async void GetControlLogs()
        {
            try
            {
                IsBusy = true;

                if (Dbcontext == null)
                    Dbcontext = new ServiceReference1.Service1Client(ServiceReference1.Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);

                var result = Dbcontext.GetOfficeInternalControlLogs(Application.Current.Properties["UN"].ToString(),
                        Application.Current.Properties["PW"].ToString(), Application.Current.Properties["Ucid"].ToString(), IniControlId);
                ObservableCollection<InternalControlLogsCls> temp = new ObservableCollection<InternalControlLogsCls>();
                InternalControlLogsCls log;

                if (result != null)
                {
                    InternalControlName = result.Name;

                    if (result.Logs != null && result.Logs.Length > 0)
                    {
                        foreach (ServiceReference1.InternalControlOfficeLogData logdata in result.Logs)
                        {
                            log = new InternalControlLogsCls();
                            if (logdata.Id != null)
                                log.Id = (int)logdata.Id;
                            log.AoNr = logdata.AoNr;
                            log.Created = logdata.Created;
                            if (logdata.Created != null)
                                log.CreatedString = logdata.Created.ToString("yyyy-MM-dd H:mm");
                            if (logdata.Employee != null)
                                log.CreatedByName = logdata.Employee.FName + " " + logdata.Employee.LName;
                            if (!string.IsNullOrWhiteSpace(logdata.RegNr))
                                log.RegNr = logdata.RegNr.ToUpper();
                            if (logdata.VehicleBrand != null)
                                log.VehicleBrand = logdata.VehicleBrand.Name;

                            if (logdata.IsPartSaved == true)
                            {
                                log.Icon = "warning.png";
                                log.IsPartSaved = true;
                            }
                            else
                            {
                                log.Icon = "info.png";
                                log.IsPartSaved = false;
                            }
                            temp.Add(log);
                        }

                        AllLogs = new ObservableCollection<InternalControlLogsCls>(temp.OrderByDescending(ro => ro.Created));
                    }
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
