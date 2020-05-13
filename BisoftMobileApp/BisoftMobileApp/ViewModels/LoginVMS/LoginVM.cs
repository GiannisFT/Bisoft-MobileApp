using BisoftMobileApp.HelpClasses;
using BisoftMobileApp.Views.InternalControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BisoftMobileApp.ViewModels.LoginVMS
{
    class LoginVM:ViewModelBase
    {
        #region Properties

        #region Commands    

        public ICommand LoginCommand { get; set; }

        #endregion

        #region Username

        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                if (_username == value)
                    return;
                _username = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Username"));
            }
        }

        #endregion

        #region Password

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password == value)
                    return;
                _password = value;
                OnPropertyChanged(new PropertyChangedEventArgs("RegNr"));
            }
        }

        #endregion

        #region ucid

        private string _ucid;
        public string Ucid
        {
            get
            {
                return _ucid;
            }
            set
            {
                if (_ucid == value)
                    return;
                _ucid = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Ucid"));
            }
        }

        #endregion    

        #endregion

        #region Constructors

        public LoginVM()
        {
            IsBusy = false;
            LoginCommand = new DelegateCommand(LoginEmployee, CanLoginEmployee);

            if (Application.Current.Properties.ContainsKey("Ucid"))
                Ucid = Application.Current.Properties["Ucid"].ToString();

            if (Application.Current.Properties.ContainsKey("UN"))
                Username = Application.Current.Properties["UN"].ToString();
        }

        #endregion

        #region Methods

        #region Get Login Data
        private async void GetEmployeData()
        {
            try
            {
                IsBusy = true;
                ServiceReference1.Service1Client Dbcontext = new ServiceReference1.Service1Client(ServiceReference1.Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
                // await Dbcontext.OpenAsync();
                var result = Dbcontext.LoginEmployee(Username, Password, Ucid);

                if(result != null)
                {
                    if (!Application.Current.Properties.ContainsKey("UN"))
                        Application.Current.Properties.Add("UN", Username);
                    else
                        Application.Current.Properties["UN"] = Username;

                    if (!Application.Current.Properties.ContainsKey("PW"))
                        Application.Current.Properties.Add("PW", Password);
                    else
                        Application.Current.Properties["PW"] = Password;

                    if (!Application.Current.Properties.ContainsKey("Ucid"))
                        Application.Current.Properties.Add("Ucid", Ucid);
                    else
                        Application.Current.Properties["Ucid"] = Ucid;

                    if (!Application.Current.Properties.ContainsKey("CompanyId"))
                        Application.Current.Properties.Add("CompanyId", result.CompanyId);
                    else
                        Application.Current.Properties["CompanyId"] = result.CompanyId;

                    if (!Application.Current.Properties.ContainsKey("EmpId"))
                        Application.Current.Properties.Add("EmpId", result.Id);
                    else
                        Application.Current.Properties["EmpId"] = result.Id;

                    if (!Application.Current.Properties.ContainsKey("OfficeId"))
                        Application.Current.Properties.Add("OfficeId", result.OfficeId);
                    else
                        Application.Current.Properties["OfficeId"] = result.OfficeId;


                    await Application.Current.SavePropertiesAsync();

                   Application.Current.MainPage = new AppShell();
                    IsBusy = false;
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Fel","Fel inloggningsuppgifter", "Stäng");
            }
            catch(Exception e)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Fel", e.Message, "Stäng");
            }
        }

        #endregion

        #region  Login

        private bool CanLoginEmployee(object param)
        {
            return true;
        }

        private void LoginEmployee(object obj)
        {
            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Ucid))
            {
                GetEmployeData();
            }
            else
                Application.Current.MainPage.DisplayAlert("Fel", "Saknar anändarnamn, lösenord och/eller företagsid", "Stäng");
        }


        #endregion

        

        #endregion
    }
}
