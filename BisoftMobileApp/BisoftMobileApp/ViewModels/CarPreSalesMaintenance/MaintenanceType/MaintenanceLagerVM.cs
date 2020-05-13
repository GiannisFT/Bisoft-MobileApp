using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using BisoftMobileApp.Classes;
using BisoftMobileApp.HelpClasses;
using BisoftMobileApp.ViewModels.EmployeeVMS;
using BisoftMobileApp.Views.EmployeeViews.ChooseEmployee;
using ServiceReference1;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BisoftMobileApp.ViewModels.MaintenanceType
{
    class MaintenanceLagerVM : ViewModelBase
    {
        #region ServiceReference
        public Service1Client DbContext { get; set; }
        #endregion

        #region Commands
        public ICommand InsertLagerCommand { get; set; }
        public ICommand SearchEmployeeCommand { get; set; }
        public ICommand CancelCommand => new Command(async () => await Application.Current.MainPage.Navigation.PopAsync());
        #endregion

        #region Properties
        public ChooseEmployeesVM searchEmpVM { get; set; }
        public int IniCarPreSalesId { get; set; }

        #region Offices
        public int IniOfficeId { get; set; }
        private ObservableCollection<Office> allOffices;
        public ObservableCollection<Office> AllOffices
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

        private Office selectedOffice;
        public Office SelectedOffice
        {
            get { return selectedOffice; }
            set
            {
                if (selectedOffice == value)
                    return;
                selectedOffice = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedOffice"));
                if (SelectedOffice != null)
                {
                    SelectedOfficeEmployees = new ObservableCollection<Employee>(SelectedOffice.Employees);
                }
            }
        }
        #endregion

        #region Employees
        public int IniEmployeeId { get; set; }
        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get
            {
                return _selectedEmployee;
            }
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
            get
            {
                return _selectedOfficeEmployees;
            }
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
        private DateTime selectedDate = DateTime.Today;

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedDate"));
            }
        }
        #endregion

        #region Battery
        private string txt_battery;
        public string Text_battery
        {
            get { return txt_battery; }
            set
            {
                if (txt_battery == value)
                    return;
                txt_battery = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Text_battery"));
            }
        }

        private bool box_batteryOK;
        private bool box_batteryNotOK;
        public bool IsBatteryOKChecked
        {
            get { return box_batteryOK; }
            set
            {
                box_batteryOK = value;
                if (box_batteryOK)
                    IsBatteryNotOKChecked = false;
                OnPropertyChanged(new PropertyChangedEventArgs("IsBatteryOKChecked"));
            }
        }
        public bool IsBatteryNotOKChecked
        {
            get { return box_batteryNotOK; }
            set
            {
                box_batteryNotOK = value;
                if (box_batteryNotOK)
                    IsBatteryOKChecked = false;
                OnPropertyChanged(new PropertyChangedEventArgs("IsBatteryNotOKChecked"));
            }
        }
        public bool BatteryCheck
        {
            get { return IsBatteryOKChecked; }
            set
            {
                if (IsBatteryOKChecked == false)
                    IsBatteryNotOKChecked = value;
            }
        }
        private Color _batteryCheckedColor;
        public Color BatteryCheckedColor
        {
            get { return _batteryCheckedColor; }
            set
            {
                if (_batteryCheckedColor == null)
                    return;
                _batteryCheckedColor = value;
                OnPropertyChanged(new PropertyChangedEventArgs("BatteryCheckedColor"));
            }
        }
        #endregion

        #region High Voltage Battery
        private string txt_highVbattery;
        public string Text_highVbattery
        {
            get { return txt_highVbattery; }
            set
            {
                if (txt_highVbattery == value)
                    return;
                txt_highVbattery = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Text_highVbattery"));
            }
        }

        private bool box_highVbatteryOK;
        private bool box_highVbatteryNotOK;
        public bool IsHighVBatteryOKChecked
        {
            get { return box_highVbatteryOK; }
            set
            {
                box_highVbatteryOK = value;
                if (box_highVbatteryOK)
                    IsHighVBatteryNotOKChecked = false;
                OnPropertyChanged(new PropertyChangedEventArgs("IsHighVBatteryOKChecked"));
            }
        }
        public bool IsHighVBatteryNotOKChecked
        {
            get { return box_highVbatteryNotOK; }
            set
            {
                box_highVbatteryNotOK = value;
                if (box_highVbatteryNotOK)
                    IsHighVBatteryOKChecked = false;
                OnPropertyChanged(new PropertyChangedEventArgs("IsHighVBatteryNotOKChecked"));
            }
        }
        public bool HighVBatteryCheck
        {
            get { return IsHighVBatteryOKChecked; }
            set
            {
                if (IsHighVBatteryOKChecked == false)
                    IsHighVBatteryNotOKChecked = value;
            }
        }
        private Color _highVbatteryCheckedColor;
        public Color HighVBatteryCheckedColor
        {
            get { return _highVbatteryCheckedColor; }
            set
            {
                if (_highVbatteryCheckedColor == null)
                    return;
                _highVbatteryCheckedColor = value;
                OnPropertyChanged(new PropertyChangedEventArgs("HighVBatteryCheckedColor"));
            }
        }
        #endregion

        #region Tyres
        private string txt_tyres;
        public string Text_tyres
        {
            get { return txt_tyres; }
            set
            {
                if (txt_tyres == value)
                    return;
                txt_tyres = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Text_tyres"));
            }
        }

        private bool box_tyresOK;
        private bool box_tyresNotOK;
        public bool IsTyresOKChecked
        {
            get { return box_tyresOK; }
            set
            {
                box_tyresOK = value;
                if (box_tyresOK)
                    IsTyresNotOKChecked = false;
                OnPropertyChanged(new PropertyChangedEventArgs("IsTyresOKChecked"));
            }
        }
        public bool IsTyresNotOKChecked
        {
            get { return box_tyresNotOK; }
            set
            {
                box_tyresNotOK = value;
                if (box_tyresNotOK)
                    IsTyresOKChecked = false;
                OnPropertyChanged(new PropertyChangedEventArgs("IsTyresNotOKChecked"));
            }
        }
        public bool TyresCheck
        {
            get { return IsTyresOKChecked; }
            set
            {
                if (IsTyresOKChecked == false)
                    IsTyresNotOKChecked = value;
            }
        }
        private Color _tyresCheckedColor;
        public Color TyresCheckedColor
        {
            get { return _tyresCheckedColor; }
            set
            {
                if (_tyresCheckedColor == null)
                    return;
                _tyresCheckedColor = value;
                OnPropertyChanged(new PropertyChangedEventArgs("TyresCheckedColor"));
            }
        }
        #endregion

        #region Brakes
        private string txt_brakediscs;
        public string Text_brakediscs
        {
            get { return txt_brakediscs; }
            set
            {
                if (txt_brakediscs == value)
                    return;
                txt_brakediscs = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Text_brakediscs"));
            }
        }

        private bool box_brakediscsOK;
        private bool box_brakediscsNotOK;
        public bool IsBrakeDiscsOKChecked
        {
            get { return box_brakediscsOK; }
            set
            {
                box_brakediscsOK = value;
                if (box_brakediscsOK)
                    IsBrakeDiscsNotOKChecked = false;
                OnPropertyChanged(new PropertyChangedEventArgs("IsBrakeDiscsOKChecked"));
            }
        }
        public bool IsBrakeDiscsNotOKChecked
        {
            get { return box_brakediscsNotOK; }
            set
            {
                box_brakediscsNotOK = value;
                if (box_brakediscsNotOK)
                    IsBrakeDiscsOKChecked = false;
                OnPropertyChanged(new PropertyChangedEventArgs("IsBrakeDiscsNotOKChecked"));
            }
        }
        public bool BrakeDiscsCheck
        {
            get { return IsBrakeDiscsOKChecked; }
            set
            {
                if (IsBrakeDiscsOKChecked == false)
                    IsBrakeDiscsNotOKChecked = value;
            }
        }
        private Color _brakediscsCheckedColor;
        public Color BrakeDiscsCheckedColor
        {
            get { return _brakediscsCheckedColor; }
            set
            {
                if (_brakediscsCheckedColor == null)
                    return;
                _brakediscsCheckedColor = value;
                OnPropertyChanged(new PropertyChangedEventArgs("BrakeDiscsCheckedColor"));
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
        public MaintenanceLagerVM()
        {
            IsEnabled = true;
            BatteryCheckedColor = Color.Black;
            HighVBatteryCheckedColor = Color.Black;
            TyresCheckedColor = Color.Black;
            BrakeDiscsCheckedColor = Color.Black;
        }
        public MaintenanceLagerVM(int carpresalesId, int empId, int officeId)
        {
            IsEnabled = true;
            IniCarPreSalesId = carpresalesId;
            BatteryCheckedColor = Color.Black;
            HighVBatteryCheckedColor = Color.Black;
            TyresCheckedColor = Color.Black;
            BrakeDiscsCheckedColor = Color.Black;
            IniEmployeeId = empId;
            IniOfficeId = officeId;
            GetEmployees();
            SearchEmployeeCommand = new DelegateCommand(SearchEmployee, CanSearchEmployee);
            InsertLagerCommand = new DelegateCommand(TryInsertMaintenanceLagerData, CanTryInsertMaintenanceLagerData);
        }
        public MaintenanceLagerVM(int lagerId)
        {
            IsEnabled = false;
            BatteryCheckedColor = Color.Black;
            HighVBatteryCheckedColor = Color.Black;
            TyresCheckedColor = Color.Black;
            BrakeDiscsCheckedColor = Color.Black;

            GetLagerLog(lagerId);
        }
        #endregion

        #region Methods

        #region Insert Data
        public void TryInsertMaintenanceLagerData(object param)
        {
            try
            {
                if (CheckValues())
                {
                    DbContext = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);

                    CarPreSalesMaintenaceLagerData lagerdata = new CarPreSalesMaintenaceLagerData();
                    lagerdata.BatteriCheckOK = BatteryCheck;
                    lagerdata.BatteriCheckInfo = Text_battery;
                    lagerdata.HighVoltCheckOK = HighVBatteryCheck;
                    lagerdata.HighVoltCheckInfo = Text_highVbattery;
                    lagerdata.TireCheckOK = TyresCheck;
                    lagerdata.TireCheckInfo = Text_tyres;
                    lagerdata.BrakeCheckOK = BrakeDiscsCheck;
                    lagerdata.BrakeCheckInfo = Text_brakediscs;
                    lagerdata.PerformedDate = SelectedDate;
                    lagerdata.PerformedById = SelectedEmployee.Id;
                    lagerdata.CarPreSalesId = IniCarPreSalesId;

                    var result = DbContext.InsertCarPreSalesmaintenanceLager(Application.Current.Properties["UN"].ToString(), Application.Current.Properties["PW"].ToString(),
                        Application.Current.Properties["Ucid"].ToString(), lagerdata);
                    Application.Current.MainPage.DisplayAlert("Meddelande", result, "STÄNG");
                    Application.Current.MainPage.Navigation.PopAsync();
                }
            }
            catch (Exception e)
            {
                Application.Current.MainPage.DisplayAlert("Fel", e.Message, "STÄNG");
            }
        }
        public bool CanTryInsertMaintenanceLagerData(object param)
        {
            return true;
        }
        #endregion

        #region Get Offices And Employees
        public async void GetEmployees()
        {
            try
            {
                IsBusy = true;
                DbContext = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
                // await Dbcontext.OpenAsync();
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
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Fel", e.Message, "Stäng");
            }
        }
        #endregion

        #region  Open Search Employee
        private bool CanSearchEmployee(object param)
        {
            return true;
        }

        private void SearchEmployee(object obj)
        {
            ChooseEmployeePage page = new ChooseEmployeePage();
            searchEmpVM = new ChooseEmployeesVM(AllOffices);
            page.BindingContext = searchEmpVM;
            page.Disappearing += Page_Disappearing;

            Application.Current.MainPage.Navigation.PushAsync(page);
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

        #region Get Beg Log
        public async void GetLagerLog(int id)
        {
            try
            {
                //IsBusy = true;
                DbContext = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
                var result = DbContext.GetCarPreSalesMaintenanceLager(Application.Current.Properties["UN"].ToString(), Application.Current.Properties["PW"].ToString(),
                    Application.Current.Properties["Ucid"].ToString(), id);
                Employee emp = new Employee();
                SelectedDate = result.PerformedDate;
                emp.Id = result.PerformedByEmployee.Id;
                emp.Name = result.PerformedByEmployee.FName + " " + result.PerformedByEmployee.LName;
                SelectedEmployee = emp;
                //Battery
                Text_battery = result.BatteriCheckInfo;
                if (result.BatteriCheckOK == true)
                    IsBatteryOKChecked = true;
                else
                    IsBatteryNotOKChecked = true;
                //HighVBattery
                Text_highVbattery = result.HighVoltCheckInfo;
                if (result.HighVoltCheckOK == true)
                    IsHighVBatteryOKChecked = true;
                else
                    IsHighVBatteryNotOKChecked = true;
                //Tyres
                Text_tyres = result.TireCheckInfo;
                if (result.TireCheckOK == true)
                    IsTyresOKChecked = true;
                else
                    IsTyresNotOKChecked = true;
                //Brakes
                Text_brakediscs = result.BrakeCheckInfo;
                if (result.BrakeCheckOK == true)
                    IsBrakeDiscsOKChecked = true;
                else
                    IsBrakeDiscsNotOKChecked = true;
            }
            catch (Exception e)
            {
                //IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Fel", e.Message, "Stäng");
            }
        }
        #endregion

        #region Values Check
        private bool CheckValues()
        {
            bool isValid = true;
            if (!CheckBatteryValues())
            {
                isValid = false;
            }
            if (!CheckHighVBatteryValues())
            {
                isValid = false;
            }
            if (!CheckTyresValues())
            {
                isValid = false;
            }
            if (!CheckBrakeDiscsValues())
            {
                isValid = false;
            }
            return isValid;
        }
        private bool CheckBatteryValues()
        {
            bool isValid = true;

            if (IsBatteryOKChecked == false && IsBatteryNotOKChecked == false)
            {
                isValid = false;
                BatteryCheckedColor = Color.Red;
            }
            else if (IsBatteryNotOKChecked == true && string.IsNullOrWhiteSpace(Text_battery))
            {
                isValid = false;
                Application.Current.MainPage.DisplayAlert("Meddelande", "Beskrivningen få ej vara tom!", "STÄNG");
                BatteryCheckedColor = Color.Red;
            }
            else
                BatteryCheckedColor = Color.Black;
            return isValid;
        }

        private bool CheckHighVBatteryValues()
        {
            bool isValid = true;

            if (IsHighVBatteryOKChecked == false && IsHighVBatteryNotOKChecked == false)
            {
                isValid = false;
                HighVBatteryCheckedColor = Color.Red;
            }
            else if (IsHighVBatteryNotOKChecked == true && string.IsNullOrWhiteSpace(Text_highVbattery))
            {
                isValid = false;
                Application.Current.MainPage.DisplayAlert("Meddelande", "Beskrivningen få ej vara tom!", "STÄNG");
                HighVBatteryCheckedColor = Color.Red;
            }
            else
                HighVBatteryCheckedColor = Color.Black;
            return isValid;
        }

        private bool CheckTyresValues()
        {
            bool isValid = true;

            if (IsTyresOKChecked == false && IsTyresNotOKChecked == false)
            {
                isValid = false;
                TyresCheckedColor = Color.Red;
            }
            else if (IsTyresNotOKChecked == true && string.IsNullOrWhiteSpace(Text_tyres))
            {
                isValid = false;
                Application.Current.MainPage.DisplayAlert("Meddelande", "Beskrivningen få ej vara tom!", "STÄNG");
                TyresCheckedColor = Color.Red;
            }
            else
                TyresCheckedColor = Color.Black;
            return isValid;
        }

        private bool CheckBrakeDiscsValues()
        {
            bool isValid = true;

            if (IsBrakeDiscsOKChecked == false && IsBrakeDiscsNotOKChecked == false)
            {
                isValid = false;
                BrakeDiscsCheckedColor = Color.Red;
            }
            else if (IsBrakeDiscsNotOKChecked == true && string.IsNullOrWhiteSpace(Text_brakediscs))
            {
                isValid = false;
                Application.Current.MainPage.DisplayAlert("Meddelande", "Beskrivningen få ej vara tom!", "STÄNG");
                BrakeDiscsCheckedColor = Color.Red;
            }
            else
                BrakeDiscsCheckedColor = Color.Black;
            return isValid;
        }
        #endregion

        #endregion
    }
}

