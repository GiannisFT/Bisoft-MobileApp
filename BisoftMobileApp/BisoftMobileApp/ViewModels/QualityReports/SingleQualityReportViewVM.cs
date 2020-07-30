using BisoftMobileApp.Classes;
using BisoftMobileApp.Classes.QualityReports;
using BisoftMobileApp.HelpClasses;
using BisoftMobileApp.ViewModels.EmployeeVMS;
using BisoftMobileApp.ViewModels.ImagesVMS;
using BisoftMobileApp.Views.EmployeeViews.ChooseEmployee;
using BisoftMobileApp.Views.Images;
using BisoftMobileApp.Views.QualityReport;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Net.Http;
using System.IO;
using System.Net;
using BisoftMobileApp.Classes.Photo;

namespace BisoftMobileApp.ViewModels.QualityReports
{
    class SingleQualityReportViewVM : ViewModelBase
    {
        #region ServiceReference
        public Service1Client DbContext { get; set; }
        #endregion

        #region Commands
        public ICommand SearchEmployeeCommand { get; set; }
        public ICommand CloseCommand => new Command( async () => await  Application.Current.MainPage.Navigation.PopAsync());
        public ICommand EditAnalysisHeaderCommand { get; set; }
        public ICommand EditFinalDecisionCommand { get; set; }
        public ICommand EditErrorDescriptionCommand { get; set; }
        public ICommand AddNewFileCommand { get; set; }
        #endregion

        #region Properties
        private int IniQRId { get; set; }

        #region Report Data
        private QualityReport _reportData;
        public QualityReport ReportData
        {
            get { return _reportData; }
            set
            {
                if (_reportData == value)
                    return;
                _reportData = value;
                {
                    OnPropertyChanged(new PropertyChangedEventArgs("ReportData"));
                }
            }
        }
        #endregion

        #region Selected File View
        private ImagePageVM ImagePageVM { get; set; }

        private ObservableCollection<QRAttachedFile> _allImages;
        public ObservableCollection<QRAttachedFile> AllImages
        {
            get { return _allImages; }
            set
            {
                if (_allImages == value)
                    return;
                _allImages = value;
                {
                    OnPropertyChanged(new PropertyChangedEventArgs("AllImages"));
                }
            }
        }

        private QRAttachedFile _selectedImage;
        public QRAttachedFile SelectedImage
        {
            get { return _selectedImage; }
            set
            {
                if (_selectedImage == value)
                    return;
                _selectedImage = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedImage"));
                if (SelectedImage != null)
                {
                    ImagePage imgpage = new ImagePage();
                    ImagePageVM = new ImagePageVM(SelectedImage.FilePath);
                    imgpage.BindingContext = ImagePageVM;
                    imgpage.Disappearing += Imagepage_Disappearing;
                    Application.Current.MainPage.Navigation.PushModalAsync(imgpage);
                }
            }
        }
        private void Imagepage_Disappearing(object sender, EventArgs e)
        {
            if (SelectedImage != null)
            {
                if (ImagePageVM.DeletePhoto == true)
                {
                    if (AllImages != null && AllImages.Count > 0 && AllImages.Contains(SelectedImage))
                    {
                        AllImages.Remove(SelectedImage);
                        UpdateUploadedFiles();
                        SelectedImage = null;
                    }
                }
            }
        }
        #endregion

        #region Offices
        private ObservableCollection<Office> _allOffices;
        public ObservableCollection<Office> AllOffices
        {
            get { return _allOffices; }
            set
            {
                if (_allOffices == value)
                    return;
                _allOffices = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AllOffices"));
            }
        }

        private Office _selectedOffice;
        public Office SelectedOffice
        {
            get { return _selectedOffice; }
            set
            {
                if (_selectedOffice == value)
                    return;
                _selectedOffice = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedOffice"));
                if (SelectedOffice != null)
                {
                    SelectedOfficeEmployees = new ObservableCollection<Employee>(SelectedOffice.Employees);
                }
            }
        }
        #endregion

