using System;
using System.Collections.Generic;
using System.Text;
using ServiceReference1;
using Xamarin.Forms;
using System.Linq;
using System.Windows.Input;
using BisoftMobileApp.HelpClasses;
using BisoftMobileApp.Classes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using BisoftMobileApp.Classes.QualityReports;
using BisoftMobileApp.Views.QualityReport;

namespace BisoftMobileApp.ViewModels.QualityReports
{
    class QualityReportsVM : ViewModelBase
    {
        #region ServiceReference
        public Service1Client DbContext { get; set; }
        #endregion

        #region Commands
        public ICommand OpenNewQRCommand { get; set; } 
        #endregion

        #region Properties

        #region Active Quality Reports
        private ObservableCollection<QualityReport> _activeReports;
        public ObservableCollection<QualityReport> ActiveReports
        {
            get { return _activeReports; }
            set
            {
                if (_activeReports == value)
                    return;
                _activeReports = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ActiveReports"));
            }
        }
        private QualityReport _selectedReport;
        public QualityReport SelectedReport
        {
            get { return _selectedReport; }
            set
            {
                if (_selectedReport == value)
                    return;
                _selectedReport = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedReport"));
                SelectedQRView();
            }
        }
        private int _reportscounter;
        public int ReportsCounter
        {
            get { return _reportscounter; }
            set
            {
                if (_reportscounter == value)
                    return;
                _reportscounter = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ReportsCounter"));
            }
        }
        #endregion

        #endregion

        #region Constructors
        public QualityReportsVM()
        {
            GetAciveQualityReports();
            OpenNewQRCommand = new DelegateCommand(OpenNewQR, CanOpenNewQR);
        }
        #endregion

        #region Methods

        #region Get Quality Reports
        private void GetAciveQualityReports()
        {
            try
            {
                DbContext = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
                var result = DbContext.GetMyActiveQualityReports(Application.Current.Properties["UN"].ToString(),
                                Application.Current.Properties["PW"].ToString(), Application.Current.Properties["Ucid"].ToString(),
                                Convert.ToInt32(Application.Current.Properties["EmpId"]));

                ObservableCollection<QualityReport> temp = new ObservableCollection<QualityReport>();

                foreach (QualityReportRowData row in result)
                {
                    QualityReport qualityReport = new QualityReport();
                    qualityReport.Id = row.Id;
                    qualityReport.ReportNr = row.ReportNr;
                    qualityReport.ResponsibleName = row.ResponsibleName;
                    qualityReport.Department = row.Department;
                    qualityReport.DepartmentTask = row.DepartmentTask;
                    qualityReport.OfficeName = row.OfficeName;
                    qualityReport.Year = row.Year;
                    qualityReport.OfficeAddress = row.OfficeAddress;
                    qualityReport.NextMeasure = row.NextMeasure;
                    qualityReport.NextMeasureDate = row.NextMeasureDatetime;
                    qualityReport.Interval = Interval(row.NextMeasureDatetime);
                    
                    temp.Add(qualityReport);
                }
                ActiveReports = new ObservableCollection<QualityReport>(temp);
                ReportsCounter = ActiveReports.Count();
            }
            catch (Exception e)
            {
                Application.Current.MainPage.DisplayAlert("Fel", e.Message, "STÄNG");
            }
        }
        #endregion

        #region TimeSpan
        private string Interval(DateTime nextDate)
        {
            TimeSpan interval = nextDate - DateTime.Now;
            if (interval.Days == 0)
            {
                return interval.Hours.ToString() + " tim.";
            }
            else
                return interval.Days.ToString() + " dag";
        }
        #endregion

        #region Selected Quality Report
        private void SelectedQRView()
        {
            SingleQualityReportViewVM contextQR = new SingleQualityReportViewVM(SelectedReport.Id);
            SingleQualityReportViewPage singleQRPage = new SingleQualityReportViewPage();
            singleQRPage.BindingContext = contextQR;
            singleQRPage.Disappearing += Page_Refresh;
            Application.Current.MainPage.Navigation.PushAsync(singleQRPage);
        }
        #endregion

        #region Open New Quality Report
        private void OpenNewQR(object param)
        {
            //IsBusy = true;
            NewQualityReportPage newQR = new NewQualityReportPage();
            newQR.Disappearing += Page_Refresh;
            Application.Current.MainPage.Navigation.PushAsync(newQR);
            //IsBusy = false;
        }
        private bool CanOpenNewQR(object param)
        {
            return true;
        }
        #endregion

        #region Page Refresh
        private void Page_Refresh(object sender, EventArgs e)
        {
            GetAciveQualityReports();
        }
        #endregion

        #endregion
    }
}
