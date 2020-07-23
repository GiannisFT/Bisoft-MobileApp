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
    class QRFinalDecisionVM : ViewModelBase
    {
        #region ServiceReference
        public Service1Client DbContext { get; set; }
        #endregion

        #region Commands
        public ICommand SaveFinalDecisionCommand { get; set; }
        public ICommand CancelCommand => new Command(async () => await Application.Current.MainPage.Navigation.PopAsync());
        #endregion

        #region Properties
        private int IniQRId { get; set; }
        private int IniCreatedById { get; set; }

        #region Final Decision Headers
        private string IniFinalDHeader { get; set; }

        private ObservableCollection<QRFinalDecision> _finalDHeaders;
        public ObservableCollection<QRFinalDecision> FinalDHeaders
        {
            get { return _finalDHeaders; }
            set
            {
                if (_finalDHeaders == value)
                    return;
                _finalDHeaders = value;
                OnPropertyChanged(new PropertyChangedEventArgs("FinalDHeaders"));
            }
        }
        private QRFinalDecision _selectedHeader;
        public QRFinalDecision SelectedFinalDHeader
        {
            get { return _selectedHeader; }
            set
            {
                if (_selectedHeader == value)
                    return;
                _selectedHeader = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedFinalDHeader"));
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

        #region Cost
        private int? _costEntry;
        public int? Cost_Entry
        {
            get { return _costEntry; }
            set
            {
                if (_costEntry == value)
                    return;
                _costEntry = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Cost_Entry"));
            }
        }
        #endregion

        #region Is Repeat Repair
        private bool _isRepeatRepair;
        public bool IsRepeatRepair
        {
            get { return _isRepeatRepair; }
            set
            {
                if (_isRepeatRepair == value)
                    return;
                _isRepeatRepair = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsRepeatRepair"));
            }
        }
        #endregion

        #endregion

        #region Constructors
        public QRFinalDecisionVM(int qrid, int createdbyid, string fdheader, string fdtext, int? fdcost, bool isrepeatrepair)
        {
            IniQRId = qrid;
            IniCreatedById = createdbyid;
            IniFinalDHeader = fdheader;
            Text_Description = fdtext;
            Cost_Entry = fdcost;
            IsRepeatRepair = isrepeatrepair;

            GetQRFinalDecisionHeaders();
            SaveFinalDecisionCommand = new DelegateCommand(SaveFinaDecision, CanSaveFinalDecision);
            DescriptionColor = Color.FromHex("#1976D2");
            HeaderColor = Color.FromHex("#1976D2");
        }
        public QRFinalDecisionVM()
        {

        }
        #endregion

        #region Methods

        #region Save Final Decision
        private void SaveFinaDecision(object param)
        {
            try
            {
                if (ValuesCheck())
                {
                    DbContext = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
                    var result = DbContext.UpdateQRFinalDecision(Application.Current.Properties["UN"].ToString(),
                                            Application.Current.Properties["PW"].ToString(), Application.Current.Properties["Ucid"].ToString(),
                                            IniQRId, SelectedFinalDHeader.Id, Text_Description, (int)Cost_Entry, IniCreatedById, IsRepeatRepair);

                    Application.Current.MainPage.DisplayAlert("Meddelande", result.Message, "STÄNG");
                    Application.Current.MainPage.Navigation.PopAsync();
                }
            }
            catch (Exception e)
            {
                Application.Current.MainPage.DisplayAlert("Fel", e.Message, "STÄNG");
            }
        }
        private bool CanSaveFinalDecision(object param)
        {
            return true;
        }
        #endregion

        #region Get Final Decision Headers
        private void GetQRFinalDecisionHeaders()
        {
            try
            {
                DbContext = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
                var result = DbContext.GetQRFinalDecisionHeaders(Application.Current.Properties["UN"].ToString(),
                                    Application.Current.Properties["PW"].ToString(), Application.Current.Properties["Ucid"].ToString(),
                                    Convert.ToInt32(Application.Current.Properties["CompanyId"]));

                FinalDHeaders = new ObservableCollection<QRFinalDecision>();
                foreach (QRFinalDecisionHeaderData fdh in result.OrderBy(o => o.Text))
                {
                    QRFinalDecision decisionHeader = new QRFinalDecision();
                    decisionHeader.CompanyId = fdh.CompanyId;
                    decisionHeader.Id = fdh.Id;
                    decisionHeader.Text = fdh.Text;
                    FinalDHeaders.Add(decisionHeader);
                }
                if (IniFinalDHeader != null)
                {
                    SelectedFinalDHeader = FinalDHeaders.Where(fh => fh.Text == IniFinalDHeader).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                Application.Current.MainPage.DisplayAlert("Fel", e.Message, "STÄNG");
            }

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
            if (SelectedFinalDHeader == null)
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
