using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using ServiceReference1;
using Xamarin.Essentials;
using System.Collections.ObjectModel;
using BisoftMobileApp.HelpClasses;
using BisoftMobileApp.ViewModels.MaintenanceType;
using Xamarin.Forms;
using BisoftMobileApp.Views.CarPreSalesMaintenance;
using System.Linq;
using BisoftMobileApp.Views.EmployeeViews.ChooseEmployee;
using BisoftMobileApp.ViewModels.EmployeeVMS;
using BisoftMobileApp.Classes.CarPreSales;
using BisoftMobileApp.Classes;
using BisoftMobileApp.Views.CarPreSalesMaintenance.Logs;
using BisoftMobileApp.ViewModels.CarPreSalesMaintenance.Logs;

namespace BisoftMobileApp.ViewModels.CarPreSalesMaintenance
{
    class CarPresalesMaintenanceVM : ViewModelBase
    {
        #region ServiceReference
        public Service1Client DbContext { get; set; }
        #endregion

        #region Commands
        public ICommand GetPresalesMaintenance { get; set; }
        public ICommand SearchRegnrCommand { get; set; }
        public ICommand SearchEmployeeCommand { get; set; }
        public ICommand OpenLogCommand { get; set; }
        #endregion

        #region Properties
        public ChooseEmployeesVM searchEmpVM { get; set; }

        #region Maintenance
        private ObservableCollection<CarPresalesMaintenance> allMaintenance;
        public ObservableCollection<CarPresalesMaintenance> AllMaintenance
        {
            get { return allMaintenance; }
            set
            {
                if (allMaintenance == value)
                    return;
                allMaintenance = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AllMaintenance"));
            }
        }
        private CarPresalesMaintenance selectedMaintenance;
        public CarPresalesMaintenance SelectedMaintenance
        {
            get { return selectedMaintenance; }
            set
            {
                if (selectedMaintenance == value)
                    return;
                selectedMaintenance = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedMaintenance"));
                Collection_SelectedMaintenance();
            }
        }
        private ObservableCollection<CarPresalesMaintenance> allMaintenanceFiltered;
        public ObservableCollection<CarPresalesMaintenance> AllMaintenanceFiltered
        {
            get { return allMaintenanceFiltered; }
            set
            {
                if (allMaintenanceFiltered == value)
                    return;
                allMaintenanceFiltered = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AllMaintenanceFiltered"));
            }
        }
        private string _searchedText;
        public string SearchedText
        {
            get { return _searchedText; }
            set
            {
                if (_searchedText == value)
                    return;
                _searchedText = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SearchedText"));
            }
        }
        #endregion

        #region Offices
        private ObservableCollection<Office> allOffices;
        public ObservableCollection<Office> AllOffices
        {
            get { return allOffices; }
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
            get 
            { 
                return selectedOffice; 
            }
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
                GetCarPresalesMaintenance();
                if (SelectedEmployee != null)
                {
                    MaintenanceByEmpId();
                }
            }
        }
        #endregion

        #region Employees

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
                if (SelectedEmployee != null)
                {
                    MaintenanceByEmpId();
                }
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
        public CarPresalesMaintenanceVM()
        {
            GetPresalesMaintenance = new DelegateCommand(GetAllMaintenance, CanTryGetAllMaintenance);
            SearchRegnrCommand = new DelegateCommand(FilterMaintenance, CanFilterMaintenance);
            SearchEmployeeCommand = new DelegateCommand(SearchEmployee, CanSearchEmployee);
            OpenLogCommand = new DelegateCommand(OpenGetLogs, CanTryOpenGetLogs);
            GetEmployees();
        }
        #endregion

        #region Methods

