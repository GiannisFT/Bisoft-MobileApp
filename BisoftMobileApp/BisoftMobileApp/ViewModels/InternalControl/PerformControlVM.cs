
using BisoftMobileApp.Classes;
using BisoftMobileApp.Classes.InternalControl;
using BisoftMobileApp.Classes.Photo;
using BisoftMobileApp.HelpClasses;
using BisoftMobileApp.ViewModels.EmployeeVMS;
using BisoftMobileApp.Views.EmployeeViews.ChooseEmployee;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BisoftMobileApp.ViewModels.InternalControl
{
    class PerformControlVM:ViewModelBase
    {
        #region Properties

        #region ServiceReference
        public ServiceReference1.Service1Client Dbcontext { get; set; }
        #endregion

        #region Choose Employee VM
        public ChooseEmployeesVM searchEmpVM { get; set; }

        #endregion

        #region Commands

        public ICommand SaveControlCommand { get; set; }

        public ICommand PartSaveControlCommand { get; set; }

        public ICommand CancelControlCommand { get; set; }
        public ICommand SearchEmployeeCommand { get; set; }

        #endregion

        #region Ini Control Id

        public int IniControlId { get; set; }
        #endregion

        #region Control Name

        private string _controlName;
        public string ControlName
        {
            get
            {
                return _controlName;
            }
            set
            {
                if (_controlName == value)
                    return;
                _controlName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ControlName"));
            }
        }

        #endregion

        #region Rows
        private ObservableCollection<InternalControlPerformRow> allRows;
        public ObservableCollection<InternalControlPerformRow> AllRows
        {
            get
            {
                return allRows;
            }
            set
            {
                if (allRows == value)
                    return;
                allRows = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AllRows"));
            }
        }

        #endregion

        #region Offices

        public int IniOfficeId { get; set; }
        private ObservableCollection<Office> _allOffices;
        public ObservableCollection<Office> AllOffices
        {
            get
            {
                return _allOffices;
            }
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
            get
            {
                return _selectedOffice;
            }
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
        #endregion

        #region Vehicle Brand
        private ObservableCollection<VehicleBrand> _allVehicleBrands;
        public ObservableCollection<VehicleBrand> AllVehicleBrands
        {
            get
            {
                return _allVehicleBrands;
            }
            set
            {
                if (_allVehicleBrands == value)
                    return;
                _allVehicleBrands = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AllVehicleBrands"));
            }
        }

        private VehicleBrand _selectedVehicleBrand;
        public VehicleBrand SelectedVehicleBrand
        {
            get
            {
                return _selectedVehicleBrand;
            }
            set
            {
                if (_selectedVehicleBrand == value)
                    return;
                _selectedVehicleBrand = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedVehicleBrand"));
                
            }
        }

        private Xamarin.Forms.Color _selectedVehicleBrandBackground;
        public Xamarin.Forms.Color SelectedVehicleBrandBackground
        {
            get
            {
                return _selectedVehicleBrandBackground;
            }
            set
            {
                if (_selectedVehicleBrandBackground == value)
                    return;
                _selectedVehicleBrandBackground = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedVehicleBrandBackground"));
            }
        }

        #endregion

        #region Performed Date
        private DateTime performedDate;
        public DateTime PerformedDate
        {
            get
            {
                return performedDate;
            }
            set
            {
                if (performedDate == value)
                    return;
                performedDate = value;
                OnPropertyChanged(new PropertyChangedEventArgs("PerformedDate"));
            }
        }

        #endregion

        #region RegNr
        private string regNr;
        public string RegNr
        {
            get
            {
                return regNr;
            }
            set
            {
                if (regNr == value)
                    return;
                regNr = value;
                OnPropertyChanged(new PropertyChangedEventArgs("RegNr"));
            }
        }

        private Xamarin.Forms.Color regNrBackground;
        public Xamarin.Forms.Color RegNrBackground
        {
            get
            {
                return regNrBackground;
            }
            set
            {
                if (regNrBackground == value)
                    return;
                regNrBackground = value;
                OnPropertyChanged(new PropertyChangedEventArgs("RegNrBackground"));
            }
        }

        #endregion

        #region AoNr
        private string _aoNr;
        public string AONr
        {
            get
            {
                return _aoNr;
            }
            set
            {
                if (_aoNr == value)
                    return;
                _aoNr = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AONr"));
            }
        }

        private Xamarin.Forms.Color _aoNrBackground;
        public Xamarin.Forms.Color AONrBackground
        {
            get
            {
                return _aoNrBackground;
            }
            set
            {
                if (_aoNrBackground == value)
                    return;
                _aoNrBackground = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AONrBackground"));
            }
        }

        #endregion

        #region Vehicle Is Visible

        private bool _vehicleIsVisible;
        public bool VehicleIsVisible
        {
            get
            {
                return _vehicleIsVisible;
            }
            set
            {
                if (_vehicleIsVisible == value)
                    return;
                _vehicleIsVisible = value;
                OnPropertyChanged(new PropertyChangedEventArgs("VehicleIsVisible"));
            }
        }

        #endregion

        #region ErrorCodes

        public ObservableCollection<ICErrorCodeMainGroupCls> ErrorCodeMainGroups { get; set; }
        #endregion

        #region Delete is Visible
        private bool _deleteIsVisible;
        public bool DeleteIsVisible
        {
            get
            {
                return _deleteIsVisible;
            }
            set
            {
                if (_deleteIsVisible == value)
                    return;
                _deleteIsVisible = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DeleteIsVisible"));
            }
        }



        #endregion


        #region Is Editable

        private bool _isEditable;
        public bool IsEditable
        {
            get
            {
                return _isEditable;
            }
            set
            {
                if (_isEditable == value)
                    return;
                _isEditable = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsEditable"));
            }
        }

        #endregion

        #region Is Visible

        private bool _isVisible;
        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                if (_isVisible == value)
                    return;
                _isVisible = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsVisible"));
            }
        }

        #endregion

        #endregion

        #region Constructors

        public PerformControlVM(int controlId, int officeId)
        {
            IsBusy = false;
            IsEditable = true;
            IsVisible = true;
            DeleteIsVisible = false;
            IniControlId = controlId;
            IniOfficeId = officeId;
            VehicleIsVisible = false;
            RegNrBackground = Xamarin.Forms.Color.White;
            SelectedVehicleBrandBackground = Xamarin.Forms.Color.White;
            AONrBackground = Xamarin.Forms.Color.White;
            SaveControlCommand = new DelegateCommand(SaveControl, CanSaveControl);
            PartSaveControlCommand = new DelegateCommand(PartSaveControl, CanPartSaveControl);
            SearchEmployeeCommand = new DelegateCommand(SearchEmployee, CanSearchEmployee);
            CancelControlCommand = new DelegateCommand(CancelSaveControl, CanCancelSaveControl);
            PerformedDate = DateTime.Now;
            GetEmployees();
        }
        #endregion

        #region Methods

        #region GetControls

        public async void GetControlRows()
        {

            try
            {
                InternalControlPerformRow cont;
                //await Dbcontext.OpenAsync();

                var result = Dbcontext.GetOfficeInternalControl("as12", "as12", "xxxyyyzzz", IniOfficeId, IniControlId);
                ObservableCollection<InternalControlPerformRow> temp = new ObservableCollection<InternalControlPerformRow>();

                if (result != null)
                {
                    ControlName = result.Name;

                    if (result.HasVehicleBrand == true)
                        VehicleIsVisible = true;
                    if (result.ControlRows != null && result.ControlRows.Length > 0)
                        foreach (var data in result.ControlRows)
                        {
                            cont = new InternalControlPerformRow();
                            cont.InternalControlOffieRowId = data.Id;
                            cont.Question = data.RowText;
                            cont.TextColor = Xamarin.Forms.Color.Gray;
                            cont.IndicatorColor = Xamarin.Forms.Color.Gray;
                            cont.Group = data.Group;
                            cont.ErrorCodeMainGroups = ErrorCodeMainGroups;
                            cont.AddErrorCodesIsVisible = true;
                            cont.DeleteErrorCodesIsVisible = false;
                            cont.AddPhotoIsVisible = true;
                            cont.DeletePhotoIsVisible = false;
                            cont.AddedPhotos = new ObservableCollection<Classes.Photo.PhotoCls>();
                            cont.IsClickabled = true;
                            temp.Add(cont);
                        }
                }



                AllRows = new ObservableCollection<InternalControlPerformRow>(temp);
                IsBusy = false;

            }
            catch(Exception e)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Fel", e.Message, "Stäng");
            }

        }

        #endregion

        #region GetEmployees

        public async void GetEmployees()
        {

            try
            {
                IsBusy = true;
                Dbcontext = new ServiceReference1.Service1Client(ServiceReference1.Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
               // await Dbcontext.OpenAsync();
                var result = Dbcontext.GetOfficesAndEmployeesByCompanyId(Application.Current.Properties["UN"].ToString(),
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

                                    if (emp.Id == Convert.ToInt32(Application.Current.Properties["EmpId"]))
                                        SelectedEmployee = emp;
                                }
                            }

                            AllOffices.Add(off);
                        }
                    }
                }
                // await Dbcontext.CloseAsync();
                SelectedOffice = AllOffices.Where(of => of.Id == IniOfficeId).FirstOrDefault();

            }
            catch (Exception e)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Fel", e.Message, "Stäng");
            }

            GetVehicleBrands();

        }

        #endregion

        #region GetVehicle Brands

        public async void GetVehicleBrands()
        {

            try
            {
                
                var result = Dbcontext.GetCompanyVehicleBrands(Application.Current.Properties["UN"].ToString(),
                    Application.Current.Properties["PW"].ToString(), Application.Current.Properties["Ucid"].ToString(),
                    Convert.ToInt32(Application.Current.Properties["CompanyId"].ToString()));
                AllVehicleBrands = new ObservableCollection<VehicleBrand>();
                VehicleBrand brand;


                if (result != null)
                {
                    if (result.Length > 0)
                    {
                        foreach (var data in result)
                        {
                            brand = new VehicleBrand();
                            brand.Id = data.Id;
                            brand.Name = data.Name;

                            AllVehicleBrands.Add(brand);
                        }
                    }
                }
                GetErrorCodes();
            }
            catch (Exception e)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Fel", e.Message, "Stäng");
            }
            
        }

        #endregion

        #region Get Error Codes

        public async void GetErrorCodes()
        {
            try
            {
                var result = Dbcontext.GetICErrorCodes(Application.Current.Properties["UN"].ToString(),
                    Application.Current.Properties["PW"].ToString(), Application.Current.Properties["Ucid"].ToString(),
                    Convert.ToInt32(Application.Current.Properties["CompanyId"].ToString()));
                ErrorCodeMainGroups = new ObservableCollection<ICErrorCodeMainGroupCls>();
                ICErrorCodeMainGroupCls maingroup;
                ICErrorCodeGroupCls group;
                ICErrorCodeCls code;


                if (result != null)
                {
                    if (result.Length > 0)
                    {
                        foreach (ServiceReference1.ICErrorCodeMainGroupData data in result)
                        {
                            maingroup = new ICErrorCodeMainGroupCls();
                            maingroup.Id = data.Id;
                            maingroup.Name = data.Name;
                            maingroup.Groups = new ObservableCollection<ICErrorCodeGroupCls>();

                            foreach (ServiceReference1.ICErrorCodeGroupData gdata in data.Groups)
                            {
                                group = new ICErrorCodeGroupCls();
                                group.Id = gdata.Id;
                                group.Name = gdata.Name;
                                group.MainGroup = maingroup;
                                group.Codes = new ObservableCollection<ICErrorCodeCls>();

                                foreach (ServiceReference1.ICErrorCodeData cdata in gdata.Codes)
                                {
                                    code = new ICErrorCodeCls();
                                    code.Id = cdata.Id;
                                    code.Code = cdata.Code;
                                    code.Group = group;
                                    group.Codes.Add(code);
                                }

                                maingroup.Groups.Add(group);
                            }
                            ErrorCodeMainGroups.Add(maingroup);



                        }
                    }
                }
                GetControlRows();
            }
            catch (Exception e)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Fel", e.Message, "Stäng");
            }

        }

        #endregion

        #region  Open Save Control

        private bool CanSaveControl(object param)
        {
            return true;
        }

        private void SaveControl(object obj)
        {
            if (CheckValues())
            {
                Dbcontext = new ServiceReference1.Service1Client(ServiceReference1.Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);

                ServiceReference1.InternalControlOfficeLogData logData = new ServiceReference1.InternalControlOfficeLogData();


                    logData.AoNr = AONr;
                logData.Created = PerformedDate;
                logData.CreatedById = SelectedEmployee.Id;
                logData.Deleted = false;
                logData.InternalControlOfficeId = IniControlId;
                logData.IsPartSaved = false;
                logData.RegNr = RegNr;
                if(SelectedVehicleBrand != null)
                    logData.VehicleBrandId = SelectedVehicleBrand.Id;
                



                ServiceReference1.InternalControlOfficeLogRowData rowData;

                if (AllRows != null && AllRows.Count > 0)
                {
                    ObservableCollection<ServiceReference1.InternalControlOfficeLogRowData> temp = new ObservableCollection<ServiceReference1.InternalControlOfficeLogRowData>(); ;
                    logData.Rows = new ServiceReference1.InternalControlOfficeLogRowData[AllRows.Count];
                    int i = 0;

                    foreach (InternalControlPerformRow row in AllRows)
                    {
                        rowData = new ServiceReference1.InternalControlOfficeLogRowData();
                        rowData.Deleted = false;
                        rowData.InternalControlOfficeRowId = row.InternalControlOffieRowId;
                        rowData.IsEA = row.IsEA;
                        rowData.IsNo = row.IsNotOk;
                        rowData.IsYes = row.IsOk;
                        rowData.Text = row.Question;
                        rowData.Comment = row.Comment;

                        logData.Rows[i] = rowData;
                        i++;

                        if(row.SelectedErrorCodes != null && row.SelectedErrorCodes.Count > 0)
                        {
                            rowData.ErrorCodes = new ServiceReference1.ICErrorCodeData[row.SelectedErrorCodes.Count];
                            int j = 0;
                            ServiceReference1.ICErrorCodeData codeData;

                            foreach (ICErrorCodeCls code in row.SelectedErrorCodes)
                            {
                                codeData = new ServiceReference1.ICErrorCodeData();
                                codeData.Code = code.Code;
                                codeData.Id = code.Id;
                                rowData.ErrorCodes[j] = codeData;
                                j++;

                            }

                        }

                        if(row.AddedPhotos != null && row.AddedPhotos.Count > 0)
                        {
                            rowData.ImageFiles = new ServiceReference1.ICOLogRowFileData[row.AddedPhotos.Count];
                            int h = 0;
                            ServiceReference1.ICOLogRowFileData file;

                            foreach(PhotoCls photo in row.AddedPhotos)
                            {
                                file = new ServiceReference1.ICOLogRowFileData();
                                file.FileName = photo.Name;
                                file.FilePath = photo.Path;
                                rowData.ImageFiles[h] = file;
                                h++;
                            }
                        }

                    }



                }

                string result = Dbcontext.InsertOfficeInternalControlPerformControl(Application.Current.Properties["UN"].ToString(),
                    Application.Current.Properties["PW"].ToString(), Application.Current.Properties["Ucid"].ToString(), logData);

                Dbcontext.CloseAsync();

               Application.Current.MainPage.DisplayAlert("Spara", result, "avbryt");


                Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        #endregion

        #region  Open Part Save Control

        private bool CanPartSaveControl(object param)
        {
            return true;
        }

        private async void PartSaveControl(object obj)
        {
            try
            {
                Dbcontext = new ServiceReference1.Service1Client(ServiceReference1.Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);


                ServiceReference1.InternalControlOfficeLogData logData = new ServiceReference1.InternalControlOfficeLogData();
                logData.AoNr = AONr;
                logData.Created = PerformedDate;
                if (SelectedEmployee != null)
                    logData.CreatedById = SelectedEmployee.Id;
                logData.InternalControlOfficeId = IniControlId;
                logData.IsPartSaved = true;
                logData.RegNr = RegNr;
                if (SelectedVehicleBrand != null)
                    logData.VehicleBrandId = SelectedVehicleBrand.Id;


                ServiceReference1.InternalControlOfficeLogRowData rowData;

                if (AllRows != null && AllRows.Count > 0)
                {
                    ObservableCollection<ServiceReference1.InternalControlOfficeLogRowData> temp = new ObservableCollection<ServiceReference1.InternalControlOfficeLogRowData>(); ;
                    logData.Rows = new ServiceReference1.InternalControlOfficeLogRowData[AllRows.Count];
                    int i = 0;

                    foreach (InternalControlPerformRow row in AllRows)
                    {
                        rowData = new ServiceReference1.InternalControlOfficeLogRowData();
                        rowData.Deleted = false;
                        rowData.InternalControlOfficeRowId = row.InternalControlOffieRowId;
                        rowData.IsEA = row.IsEA;
                        rowData.IsNo = row.IsNotOk;
                        rowData.IsYes = row.IsOk;
                        rowData.Text = row.Question;
                        rowData.Comment = row.Comment;

                        logData.Rows[i] = rowData;
                        i++;

                        if (row.SelectedErrorCodes != null && row.SelectedErrorCodes.Count > 0)
                        {
                            rowData.ErrorCodes = new ServiceReference1.ICErrorCodeData[row.SelectedErrorCodes.Count];
                            int j = 0;
                            ServiceReference1.ICErrorCodeData codeData;

                            foreach (ICErrorCodeCls code in row.SelectedErrorCodes)
                            {
                                codeData = new ServiceReference1.ICErrorCodeData();
                                codeData.Code = code.Code;
                                codeData.Id = code.Id;
                                rowData.ErrorCodes[j] = codeData;
                                j++;

                            }

                        }

                        if (row.AddedPhotos != null && row.AddedPhotos.Count > 0)
                        {
                            rowData.ImageFiles = new ServiceReference1.ICOLogRowFileData[row.AddedPhotos.Count];
                            int h = 0;
                            ServiceReference1.ICOLogRowFileData file;

                            foreach (PhotoCls photo in row.AddedPhotos)
                            {
                                file = new ServiceReference1.ICOLogRowFileData();
                                file.FileName = photo.Name;
                                file.FilePath = photo.Path;
                                rowData.ImageFiles[h] = file;
                                h++;
                            }
                        }

                    }



                }

                string result = Dbcontext.InsertOfficeInternalControlPerformControl(Application.Current.Properties["UN"].ToString(),
                    Application.Current.Properties["PW"].ToString(), Application.Current.Properties["Ucid"].ToString(), logData);

                await Dbcontext.CloseAsync();

                await Application.Current.MainPage.DisplayAlert("Spara", "Kontrollen är delsparad", "Stäng");


                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch(Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Spara", e.Message, "Stäng");
            }
           
        }


        #endregion

        #region  Cancel Save Control

        private bool CanCancelSaveControl(object param)
        {
            return true;
        }

        private async void CancelSaveControl(object obj)
        {
            await Dbcontext.CloseAsync();
            await Application.Current.MainPage.Navigation.PopAsync();
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
                SelectedEmployee = searchEmpVM.SelectedEmployee;
        }

        #endregion

        #region Check values

        private bool CheckValues()
        {
            bool isValid = true;

            if (VehicleIsVisible)
            {
                if (SelectedVehicleBrand == null)
                {
                    isValid = false;
                    SelectedVehicleBrandBackground = Xamarin.Forms.Color.Red;
                }
                else
                    SelectedVehicleBrandBackground = Xamarin.Forms.Color.White;

                if (string.IsNullOrWhiteSpace(RegNr))
                {
                    isValid = false;
                    RegNrBackground = Xamarin.Forms.Color.Red;
                }
                else
                {
                    RegNrBackground = Xamarin.Forms.Color.White;
                }

                if (string.IsNullOrWhiteSpace(AONr))
                {
                    isValid = false;
                    AONrBackground = Xamarin.Forms.Color.Red;
                }
                else
                {
                    AONrBackground = Xamarin.Forms.Color.White;
                }
            }

            if(AllRows != null && AllRows.Count > 0)
            {
                foreach (InternalControlPerformRow row in AllRows)
                {
                    if (row.IsEA != true && row.IsNotOk != true && row.IsOk != true)
                    {
                        isValid = false;
                        row.TextColor = Xamarin.Forms.Color.Red;

                    }
                }
            }

            return isValid;
        }

        #endregion

        #endregion
    }
}
