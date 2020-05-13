using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BisoftMobileApp.ViewModels
{
    class ViewModelBase:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Is Busy

        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                if (_isBusy == value)
                    return;
                _isBusy = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsBusy"));
            }
        }

        #endregion

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
