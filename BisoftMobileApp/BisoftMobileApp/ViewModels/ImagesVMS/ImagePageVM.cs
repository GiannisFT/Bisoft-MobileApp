using BisoftMobileApp.HelpClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BisoftMobileApp.ViewModels.ImagesVMS
{
    class ImagePageVM:ViewModelBase
    {
        #region Properties

        public bool DeletePhoto { get; set; }

        #region Image Path
        private string _imagePath;
        public string ImagePath
        {
            get
            {
                return _imagePath;
            }
            set
            {
                if (_imagePath == value)
                    return;
                _imagePath = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ImagePath"));
            }
        }



        #endregion

        #region Commands

        public ICommand CloseImageCommand { get; set; }
        public ICommand DeleteImageCommand { get; set; }

        #endregion

        #endregion

        #region Constructors
        public ImagePageVM(string path)
        {
            DeletePhoto = false;
            ImagePath = "http://www.bisoft.se/Bisoft/ClientBin/" + path;
            CloseImageCommand = new DelegateCommand(CloseImage, CanCloseImage);
            DeleteImageCommand = new DelegateCommand(DeleteImage, CanDeleteImage);
        }
        #endregion

        #region Methods

        #region  Close Image

        private bool CanCloseImage(object param)
        {
            return true;
        }

        private async void CloseImage(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }


        #endregion

        #region  Delete Image

        private bool CanDeleteImage(object param)
        {
            return true;
        }

        private async void DeleteImage(object obj)
        {
            DeletePhoto = true;
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }


        #endregion

        #endregion
    }
}
