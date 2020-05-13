using BisoftMobileApp.Classes.InternalControl;
using BisoftMobileApp.HelpClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BisoftMobileApp.ViewModels.InternalControl.ErrorCodesVM
{
    class ChooseErrorCodeVM:ViewModelBase
    {
        #region Properties


        #region Commands

        public ICommand CloseCommand { get; set; }

        #endregion

        #region ReturnResult

        public bool ReturnResult { get; set; }
        #endregion

        #region Main Groups


        private ObservableCollection<ICErrorCodeMainGroupCls> _allMainGroups;
        public ObservableCollection<ICErrorCodeMainGroupCls> AllMainGroups
        {
            get
            {
                return _allMainGroups;
            }
            set
            {
                if (_allMainGroups == value)
                    return;
                _allMainGroups = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AllMainGroups"));
            }
        }

        private ICErrorCodeMainGroupCls _selectedMainGroup;
        public ICErrorCodeMainGroupCls SelectedMainGroup
        {
            get
            {
                return _selectedMainGroup;
            }
            set
            {
                if (_selectedMainGroup == value)
                    return;
                _selectedMainGroup = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedMainGroup"));
                if (_selectedMainGroup != null)
                {
                    AllGroups = new ObservableCollection<ICErrorCodeGroupCls>(SelectedMainGroup.Groups);
                }
            }
        }

        #endregion

        #region Groups


        private ObservableCollection<ICErrorCodeGroupCls> _allGroups;
        public ObservableCollection<ICErrorCodeGroupCls> AllGroups
        {
            get
            {
                return _allGroups;
            }
            set
            {
                if (_allGroups == value)
                    return;
                _allGroups = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AllGroups"));
            }
        }

        private ICErrorCodeGroupCls _selectedGroup;
        public ICErrorCodeGroupCls SelectedGroup
        {
            get
            {
                return _selectedGroup;
            }
            set
            {
                if (_selectedGroup == value)
                    return;
                _selectedGroup = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedGroup"));
                if (_selectedGroup != null)
                {
                    AllCodes = new ObservableCollection<ICErrorCodeCls>(SelectedGroup.Codes);
                }
            }
        }

        #endregion

        #region Groups


        private ObservableCollection<ICErrorCodeCls> _allCodes;
        public ObservableCollection<ICErrorCodeCls> AllCodes
        {
            get
            {
                return _allCodes;
            }
            set
            {
                if (_allCodes == value)
                    return;
                _allCodes = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AllCodes"));
            }
        }

        private ICErrorCodeCls _selectedCode;
        public ICErrorCodeCls SelectedCode
        {
            get
            {
                return _selectedCode;
            }
            set
            {
                if (_selectedCode == value)
                    return;
                _selectedCode = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedCode"));
                if (_selectedCode != null)
                {
                    ReturnResult = true;
                    Application.Current.MainPage.Navigation.PopModalAsync();
                }
            }
        }

        #endregion

        #endregion

        #region Constructors

        public ChooseErrorCodeVM(ObservableCollection<ICErrorCodeMainGroupCls> mainGroups)
        {
            ReturnResult = false;
            CloseCommand = new DelegateCommand(ClosePage, CanClosePage);
            AllMainGroups = mainGroups;
        }
        #endregion

        #region Methods

        #region  Close Page

        private bool CanClosePage(object param)
        {
            return true;
        }

        private void ClosePage(object obj)
        {
            ReturnResult = false;
            Application.Current.MainPage.Navigation.PopModalAsync();

        }

        #endregion
        #endregion
    }
}
