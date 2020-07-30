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
using BisoftMobileApp.ViewModels.EmployeeVMS;
using BisoftMobileApp.Views.EmployeeViews.ChooseEmployee;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Net.Http;
using System.IO;
using System.Net;
using BisoftMobileApp.Classes.Photo;

namespace BisoftMobileApp.ViewModels.QualityReports
{
    class NewQualityReportVM : ViewModelBase
    {
        #region ServiceReference
        public Service1Client DbContext { get; set; }
        #endregion

        #region Commands
        public ICommand SaveQRCommand { get; set; }
        public ICommand CancelCommand => new Command(async () => await Application.Current.MainPage.Navigation.PopAsync());
        public ICommand SearchEmployeeCommand { get; set; }
        public ICommand UploadFileCommand { get; set; }
        #endregion

        #region Properties

        #region Information
        private string _txtInfo;
        public string Text_Info
        {
            get { return _txtInfo; }
            set
            {
                if (_txtInfo == value)
                    return;
                _txtInfo = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Text_Info"));
            }
        }
        private Color _infoColor;
        public Color InfoColor
        {
            get { return _infoColor; }
            set
            {
                if (_infoColor == null)
                    return;
                _infoColor = value;
                OnPropertyChanged(new PropertyChangedEventArgs("InfoColor"));
            }
        }
        #endregion

        #region RegNr
        private string _txtRegnr;
        public string Text_RegNr
        {
            get { return _txtRegnr; }
            set
            {
                if (_txtRegnr == value)
                    return;
                _txtRegnr = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Text_RegNr"));
            }
        }
        #endregion

        #region AoNr
        private string _txtAonr;
        public string Text_AoNr
        {
            get { return _txtAonr; }
            set
            {
                if (_txtAonr == value)
                    return;
                _txtAonr = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Text_AoNr"));
            }
        }
        #endregion