        #region GetMaintenance
        private void GetCarPresalesMaintenance()
        {
            try
            {
                int? empId = null;

                if (SelectedEmployee != null)
                    empId = SelectedEmployee.Id;

                DbContext = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);

                var result = DbContext.GetCarPreSalesmaintenanceByOffice(Application.Current.Properties["UN"].ToString(), Application.Current.Properties["PW"].ToString(),
                        Application.Current.Properties["Ucid"].ToString(), SelectedOffice.Id, empId);
                ObservableCollection<CarPresalesMaintenance> temp = new ObservableCollection<CarPresalesMaintenance>();
                Employee emp;
                PreSalesFlowGroup flowGroup;
                foreach (CarPreSalesMaintenanceData row in result)
                {
                    CarPresalesMaintenance cPM = new CarPresalesMaintenance();
                    cPM.Id = row.Id;
                    cPM.KeyCabinet = row.KeyCabinet;
                    cPM.Parking = row.Parking;
                    cPM.RegNr = row.RegNr;
                    if (row.VehicelModel != null)
                    {
                        cPM.VehicleModel = row.VehicelModel.TrimStart();
                    }
                    else
                        cPM.VehicleModel = row.VehicelModel;
                    cPM.VehicleBrandId = row.VehicleBrandId;
                    cPM.VehicleBrandName = row.VehicleBrandName;
                    if (row.CarPreSaleFlowGroupData != null)
                    {
                        flowGroup = new PreSalesFlowGroup();
                        flowGroup.Id = row.CarPreSaleFlowGroupData.Id;
                        flowGroup.Name = row.CarPreSaleFlowGroupData.Name;
                        flowGroup.Color = row.CarPreSaleFlowGroupData.Color;
                        cPM.PreSalesFlowGroup = flowGroup;
                    }
                    if (row.MaintenanceResponsible != null)
                    {
                        emp = new Employee();
                        emp.Id = row.MaintenanceResponsible.Id;
                        emp.Name = row.MaintenanceResponsible.FName + " " + row.MaintenanceResponsible.LName;
                        cPM.Employee = emp;
                    }
                    cPM.MaintenanceFormId = row.MaintenanceFormId;
                    cPM.NextDate = row.MaintenanceNext;
                    temp.Add(cPM);
                }
                AllMaintenance = new ObservableCollection<CarPresalesMaintenance>(temp.OrderBy(o => o.NextDate));
                AllMaintenanceFiltered = new ObservableCollection<CarPresalesMaintenance>(AllMaintenance);
            }
            catch (Exception e)
            {
                Application.Current.MainPage.DisplayAlert("Fel", e.Message, "STÄNG");
            }
        }
        private bool CanTryGetCarPresalesMaintenance(object param)
        {
            return true;
        }
        #endregion

        #region Show All Maintenance
        public void GetAllMaintenance(object param)
        {
            AllMaintenanceFiltered = new ObservableCollection<CarPresalesMaintenance>(AllMaintenance);
            SelectedEmployee = null;
        }
        private bool CanTryGetAllMaintenance(object param)
        {
            return true;
        }
        #endregion

        #region Filter Maintenance
        private void FilterMaintenance(object param)
        {
            if (!string.IsNullOrWhiteSpace(SearchedText))
            {
                AllMaintenanceFiltered = new ObservableCollection<CarPresalesMaintenance>(AllMaintenanceFiltered.Where(kl => kl.RegNr.ToLower().StartsWith(SearchedText.ToLower())));
            }
            else
            {
                if (SelectedEmployee == null)
                {
                    AllMaintenanceFiltered = new ObservableCollection<CarPresalesMaintenance>(AllMaintenance);
                }
                else
                {
                    AllMaintenanceFiltered = new ObservableCollection<CarPresalesMaintenance>(AllMaintenance.Where(rid => rid.Employee.Id == SelectedEmployee.Id));
                }
            }
        }
        private bool CanFilterMaintenance(object param)
        {
            return true;
        }
        private void MaintenanceByEmpId()
        {
            AllMaintenanceFiltered = new ObservableCollection<CarPresalesMaintenance>(AllMaintenance.Where(rid => rid.Employee.Id == SelectedEmployee.Id));
        }
        #endregion

        #region Open Get Logs
        private void OpenGetLogs(object param)
        {
            int id = (int)param;
            
            MaintenanceLogsVM contextLogs = new MaintenanceLogsVM(id);
            MaintenanceLogsPage logsPage = new MaintenanceLogsPage();
            logsPage.BindingContext = contextLogs;
            Application.Current.MainPage.Navigation.PushAsync(logsPage);
        }
        private bool CanTryOpenGetLogs(object param)
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
                SelectedEmployee = AllOffices.SelectMany(en => en.Employees).Where(em => em.Id == Convert.ToInt32(Application.Current.Properties["EmpId"])).FirstOrDefault();
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

        #region Open Maintenance Type
        public void Collection_SelectedMaintenance()
        {
            if (SelectedMaintenance.MaintenanceFormId != 0)
            {
                switch (SelectedMaintenance.MaintenanceFormId)
                {
                    case 1:
                        MaintenanceStandardVM contextStandard = new MaintenanceStandardVM(SelectedMaintenance.Id, Convert.ToInt32(Application.Current.Properties["EmpId"]), SelectedOffice.Id);
                        MaintenanceStandardPage pageStandard = new MaintenanceStandardPage();
                        pageStandard.BindingContext = contextStandard;
                        pageStandard.Disappearing += PageStandard_Disappearing;
                        Application.Current.MainPage.Navigation.PushAsync(pageStandard);
                        break;
                    case 2:
                        MaintenanceBegVM contextBeg = new MaintenanceBegVM(SelectedMaintenance.Id, Convert.ToInt32(Application.Current.Properties["EmpId"]), SelectedOffice.Id);
                        MaintenanceBegPage pageBeg = new MaintenanceBegPage();
                        pageBeg.BindingContext = contextBeg;
                        pageBeg.Disappearing += PageStandard_Disappearing;
                        Application.Current.MainPage.Navigation.PushAsync(pageBeg);
                        break;
                    case 3:
                        MaintenanceLagerVM contextLager = new MaintenanceLagerVM(SelectedMaintenance.Id, Convert.ToInt32(Application.Current.Properties["EmpId"]), SelectedOffice.Id);
                        MaintenanceLagerPage pageLager = new MaintenanceLagerPage();
                        pageLager.BindingContext = contextLager;
                        pageLager.Disappearing += PageStandard_Disappearing;
                        Application.Current.MainPage.Navigation.PushAsync(pageLager);
                        break;
                    default:
                        Application.Current.MainPage.DisplayAlert("FEL", "Object not found!", "STÄNG");
                        break;
                }
            }
        }

        private void PageStandard_Disappearing(object sender, EventArgs e)
        {
            GetCarPresalesMaintenance();
        }
        #endregion

        #endregion
    }
}

