using BisoftMobileApp.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace BisoftMobileApp.ViewModels.EmployeeVMS
{
    class ChooseEmployeesVM:ViewModelBase
    {
        #region Properties

        #region Return Result

        public bool ReturnResult { get; set; }
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
                if (SelectedEmployee != null)
                {
                    ReturnResult = true;
                    Application.Current.MainPage.Navigation.PopAsync();
                }
            }
        }
        #endregion

        #endregion

        #region Constructors

        public ChooseEmployeesVM(ObservableCollection<Office> offices)
        {
            ReturnResult = false;
            AllOffices = offices;
           

        }

        #endregion

    }
}