        #region Date
        private DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedDate"));
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
                GetOfficeSettings();
                SelectedDeptTask = null;
            }
        }
        private Color _officeColor;
        public Color OfficeColor
        {
            get { return _officeColor; }
            set
            {
                if (_officeColor == null)
                    return;
                _officeColor = value;
                OnPropertyChanged(new PropertyChangedEventArgs("OfficeColor"));
            }
        }
        #endregion

        #region Office Department
        private ObservableCollection<OfficeDepartments> _allOfficeDepartments;
        public ObservableCollection<OfficeDepartments> AllOfficeDepartments
        {
            get { return _allOfficeDepartments; }
            set
            {
                if (_allOfficeDepartments == value)
                    return;
                _allOfficeDepartments = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AllOfficeDepartments"));
            }
        }

        private OfficeDepartments _selectedOfficeDept;
        public OfficeDepartments SelectedOfficeDept
        {
            get { return _selectedOfficeDept; }
            set
            {
                if (_selectedOfficeDept == value)
                    return;
                _selectedOfficeDept = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedOfficeDept"));
                if (SelectedOfficeDept != null)
                {
                    SelectedOfficeDeptTasks = new ObservableCollection<OfficeDepartmentTasks>(SelectedOfficeDept.OfficeDepartmentTasks);
                    if(SelectedOfficeDept.ResponsibleEmployeeId > 0)
                    {
                        SelectedResponsibleEmployee = ResponsibleEmployees.Where(emp => emp.Id == SelectedOfficeDept.ResponsibleEmployeeId).FirstOrDefault();
                    }
                }
            }
        }
        private Color _departmentColor;
        public Color DepartmentColor
        {
            get { return _departmentColor; }
            set
            {
                if (_departmentColor == null)
                    return;
                _departmentColor = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DepartmentColor"));
            }
        }
        #endregion

        #region Office Department Tasks
        private ObservableCollection<OfficeDepartmentTasks> _selectedOfficeDeptTasks;
        public ObservableCollection<OfficeDepartmentTasks> SelectedOfficeDeptTasks
        {
            get { return _selectedOfficeDeptTasks; }
            set
            {
                if (_selectedOfficeDeptTasks == value)
                    return;
                _selectedOfficeDeptTasks = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedOfficeDeptTasks"));
            }
        }
        private OfficeDepartmentTasks _selectedDeptTask;
        public OfficeDepartmentTasks SelectedDeptTask
        {
            get { return _selectedDeptTask; }
            set
            {
                if (_selectedDeptTask == value)
                    return;
                _selectedDeptTask = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedDeptTask"));
            }
        }
        private Color _taskColor;
        public Color TaskColor
        {
            get { return _taskColor; }
            set
            {
                if (_taskColor == null)
                    return;
                _taskColor = value;
                OnPropertyChanged(new PropertyChangedEventArgs("TaskColor"));
            }
        }
        #endregion

        #region Employees
        public ChooseEmployeesVM searchEmpVM { get; set; }

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
        private ObservableCollection<Employee> _responsibleEmployees;
        public ObservableCollection<Employee> ResponsibleEmployees
        {
            get { return _responsibleEmployees; }
            set
            {
                if (_responsibleEmployees == value)
                    return;
                _responsibleEmployees = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ResponsibleEmployees"));
            }
        }
        private Employee _selectedResponsibleEmployee;
        public Employee SelectedResponsibleEmployee
        {
            get { return _selectedResponsibleEmployee; }
            set
            {
                if (_selectedResponsibleEmployee == value)
                    return;
                _selectedResponsibleEmployee = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedResponsibleEmployee"));
            }
        }
        private Color _responsibleEmpColor;
        public Color ResponsibleEmpColor
        {
            get { return _responsibleEmpColor; }
            set
            {
                if (_responsibleEmpColor == null)
                    return;
                _responsibleEmpColor = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ResponsibleEmpColor"));
            }
        }
        #endregion

        #region Photo Files
        private ObservableCollection<PhotoCls> _allPhotoFiles;
        public ObservableCollection<PhotoCls> AllPhotoFiles
        {
            get { return _allPhotoFiles; }
            set
            {
                if (_allPhotoFiles == value)
                    return;
                _allPhotoFiles = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AllPhotoFiles"));
            }
        }
        #endregion

        #endregion

        #region Constructors
        public NewQualityReportVM()
        {
            GetOffices();
            SearchEmployeeCommand = new DelegateCommand(SearchEmployee, CanSearchEmployee);
            SaveQRCommand = new DelegateCommand(TrySaveQR, CanTrySaveQR);
            UploadFileCommand = new Command(UploadFile); 
            InfoColor = Color.FromHex("#1976D2");
            OfficeColor = Color.FromHex("#1976D2");
            ResponsibleEmpColor = Color.FromHex("#1976D2");
            DepartmentColor = Color.FromHex("#1976D2");
            TaskColor = Color.FromHex("#1976D2");
        }
        #endregion

        #region Methods

        #region Save Quality Report
        private void TrySaveQR(object param)
        {
            try
            {
                if (ValuesCheck())
                {
                    DbContext = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);

                    QualityReportData qrData = new QualityReportData();

                    qrData.Description = Text_Info;
                    qrData.RegNr = Text_RegNr;
                    qrData.AoNr = Text_AoNr;
                    
                    if(AllPhotoFiles != null && AllPhotoFiles.Count > 0)
                    {
                        qrData.QRAttachedFileData = new QRAttachedFileData[AllPhotoFiles.Count];
                        QRAttachedFileData file;
                        int i = 0;
                        foreach(PhotoCls phfile in AllPhotoFiles)
                        {
                            file = new QRAttachedFileData
                            {
                                FileName = phfile.Name,
                                FilePath = phfile.Path
                            };

                            qrData.QRAttachedFileData[i] = file;
                            i++;
                        }
                    }

                    qrData.OfficeId = SelectedOffice.Id;
                    qrData.QRReportResponsibleId = SelectedResponsibleEmployee.Id;
                    qrData.OfficeDepartmentTaskId = SelectedDeptTask.Id;
                    qrData.CreatedBy = Convert.ToInt32(Application.Current.Properties["EmpId"]);
                    qrData.Created = SelectedDate;

                    var result = DbContext.InsertQualityReport(Application.Current.Properties["UN"].ToString(),
                            Application.Current.Properties["PW"].ToString(), Application.Current.Properties["Ucid"].ToString(), qrData);

                    Application.Current.MainPage.DisplayAlert("Meddelande", result.Message, "STÄNG");
                    Application.Current.MainPage.Navigation.PopAsync();
                }
            }
            catch (Exception e)
            {
                Application.Current.MainPage.DisplayAlert("Fel", e.Message, "STÄNG");
            }
        }
        private bool CanTrySaveQR(object param)
        {
            return true;
        }
        #endregion

        #region Office Settings
        private void GetOfficeSettings()
        {
            try
            {
                //IsBusy = true;
                DbContext = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
                var result = DbContext.GetOfficeSettingsNewQualityReport(Application.Current.Properties["UN"].ToString(), Application.Current.Properties["PW"].ToString(),
                    Application.Current.Properties["Ucid"].ToString(), SelectedOffice.Id);

                AllOfficeDepartments = new ObservableCollection<OfficeDepartments>();
                ResponsibleEmployees = new ObservableCollection<Employee>();
                OfficeDepartments offdept;
                OfficeDepartmentTasks tasks;
                Employee emp;

                if (result != null)
                {
                    foreach (var e in result.Employees.OrderBy(o => o.LName))
                    {
                        emp = new Employee();
                        emp.Id = e.Id;
                        emp.Name = e.FName + " " + e.LName;
                        ResponsibleEmployees.Add(emp);

                    }
                    foreach (var item in result.OfficeDepartments.OrderBy(o => o.Name))
                    {
                        offdept = new OfficeDepartments();
                        offdept.Id = item.Id;
                        offdept.Name = item.Name;
                        offdept.OfficeId = item.OfficeId;
                        offdept.ResponsibleEmployeeId = item.ResponsibleEmployeeId;

                        if (item.Tasks != null)
                        {
                            offdept.OfficeDepartmentTasks = new ObservableCollection<OfficeDepartmentTasks>();
                            foreach (var t in item.Tasks.OrderBy(o => o.Name))
                            {
                                tasks = new OfficeDepartmentTasks();
                                tasks.Id = t.Id;
                                tasks.Name = t.Name;
                                tasks.OfficeDepartmentId = t.OfficeDepartmentId;
                                offdept.OfficeDepartmentTasks.Add(tasks);
                            }
                        }
                        AllOfficeDepartments.Add(offdept);
                    }
                }
                //IsBusy = false;
            }
            catch (Exception e)
            {
                //IsBusy = false;
                Application.Current.MainPage.DisplayAlert("Fel", e.Message, "STÄNG");
            }
        }

        #endregion

        #region Get Offices
        public async void GetOffices()
        {
            try
            {
                IsBusy = true;
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
                SelectedOffice = AllOffices.Where(of => of.Id == Convert.ToInt32(Application.Current.Properties["OfficeId"])).FirstOrDefault();
                //SelectedEmployee = AllOffices.SelectMany(en => en.Employees).Where(em => em.Id == Convert.ToInt32(Application.Current.Properties["EmpId"])).FirstOrDefault();
                IsBusy = false;
            }
            catch (Exception e)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Fel", e.Message, "Stäng");
            }
        }
        #endregion

        #region  Open Search Employee
        private void SearchEmployee(object obj)
        {
            ChooseEmployeePage page = new ChooseEmployeePage();
            searchEmpVM = new ChooseEmployeesVM(AllOffices);
            page.BindingContext = searchEmpVM;
            page.Disappearing += Page_Disappearing;

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
                //SelectedOffice = searchEmpVM.SelectedOffice;
                SelectedEmployee = searchEmpVM.SelectedEmployee;
            }
        }
        #endregion

        #region Upload File
        public async void UploadFile()
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

                    if (AllPhotoFiles == null)
                        AllPhotoFiles = new ObservableCollection<PhotoCls>();

                    PhotoCls phfile = new PhotoCls();
                    phfile.Name = filename;
                    phfile.Path = filePath;
                    
                    AllPhotoFiles.Add(phfile);
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
        #endregion

        #region Values Check
        private bool ValuesCheck()
        {
            bool isvalid = true;
            if (!CheckInfo())
            {
                isvalid = false;
            }
            if (!CheckOffice())
            {
                isvalid = false;
            }
            if (!CheckResponsibleEmp())
            {
                isvalid = false;
            }
            if (!CheckDepartment())
            {
                isvalid = false;
            }
            if (!CheckTask())
            {
                isvalid = false;
            }
            return isvalid;
        }
        private bool CheckInfo()
        {
            bool isvalid = true;
            if (string.IsNullOrWhiteSpace(Text_Info))
            {
                isvalid = false;
                InfoColor = Color.Red;
            }
            else
                InfoColor = Color.FromHex("#1976D2");
            return isvalid;
        }
        private bool CheckOffice()
        {
            bool isvalid = true;
            if (SelectedOffice == null)
            {
                isvalid = false;
                OfficeColor = Color.Red;
            }
            else
                OfficeColor = Color.FromHex("#1976D2");
            return isvalid;
        }
        private bool CheckResponsibleEmp()
        {
            bool isvalid = true;
            if (SelectedResponsibleEmployee == null)
            {
                isvalid = false;
                ResponsibleEmpColor = Color.Red;
            }
            else
                ResponsibleEmpColor = Color.FromHex("#1976D2");
            return isvalid;
        }
        private bool CheckDepartment()
        {
            bool isvalid = true;
            if (SelectedOfficeDept == null)
            {
                isvalid = false;
                DepartmentColor = Color.Red;
            }
            else
                DepartmentColor = Color.FromHex("#1976D2");
            return isvalid;
        }
        private bool CheckTask()
        {
            bool isvalid = true;
            if (SelectedDeptTask == null)
            {
                isvalid = false;
                TaskColor = Color.Red;
            }
            else
                TaskColor = Color.FromHex("#1976D2");
            return isvalid;
        }
        #endregion

        #endregion
    }
}
