using BisoftMobileApp.Classes;
using BisoftMobileApp.Classes.InternalControl;
using BisoftMobileApp.HelpClasses;
using BisoftMobileApp.Views.InternalControl.ControlLogs;
using BisoftMobileApp.Views.InternalControl.PerfomeControl;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BisoftMobileApp.ViewModels.InternalControl
{
    class InternalControlVM:ViewModelBase
    {
      
        #region Properties

        #region ServiceReference
        public ServiceReference1.Service1Client Dbcontext { get; set; }
        #endregion

        #region Command
        public ICommand PerformControlCommand { get; set; }

        public ICommand PerformControlCarucelCommand { get; set; }

        public ICommand OpenLogCommand { get; set; }

        #endregion

        #region Offices
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
                GetControls();
            }
        }
        #endregion

        #region Internal Controls

        private ObservableCollection<InternalControlOffice> allInternalControls;
        public ObservableCollection<InternalControlOffice> AllInternalControls
        {
            get
            {
                return allInternalControls;
            }
            set
            {
                if (allInternalControls == value)
                    return;
                allInternalControls = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AllInternalControls"));
            }
        }

        private InternalControlOffice selectedInternalControl;
        public InternalControlOffice SelectedInternalControl
        {
            get
            {
                return selectedInternalControl;
            }
            set
            {
                if (selectedInternalControl == value)
                    return;
                selectedInternalControl = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedInternalControl"));
            }
        }
        #endregion

        #endregion

        #region Constructors

        public InternalControlVM()
        {
            PerformControlCommand = new DelegateCommand(OpenPerfomeControlPage, CanOpenPerfomeControlPage);
            PerformControlCarucelCommand = new DelegateCommand(OpenPerfomeControlCarucelPage, CanOpenPerfomeControlCarucelPage);
            OpenLogCommand = new DelegateCommand(OpenLogsPage, CanOpenLogsPage);

            GetOffices(); 
        }

        #endregion

        #region Methods

        #region Get Offices
        public async void GetOffices()
        {
            try
            {
                IsBusy = true;
                Dbcontext = new ServiceReference1.Service1Client(ServiceReference1.Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
                Office off;

                //await Dbcontext.OpenAsync();


                var result = Dbcontext.GetOfficesByCompanyId(Application.Current.Properties["UN"].ToString(), 
                    Application.Current.Properties["PW"].ToString(), Application.Current.Properties["Ucid"].ToString(),
                    Convert.ToInt32(Application.Current.Properties["CompanyId"].ToString()));
                AllOffices = new ObservableCollection<Office>();
                foreach (ServiceReference1.OfficeData data in result)
                {
                    off = new Office();
                    off.Name = data.Name;
                    off.Id = data.Id;

                   
                    AllOffices.Add(off);
                }

                IsBusy = false;
            }
            catch(Exception e)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("", e.Message, "Stäng");
            }
           

        }

        #endregion

        #region GetControls

        public async void GetControls()
        {
            if (SelectedOffice != null)
            {
                try
                {
                    IsBusy = true;
                    // Dbcontext = new ServiceReference1.Service1Client(ServiceReference1.Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);

                    // await Dbcontext.OpenAsync();
                    InternalControlOffice cont;
                    InternalControlOfficeGoal goal;
                    var result = Dbcontext.GetOfficeInternalControls(Application.Current.Properties["UN"].ToString(),
                        Application.Current.Properties["PW"].ToString(), Application.Current.Properties["Ucid"].ToString(), SelectedOffice.Id);
                    ObservableCollection<InternalControlOffice> temp = new ObservableCollection<InternalControlOffice>();

                    if (result != null && result.Length > 0)
                    {
                        foreach (ServiceReference1.InternalControlOfficeData data in result)
                        {
                            cont = new InternalControlOffice();
                            cont.Id = data.Id;
                            cont.Name = data.Name;
                            //cont.CountThisMonth = data.PerformedThisMonth + " st";
                            //cont.CountThisYear = data.PerformedThisYear + " st";
                            cont.Goals = new ObservableCollection<InternalControlOfficeGoal>();

                            goal = new InternalControlOfficeGoal();
                            goal.BrandName = "Tot mån";
                            goal.MonthResult = data.PerformedThisMonth + " st";

                            cont.Goals.Add(goal);

                            goal = new InternalControlOfficeGoal();
                            goal.BrandName = "Tot år";
                            goal.MonthResult = data.PerformedThisYear + " st";

                            cont.Goals.Add(goal);

                            if (data.Goals != null && data.Goals.Count() > 0)
                            {
                                

                                foreach(InternalControlBrandGoalData goaldata in data.Goals)
                                {
                                    goal = new InternalControlOfficeGoal();
                                    goal.BrandName = goaldata.BrandName;
                                    goal.MonthResult = goaldata.MonthResult + "/" + goaldata.MonthGoal;
                                    goal.YearResult = goaldata.YearResult + "/" + goaldata.YearGoal;

                                    cont.Goals.Add(goal);
                                }
                            }

                            temp.Add(cont);
                        }
                    }

                    //  await Dbcontext.CloseAsync();

                    AllInternalControls = new ObservableCollection<InternalControlOffice>(temp);
                    IsBusy = false;
                }
                catch(Exception e)
                {
                    IsBusy = false;
                    await Application.Current.MainPage.DisplayAlert("Fel", e.Message, "Stäng");
                }
               
            }
        }

        #endregion

        #region  Open Perform Control

        private bool CanOpenPerfomeControlPage(object param)
        {
            return true;
        }

        private void OpenPerfomeControlPage(object obj)
        {
            int id = (int)obj;

            PerfomeControlCP page = new PerfomeControlCP();
            PerformControlVM context = new PerformControlVM(id, SelectedOffice.Id);
            page.BindingContext = context;
            page.Disappearing += Page_Disappearing;

            Application.Current.MainPage.Navigation.PushAsync(page);


        }

        private void Page_Disappearing(object sender, EventArgs e)
        {
            GetControls();
        }

        #endregion

        #region  Open Perform Control

        private bool CanOpenPerfomeControlCarucelPage(object param)
        {
            return true;
        }

        private void OpenPerfomeControlCarucelPage(object obj)
        {
            int id = (int)obj;

            PerformControlCaruCP page = new PerformControlCaruCP();
            PerformControlVM context = new PerformControlVM(id, SelectedOffice.Id);
            page.BindingContext = context;
            page.Disappearing+= Page_Disappearing;

            Application.Current.MainPage.Navigation.PushAsync(page);


        }

        #endregion

        #region  Open Logs

        private bool CanOpenLogsPage(object param)
        {
            return true;
        }

        private void OpenLogsPage(object obj)
        {
            int id = (int)obj;

            ControlLogsPage page = new ControlLogsPage();
            InternalControlLogsVM context = new InternalControlLogsVM(id, SelectedOffice.Id);
            page.BindingContext = context;
            page.Disappearing+= Page_Disappearing;

            Application.Current.MainPage.Navigation.PushAsync(page);


        }

        #endregion

        #endregion
    }
}
