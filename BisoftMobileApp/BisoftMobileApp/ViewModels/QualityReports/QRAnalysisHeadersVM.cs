using BisoftMobileApp.Classes;
using BisoftMobileApp.Classes.QualityReports;
using BisoftMobileApp.HelpClasses;
using BisoftMobileApp.ViewModels.EmployeeVMS;
using BisoftMobileApp.Views.EmployeeViews.ChooseEmployee;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BisoftMobileApp.ViewModels.QualityReports
{
    class QRAnalysisHeadersVM : ViewModelBase
    {
        #region ServiceReference
        public Service1Client DbContext { get; set; }
        #endregion

        #region Commands
        public ICommand SaveAnalysisHeaderCommand { get; set; }
        public ICommand SearchEmployeeCommand { get; set; }
        public ICommand RemoveCausedByEmpCommand { get; set; }
        public ICommand CancelCommand => new Command(async () => await Application.Current.MainPage.Navigation.PopAsync());
        #endregion

        #region Properties
        private int IniQRId { get; set; }
        private int IniCreatedById { get; set; }

        #region Employee Ids
        private int[] EmpIds { get; set; }
        #endregion

        #region Analysis Headers
        private int? IniAnalysisHeaderId { get; set; }
        private ObservableCollection<QRAnalysisHeader> _analysisHeaders;
        public ObservableCollection<QRAnalysisHeader> AnalysisHeaders
        {
            get { return _analysisHeaders; }
            set
            {
                if (_analysisHeaders == value)
                    return;
                _analysisHeaders = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AnalysisHeaders"));
            }
        }
        private QRAnalysisHeader _selectedHeader;
        public QRAnalysisHeader SelectedAnalysisHeader
        {
            get { return _selectedHeader; }
            set
            {
                if (_selectedHeader == value)
                    return;
                _selectedHeader = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedAnalysisHeader"));
            }
        }
        private Color _headerColor;
        public Color HeaderColor
        {
            get { return _headerColor; }
            set
            {
                if (_headerColor == null)
                    return;
                _headerColor = value;
                OnPropertyChanged(new PropertyChangedEventArgs("HeaderColor"));
            }
        }
        #endregion

        #region Description
        private string _txtDescription;
        public string Text_Description
        {
            get { return _txtDescription; }
            set
            {
                if (_txtDescription == value)
                    return;
                _txtDescription = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Text_Description"));
            }
        }
        private Color _descriptionColor;
        public Color DescriptionColor
        {
            get { return _descriptionColor; }
            set
            {
                if (_descriptionColor == null)
                    return;
                _descriptionColor = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DescriptionColor"));
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

        #region Caused by Employees
        private ObservableCollection<Employee> IniCausedByEmployees { get; set; }

        private ObservableCollection<Employee> _causedByEmployees;
        public ObservableCollection<Employee> CausedByEmployees
        {
            get { return _causedByEmployees; }
            set
            {
                if (_causedByEmployees == value)
                    return;
                _causedByEmployees = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CausedByEmployees"));
                if (IniCausedByEmployees != null)
                {
                    CausedByEmployees = IniCausedByEmployees;
                }
            }
        }
        private Employee _selectedCausedByEmployee;
        public Employee SelectedCausedByEmployee
        {
            get { return _selectedCausedByEmployee; }
            set
            {
                if (_selectedCausedByEmployee == value)
                    return;
                _selectedCausedByEmployee = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedCausedByEmployee"));
                if (SelectedCausedByEmployee != null)
                {
                    RemoveButtonVisible = true;
                }
            }
        }
        #endregion

        #region Remove Button Visible

        private bool _removeButtonVisible;
        public bool RemoveButtonVisible
        {
            get { return _removeButtonVisible; }
            set
            {
                if (_removeButtonVisible == value)
                    return;
                _removeButtonVisible = value;
                OnPropertyChanged(new PropertyChangedEventArgs("RemoveButtonVisible"));
            }
        }
        #endregion

        #endregion

        #region Constructors
        public QRAnalysisHeadersVM(int qrid, int createdbyid, int? analysisheaderId, string analysistext, ObservableCollection<Employee> causedByEmployees)
        {
            IniCreatedById = createdbyid;
            IniQRId = qrid;
            IniAnalysisHeaderId = analysisheaderId;
            Text_Description = analysistext;
            IniCausedByEmployees = causedByEmployees;
            CausedByEmployees = new ObservableCollection<Employee>();

            GetEmployees();
            GetQRAnalysisHeaders();

            SaveAnalysisHeaderCommand = new DelegateCommand(SaveAnalysisHeader, CanSaveAnalysisHeader);
            SearchEmployeeCommand = new DelegateCommand(SearchEmployee, CanSearchEmployee);
            RemoveCausedByEmpCommand = new DelegateCommand(RemoveCausedByEmployees, CanRemoveCausedByEmployees);
            HeaderColor = Color.FromHex("#1976D2");
            DescriptionColor = Color.FromHex("#1976D2");
        }
        public QRAnalysisHeadersVM()
        {

        }
        #endregion

        #region Methods

        #region Save Analysis Header
        private void SaveAnalysisHeader(object param)
        {
            try
            {
                if (ValuesCheck())
                {
                    EmployeeIds();

                    DbContext = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);

                    var result = DbContext.UpdateQRAnalysis(Application.Current.Properties["UN"].ToString(),
                                        Application.Current.Properties["PW"].ToString(), Application.Current.Properties["Ucid"].ToString(),
                                        IniQRId, SelectedAnalysisHeader.Id, Text_Description, EmpIds, IniCreatedById);

                    Application.Current.MainPage.DisplayAlert("Meddelande", result.Message, "STÄNG");
                    Application.Current.MainPage.Navigation.PopAsync();
                }
            }
            catch (Exception e)
            {
                Application.Current.MainPage.DisplayAlert("Fel", e.Message, "STÄNG");
            }
            
        }
        private bool CanSaveAnalysisHeader(object param)
        {
            return true;
        }
        #endregion

        #region Get Analysis Headers
        private void GetQRAnalysisHeaders()
        {
            try
            {
                DbContext = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
                var result = DbContext.GetQRAnalysisHeaders(Application.Current.Properties["UN"].ToString(),
                                    Application.Current.Properties["PW"].ToString(), Application.Current.Properties["Ucid"].ToString(),
                                    Convert.ToInt32(Application.Current.Properties["CompanyId"]));

                AnalysisHeaders = new ObservableCollection<QRAnalysisHeader>();
                foreach (QRAnalysisHeaderData ahd in result.OrderBy(o => o.Text))
                {
                    QRAnalysisHeader analysisHeader = new QRAnalysisHeader();
                    analysisHeader.CompanyId = ahd.CompanyId;
                    analysisHeader.Id = ahd.Id;
                    analysisHeader.Text = ahd.Text;
                    AnalysisHeaders.Add(analysisHeader);
                }
                if (IniAnalysisHeaderId != null)
                {
                    SelectedAnalysisHeader = AnalysisHeaders.Where(ah => ah.Id == IniAnalysisHeaderId).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                Application.Current.MainPage.DisplayAlert("Fel", e.Message, "STÄNG");
            }
            
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
                SelectedEmployee = searchEmpVM.SelectedEmployee;

                AddCausedByEmployees(SelectedEmployee);
            }
        }
        #endregion

        #region Add Remove Caused by Employees
        private void AddCausedByEmployees(Employee employee)
        {
            if (CausedByEmployees.Count() == 0)
            {
                CausedByEmployees.Add(employee);
            }
            else
            {
                if (!CausedByEmployees.Contains(employee))
                {
                    CausedByEmployees.Add(employee);
                }
            }
        }
        private void RemoveCausedByEmployees(object param)
        {
            CausedByEmployees.Remove(SelectedCausedByEmployee);
            RemoveButtonVisible = false;
        }
        private bool CanRemoveCausedByEmployees(object param)
        {
            return true;
        }
        #endregion

        #region Employee Id's array
        private int[] EmployeeIds()
        {
            if (CausedByEmployees.Count() != 0)
            {
                int size = CausedByEmployees.Count();
                EmpIds = new int[size];
                for (int i = 0; i < EmpIds.Length; i++)
                {
                    EmpIds[i] = CausedByEmployees[i].Id;
                }
            }
            return EmpIds;
        }
        #endregion

        #region Values Check
        private bool ValuesCheck()
        {
            bool isValid = true;
            if (!CheckDescription())
            {
                isValid = false;
            }
            if (!CheckHeader())
            {
                isValid = false;
            }
            return isValid;
        }
        private bool CheckHeader()
        {
            bool hasText = true;
            if (SelectedAnalysisHeader == null)
            {
                hasText = false;
                HeaderColor = Color.Red;
            }
            else
                HeaderColor = Color.FromHex("#1976D2");
            return hasText;
        }
        private bool CheckDescription()
        {
            bool hasText = true;
            if (string.IsNullOrWhiteSpace(Text_Description))
            {
                hasText = false;
                DescriptionColor = Color.Red;
            }
            else
                DescriptionColor = Color.FromHex("#1976D2");
            return hasText;
        }
        #endregion

        #endregion
    }
}
