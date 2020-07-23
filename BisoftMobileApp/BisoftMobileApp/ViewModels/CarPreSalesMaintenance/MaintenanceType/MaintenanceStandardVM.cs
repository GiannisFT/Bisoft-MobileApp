using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using BisoftMobileApp.Classes;
using BisoftMobileApp.Classes.CarPreSales;
using BisoftMobileApp.HelpClasses;
using BisoftMobileApp.ViewModels.EmployeeVMS;
using BisoftMobileApp.Views.EmployeeViews.ChooseEmployee;
using Plugin.Media;
using Plugin.Media.Abstractions;
using ServiceReference1;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BisoftMobileApp.ViewModels.MaintenanceType
{
    class MaintenanceStandardVM : ViewModelBase
    {
        #region ServiceReference
        public Service1Client DbContext { get; set; }
        #endregion

        #region Commands
        public ICommand InsertStandardCommand { get; set; }
        public ICommand SearchEmployeeCommand { get; set; }
        public ICommand CancelCommand => new Command(async () => await Application.Current.MainPage.Navigation.PopAsync());
        public ICommand SelectPic { get; set; }
        #endregion

        #region Properties
        
        public int IniCarPreSalesId { get; set; }

        #region Offices
        public int IniOfficeId { get; set; }
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
        public int IniEmployeeId { get; set; }
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

        #region Date
        private DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (_selectedDate == value)
                    return;
                _selectedDate = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedDate"));
            }
        }
        #endregion

        #region Information
        private string _txtInfo;
        public string Text_info
        {
            get { return _txtInfo; }
            set
            {
                if (_txtInfo == value)
                    return;
                _txtInfo = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Text_info"));
            }
        }
        private Color _infoCheckedColor;
        public Color InfoCheckedColor
        {
            get { return _infoCheckedColor; }
            set
            {
                if (_infoCheckedColor == null)
                    return;
                _infoCheckedColor = value;
                OnPropertyChanged(new PropertyChangedEventArgs("InfoCheckedColor"));
            }
        }
        #endregion

        #region File Path
        private string _filepath;
        public string FilePath
        {
            get { return _filepath; }
            set
            {
                if (_filepath == value)
                    return;
                _filepath = value;
                OnPropertyChanged(new PropertyChangedEventArgs("FilePath"));
            }
        }
        #endregion

        #region IsEnabled
        private bool _isEnabled;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (_isEnabled == value)
                    return;
                _isEnabled = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsEnabled"));
            }
        }
        #endregion

        #endregion

        #region Constructors
        public MaintenanceStandardVM()
        {
            IsEnabled = true;
            InfoCheckedColor = Color.Black;
        }
        public MaintenanceStandardVM(int carpresalesId, int empId, int officeId)
        {
            IsEnabled = true;
            InfoCheckedColor = Color.Black;
            IniCarPreSalesId = carpresalesId;
            IniEmployeeId = empId;
            IniOfficeId = officeId;
            GetEmployees();
            SearchEmployeeCommand = new DelegateCommand(SearchEmployee, CanSearchEmployee);
            InsertStandardCommand = new DelegateCommand(TryInsertMaintenanceStandardData, CanTryInsertMaintenanceStandardData);
            SelectPic = new Command(UploadFile);
        }
        public MaintenanceStandardVM(PreSalesMaintenanceLog log)
        {
            IsEnabled = false;
            InfoCheckedColor = Color.Black;
            SelectedDate = log.DateCreated;
            SelectedEmployee = log.Employee;
            Text_info = log.Description;
            FilePath = log.DocPath;
        }
        #endregion

        #region Methods

        #region Insert Data
        public void TryInsertMaintenanceStandardData(object param)
        {
            try
            {
                if (CheckInfoValue() || FilePath != null)
                {
                    DbContext = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);

                    CarPreSalesMaintenaceStandardData standardData = new CarPreSalesMaintenaceStandardData();
                    standardData.PerformedDate = SelectedDate;
                    standardData.PerformedById = SelectedEmployee.Id;
                    standardData.DocPath = FilePath;
                    standardData.CarPreSalesId = IniCarPreSalesId;

                    var result = DbContext.InsertCarPreSalesmaintenanceStandard(Application.Current.Properties["UN"].ToString(), Application.Current.Properties["PW"].ToString(),
                        Application.Current.Properties["Ucid"].ToString(), standardData);
                    Application.Current.MainPage.DisplayAlert("Meddelande", result, "STÄNG");
                    Application.Current.MainPage.Navigation.PopAsync();
                }
            }
            catch (Exception e)
            {
                Application.Current.MainPage.DisplayAlert("Fel", e.Message, "STÄNG");
            }
        }
        public bool CanTryInsertMaintenanceStandardData(object param)
        {
            return true;
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
                SelectedOffice = AllOffices.Where(of => of.Id == IniOfficeId).FirstOrDefault();
                SelectedEmployee = AllOffices.SelectMany(en => en.Employees).Where(em => em.Id == IniEmployeeId).FirstOrDefault();
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
                SelectedOffice = searchEmpVM.SelectedOffice;
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
                PhotoSize = PhotoSize.Large
            });

            if (file != null)
            {
                string[] temp = file.Path.Split('/');
                string[] tempName = temp[temp.Length - 1].Split('.');
                string filename = tempName[0];
                string foldername = DateTime.Now.ToString("yyyy-MM-dd") + "/" + DateTime.Now.ToString("H-mm-ss");
                FilePath = "Files/CarPreSales/" + Application.Current.Properties["CompanyId"].ToString() + "/" + Application.Current.Properties["OfficeId"].ToString() + "/" + foldername + "/" + temp[temp.Length - 1];

                var content = new MultipartFormDataContent();
                Uri host = new Uri("http://www.bisoft.se/Bisoft/receiver.ashx");
                UriBuilder ub = new UriBuilder(host)
                {
                    Query = string.Format("filename={0}", FilePath)
                };

                Stream data = file.GetStream();

                WebClient c = new WebClient();
                c.OpenWriteCompleted += (sender, e) =>
                {
                    PushData(data, e.Result);
                    e.Result.Close();
                    data.Close();
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
        private bool CheckInfoValue()
        {
            bool isValid = true;

            if (Text_info == null)
            {
                isValid = false;
                InfoCheckedColor = Color.Red;
            }
            else
                InfoCheckedColor = Color.Black;
            return isValid;
        }
        #endregion

        #endregion
    }
}