        #region Employees
        public ChooseEmployeesVM searchEmpVM { get; set; }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                if (_selectedEmployee == value)
                    return;
                _selectedEmployee = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedEmployee"));
            }
        }
        private ObservableCollection<Employee> _selectedOfficeEmployees;
        public ObservableCollection<Employee> SelectedOfficeEmployees
        {
            get { return _selectedOfficeEmployees; }
            set
            {
                if (_selectedOfficeEmployees == value)
                    return;
                _selectedOfficeEmployees = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedOfficeEmployees"));
            }
        }
        #endregion

        #endregion

        #region Constructors
        public SingleQualityReportViewVM(int id)
        {
            IniQRId = id;
            GetReportData();
            SearchEmployeeCommand = new DelegateCommand(SearchEmployee, CanSearchEmployee);
            EditAnalysisHeaderCommand = new DelegateCommand(OpenEditAnalysisHeader, CanEditAnalysisHeader);
            EditFinalDecisionCommand = new DelegateCommand(OpenEditFinalDecision, CanEditFinalDecision);
            EditErrorDescriptionCommand = new DelegateCommand(OpenEditErrorDescription, CanEditErrorDescription);
            AddNewFileCommand = new DelegateCommand(UploadFile, CanUploadFile);
        }
        public SingleQualityReportViewVM()
        {

        }
        #endregion

        #region Methods

        #region Get Report Data
        private void GetReportData()
        {
            DbContext = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
            var data = DbContext.GetQualityReportById(Application.Current.Properties["UN"].ToString(),
                                Application.Current.Properties["PW"].ToString(), Application.Current.Properties["Ucid"].ToString(), IniQRId);

            ReportData = new QualityReport();
            QualityReport qualityReport = new QualityReport();

            qualityReport.Id = data.Id;
            qualityReport.ReportNr = data.ReportNr;
            qualityReport.DateCreated = data.Created;
            qualityReport.Status = data.Status;

            TimeSpan span = DateTime.Now - data.Created;
            qualityReport.Interval = span.Days + " dag " + span.Hours + " tim";

            if (data.CreatedByEmployee != null)
            {
                Employee createdBy = new Employee();
                createdBy.Id = data.CreatedByEmployee.Id;
                createdBy.Name = data.CreatedByEmployee.FName + " " + data.CreatedByEmployee.LName;
                qualityReport.CreatedByEmployee = createdBy;
            }

            if (data.OfficeData != null)
            {
                Office off = new Office();
                off.Id = data.OfficeData.Id;
                off.Name = data.OfficeData.Name;
                qualityReport.Office = off;
            }

            //"Felbeskrivning" the following 3 properties
            qualityReport.Department = data.OfficeDepartment;
            qualityReport.DepartmentTask = data.OfficeDepartmentTask;
            qualityReport.OfficeDepartmentTaskId = data.OfficeDepartmentTaskId;
            qualityReport.Description = data.Description;

            //"Trolig orsak" the following 3 properties
            qualityReport.QRAnalysisHeaderId = data.QRAnalysisHeaderId;
            qualityReport.QRAnalysisHeader = data.QRAnalysisHeader;
            qualityReport.AnalysisText = data.AnalysisText;
            qualityReport.AnalysisCausedById = data.AnalysisCausedById;
            if (data.CausedByEmployees != null)
            {
                qualityReport.CausedByEmployees = new ObservableCollection<Employee>();
                Employee causedBy;
                foreach (var emp in data.CausedByEmployees)
                {
                    causedBy = new Employee();
                    causedBy.Id = emp.Id;
                    causedBy.Name = emp.FName + " " + emp.LName;
                    qualityReport.CausedByEmployees.Add(causedBy);
                }
            }

            //"Slutlig åtgärd" the following 4 properties
            qualityReport.QRFinalDecisionHeader = data.QRFinalDecisionHeader;
            qualityReport.FinalDecisionText = data.FinalDecisionText;
            qualityReport.FinalDecisionCost = data.FinalDecisionCost;
            qualityReport.IsRepeatRepair = data.IsRepeatRepair;

            if (data.ResponsibleEmployee != null)
            {
                Employee responsible = new Employee();
                responsible.Id = data.ResponsibleEmployee.Id;
                responsible.Name = data.ResponsibleEmployee.FName + " " + data.ResponsibleEmployee.LName;
                qualityReport.ResponsibleEmployee = responsible;
            }

            if (data.QRAttachedFileData != null)
            {
                QRAttachedFile attachedFile;
                AllImages = new ObservableCollection<QRAttachedFile>();
                foreach (var img in data.QRAttachedFileData)
                {
                    attachedFile = new QRAttachedFile();
                    attachedFile.Id = img.Id;
                    attachedFile.FileName = img.FileName;
                    attachedFile.FilePath = img.FilePath;

                    AllImages.Add(attachedFile);
                }
            }
            
            qualityReport.AoNr = data.AoNr;
            qualityReport.RegNr = data.RegNr;
            qualityReport.TotalCost = data.TotalCost;
            qualityReport.Year = data.Year;
            qualityReport.Deleted = data.Deleted;

            if (data.QRLogs != null)
            {
                qualityReport.QRLog = new ObservableCollection<QRLog>();
                QRLog qrlog;
                foreach (var log in data.QRLogs.OrderBy(o => o.Id))
                {
                    qrlog = new QRLog();
                    qrlog.QRId = log.QualityReportId;
                    qrlog.Id = log.Id;
                    qrlog.LogTypeId = log.LogTypeId;
                    qrlog.LogTypeText = log.LogTypeText;
                    qrlog.Subject = log.Subject;
                    qrlog.Created = log.Created;
                    qrlog.Description = log.Description;
                    if (log.CreatedEmployee != null)
                    {
                        Employee emp = new Employee();
                        emp.Id = log.CreatedEmployee.Id;
                        emp.Name = log.CreatedEmployee.FName + " " + log.CreatedEmployee.LName;
                        qrlog.CreatedByEmployee = emp;
                    }
                    qualityReport.QRLog.Add(qrlog);
                }
            }

            ReportData = qualityReport;
        }
        #endregion

        #region Get Offices And Employees
        public async void GetEmployees()
        {
            try
            {
                DbContext = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
                var result = DbContext.GetOfficesAndEmployeesByCompanyId(Application.Current.Properties["UN"].ToString(),
                    Application.Current.Properties["PW"].ToString(), Application.Current.Properties["Ucid"].ToString(),
                    Convert.ToInt32(Application.Current.Properties["CompanyId"].ToString()));
                AllOffices = new ObservableCollection<Office>();
                Office off;
                Employee emp;

                if (result != null)
                {
                    if (result.Length > 0)
                    {
                        foreach (var data in result.OrderBy(o => o.Name))
                        {
                            off = new Office();
                            off.Id = data.Id;
                            off.Name = data.Name;

                            off.Employees = new ObservableCollection<Employee>();

                            if (data.Employees != null && data.Employees.Count() > 0)
                            {
                                foreach (var e in data.Employees.OrderBy(em => em.LName))
                                {
                                    emp = new Employee();
                                    emp.Id = e.Id;
                                    emp.Name = e.FName + " " + e.LName;

                                    off.Employees.Add(emp);
                                }
                            }
                            AllOffices.Add(off);
                        }
                    }
                }
                //SelectedOffice = AllOffices.Where(of => of.Id == IniOfficeId).FirstOrDefault();
                //SelectedEmployee = AllOffices.SelectMany(en => en.Employees).Where(em => em.Id == IniEmployeeId).FirstOrDefault();
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Fel", e.Message, "STÄNG");
            }
        }
        #endregion

        #region  Open Search Employee
        private void SearchEmployee(object obj)
        {
            GetEmployees();

            ChooseEmployeePage page = new ChooseEmployeePage();
            searchEmpVM = new ChooseEmployeesVM(AllOffices);
            page.BindingContext = searchEmpVM;
            page.Disappearing += Page_Disappearing;
            page.Disappearing += Page_Refresh;
            Application.Current.MainPage.Navigation.PushAsync(page);
        }
        private bool CanSearchEmployee(object param)
        {
            return true;
        }
        private void Page_Disappearing(object sender, EventArgs e)
        {
            if (searchEmpVM.ReturnResult)
            {
                SelectedOffice = searchEmpVM.SelectedOffice;
                SelectedEmployee = searchEmpVM.SelectedEmployee;
                SaveResponsibleEmp();
            }
        }
        #endregion

        #region Edit Save Responsible Employee
        private void SaveResponsibleEmp()
        {
            try
            {
                DbContext = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);

                var result = DbContext.UpdateQRResponsible(Application.Current.Properties["UN"].ToString(),
                    Application.Current.Properties["PW"].ToString(), Application.Current.Properties["Ucid"].ToString(),
                    ReportData.Id, SelectedOffice.Id, SelectedEmployee.Id, ReportData.CreatedByEmployee.Id);
            }
            catch (Exception e)
            {
                Application.Current.MainPage.DisplayAlert("Fel", e.Message, "STÄNG");
            }
        }
        #endregion

        #region Edit Error Description
        private void OpenEditErrorDescription(object param)
        {
            QRErrorDescriptionVM context = new QRErrorDescriptionVM(IniQRId, ReportData.CreatedByEmployee.Id,
                ReportData.RegNr, ReportData.AoNr, ReportData.Department, ReportData.OfficeDepartmentTaskId, ReportData.Description);
            ErrorDescriptionPage descriptionPage = new ErrorDescriptionPage();
            descriptionPage.BindingContext = context;
            descriptionPage.Disappearing += Page_Refresh;
            Application.Current.MainPage.Navigation.PushAsync(descriptionPage);
        }
        private bool CanEditErrorDescription(object param)
        {
            return true;
        }
        #endregion

        #region Edit Analysis Header
        private void OpenEditAnalysisHeader(object param)
        {
            QRAnalysisHeadersVM context = new QRAnalysisHeadersVM(IniQRId, ReportData.CreatedByEmployee.Id,
                ReportData.QRAnalysisHeaderId, ReportData.AnalysisText, ReportData.CausedByEmployees);
            AnalysisHeadersPage headersPage = new AnalysisHeadersPage();
            headersPage.BindingContext = context;
            headersPage.Disappearing += Page_Refresh;
            Application.Current.MainPage.Navigation.PushAsync(headersPage);
        }
        private bool CanEditAnalysisHeader(object param)
        {
            return true;
        }
        #endregion

        #region Edit Final Decision
        private void OpenEditFinalDecision(object param)
        {
            QRFinalDecisionVM context = new QRFinalDecisionVM(IniQRId, ReportData.CreatedByEmployee.Id,
                ReportData.QRFinalDecisionHeader, ReportData.FinalDecisionText, ReportData.FinalDecisionCost, ReportData.IsRepeatRepair);
            FinalDecisionPage finalDPage = new FinalDecisionPage();
            finalDPage.BindingContext = context;
            finalDPage.Disappearing += Page_Refresh;
            Application.Current.MainPage.Navigation.PushAsync(finalDPage);
        }
        private bool CanEditFinalDecision(object param)
        {
            return true;
        }
        #endregion

        #region Upload File
        private void UpdateUploadedFiles()
        {
            try
            {
                DbContext = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);

                QRAttachedFileData[] data = new QRAttachedFileData[0];
                if (AllImages != null && AllImages.Count > 0)
                {
                    data = new QRAttachedFileData[AllImages.Count];
                    QRAttachedFileData filedata;
                    int i = 0;
                    foreach (QRAttachedFile photo in AllImages)
                    {
                        filedata = new QRAttachedFileData
                        {
                            FileName = photo.FileName,
                            FilePath = photo.FilePath,
                            Id = photo.Id
                        };
                        data[i] = filedata;
                        i++;
                    }
                }
                var result = DbContext.UpdateQRAttatchedFiles(Application.Current.Properties["UN"].ToString(),
                    Application.Current.Properties["PW"].ToString(), Application.Current.Properties["Ucid"].ToString(), IniQRId, data);

                Application.Current.MainPage.DisplayAlert("Meddelande", result.Message, "STÄNG");
            }
            catch (Exception e)
            {
                Application.Current.MainPage.DisplayAlert("Fel", e.Message, "STÄNG");
            }
        }
        public async void UploadFile(object param)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("File Not Supported.", "Permission not granted to files.", "STÄNG");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Large,
            });

            if (file != null)
            {
                string[] temp = file.Path.Split('/');
                string[] tempName = temp[temp.Length - 1].Split('.');
                string filename = tempName[0];
                string foldername = DateTime.Now.ToString("yyyy-MM-dd") + "/" + DateTime.Now.ToString("H-mm-ss");
                string filePath = "Files/NyKvalitetsRapport/" + Application.Current.Properties["CompanyId"].ToString() + "/" + Application.Current.Properties["OfficeId"].ToString() + "/" + foldername + "/" + temp[temp.Length - 1];

                var content = new MultipartFormDataContent();
                Uri host = new Uri("http://www.bisoft.se/Bisoft/receiver.ashx");
                UriBuilder ub = new UriBuilder(host)
                {
                    Query = string.Format("filename={0}", filePath)
                };

                Stream data = file.GetStream();

                WebClient c = new WebClient();
                c.OpenWriteCompleted += (sender, e) =>
                {
                    PushData(data, e.Result);
                    e.Result.Close();
                    data.Close();

                    if (AllImages == null)
                        AllImages = new ObservableCollection<QRAttachedFile>();

                    QRAttachedFile phfile = new QRAttachedFile();
                    phfile.FileName = filename;
                    phfile.FilePath = filePath;
                    phfile.Id = 0;

                    AllImages.Add(phfile);
                    UpdateUploadedFiles();
                };
                c.OpenWriteAsync(ub.Uri);
                
            }
            else
                await Application.Current.MainPage.DisplayAlert("File not supported.", "Ingen fil vald.", "STÄNG");
        }
        private void PushData(Stream input, Stream output)
        {
            byte[] buffer = new byte[4096];
            int bytesRead;
            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) != 0)
            {
                output.Write(buffer, 0, bytesRead);
            }
        }
        private bool CanUploadFile(object param)
        {
            return true;
        }
        #endregion

        #region Page Refresh
        private void Page_Refresh(object sender, EventArgs e)
        {
            GetReportData();
        }
        #endregion

        #endregion
    }
}
