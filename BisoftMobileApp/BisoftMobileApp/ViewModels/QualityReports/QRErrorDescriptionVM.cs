using BisoftMobileApp.Classes;
using BisoftMobileApp.Classes.QualityReports;
using BisoftMobileApp.HelpClasses;
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
    class QRErrorDescriptionVM : ViewModelBase
    {
        #region ServiceReference
        public Service1Client DbContext { get; set; }
        #endregion

        #region Commands
        public ICommand SaveDescriptionCommand{ get; set; }
        public ICommand CancelCommand => new Command(async () => await Application.Current.MainPage.Navigation.PopAsync());
        #endregion

        #region Properties
        private int IniQRId { get; set; }

        private int IniCreatedById { get; set; }

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

        #region Office Department
        private string IniOffDept { get; set; }

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
        private int IniTaskId { get; set; }

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

        #endregion

        #region Constructors
        public QRErrorDescriptionVM(int qrid, int createdbyid, string regnr, string aonr, string department, int taskid, string description)
        {
            IniQRId = qrid;
            IniCreatedById = createdbyid;
            Text_RegNr = regnr;
            Text_AoNr = aonr;
            IniOffDept = department;
            IniTaskId = taskid;
            Text_Description = description;

            GetOfficeSettings();
            
            SaveDescriptionCommand = new DelegateCommand(SaveErrorDescription, CanSaveErrorDescription);
            DescriptionColor = Color.FromHex("#1976D2");
            DepartmentColor = Color.FromHex("#1976D2");
            TaskColor = Color.FromHex("#1976D2");
        }
        public QRErrorDescriptionVM()
        {

        }
        #endregion

        #region Methods

        #region Get Office Settings
        private void GetOfficeSettings()
        {
            try
            {
                IsBusy = true;
                DbContext = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
                var result = DbContext.GetOfficeSettingsNewQualityReport(Application.Current.Properties["UN"].ToString(),
                    Application.Current.Properties["PW"].ToString(), Application.Current.Properties["Ucid"].ToString(),
                    Convert.ToInt32(Application.Current.Properties["OfficeId"]));

                AllOfficeDepartments = new ObservableCollection<OfficeDepartments>();
                OfficeDepartments offdept;
                OfficeDepartmentTasks tasks;

                if (result != null)
                {
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
                    SelectedOfficeDept = AllOfficeDepartments.Where(d => d.Name == IniOffDept).FirstOrDefault();
                    SelectedDeptTask = SelectedOfficeDeptTasks.Where(t => t.Id == IniTaskId).FirstOrDefault();
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

        #region Save Error Description
        private void SaveErrorDescription(object param)
        {
            try
            {
                if (ValuesCheck())
                {
                    DbContext = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
                    var result = DbContext.UpdateQRDescription(Application.Current.Properties["UN"].ToString(),
                                        Application.Current.Properties["PW"].ToString(), Application.Current.Properties["Ucid"].ToString(),
                                        IniQRId, SelectedDeptTask.Id, Text_Description, Text_RegNr, Text_AoNr, IniCreatedById);

                    Application.Current.MainPage.DisplayAlert("Meddelande", result.Message, "STÄNG");
                    Application.Current.MainPage.Navigation.PopAsync();
                }
            }
            catch (Exception e)
            {
                Application.Current.MainPage.DisplayAlert("Fel", e.Message, "STÄNG");
                throw;
            }
        }
        private bool CanSaveErrorDescription(object param)
        {
            return true;
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
            if (!CheckDepartment())
            {
                isValid = false;
            }
            if (!CheckTask())
            {
                isValid = false;
            }
            return isValid;
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
        private bool CheckDepartment()
        {
            bool hasText = true;
            if (SelectedOfficeDept == null)
            {
                hasText = false;
                DepartmentColor = Color.Red;
            }
            else
                DepartmentColor = Color.FromHex("#1976D2");
            return hasText;
        }
        private bool CheckTask()
        {
            bool hasText = true;
            if (SelectedDeptTask == null)
            {
                hasText = false;
                TaskColor = Color.Red;
            }
            else
                TaskColor = Color.FromHex("#1976D2");
            return hasText;
        }
        #endregion

        #endregion
    }
}
