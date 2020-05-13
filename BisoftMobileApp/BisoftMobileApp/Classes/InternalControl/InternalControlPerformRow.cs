using BisoftMobileApp.Classes.InternalControl;
using BisoftMobileApp.Classes.Photo;
using BisoftMobileApp.HelpClasses;
using BisoftMobileApp.ViewModels;
using BisoftMobileApp.ViewModels.ImagesVMS;
using BisoftMobileApp.ViewModels.InternalControl.ErrorCodesVM;
using BisoftMobileApp.Views.Images;
using BisoftMobileApp.Views.InternalControl.ErrorCodeViews;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BisoftMobileApp.Classes.InternalControl
{
    class InternalControlPerformRow:ViewModelBase
    {
        #region Properties

        #region Images VM

        public ImagePageVM _imagesVM { get; set; }
        #endregion

        #region Commands

        public ICommand AddFileCommand { get; set; }
        public ICommand AddCommentCommand { get; set; }
        public ICommand DeleteCommentCommand { get; set; }

        public ICommand AddCodeCommand { get; set; }
        public ICommand DeleteCodeCommand { get; set; }

        public ICommand AddPictureCommand { get; set; }
        public ICommand DeletePictureCommand { get; set; }
        #endregion

        #region Id
        public int Id { get; set; }
        #endregion

        #region Id
        public int? InternalControlOffieRowId { get; set; }
        #endregion

        #region Question
        public string Question { get; set; }
        #endregion

        #region Group
        public string Group { get; set; }
        #endregion

        #region HelpText
        public string Helptext { get; set; }
        #endregion

        #region Ok
        private bool _isOk;
        public bool IsOk
        {
            get
            {
                return _isOk;
            }
            set
            {
                if (_isOk == value)
                    return;
                _isOk = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsOk"));
                if(IsOk == true)
                {
                    IsNotOk = false;
                    IsEA = false;
                    TextColor = Xamarin.Forms.Color.Gray;
                    IndicatorColor = Xamarin.Forms.Color.Green;
                    //AddErrorCodesIsVisible = false;
                    //AddPhotoIsVisible = false;
                }

            }
        }

        #endregion

        #region Not Ok
        private bool _isNotOk;
        public bool IsNotOk
        {
            get
            {
                return _isNotOk;
            }
            set
            {
                if (_isNotOk == value)
                    return;
                _isNotOk = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsNotOk"));
                if (IsNotOk == true)
                {
                    IsOk = false;
                    IsEA = false;
                    TextColor = Xamarin.Forms.Color.Gray;
                    IndicatorColor = Xamarin.Forms.Color.Red;
                    //AddErrorCodesIsVisible = true;
                    //AddPhotoIsVisible = true;
                }

            }
        }

        #endregion

        #region EA
        private bool _isEA;
        public bool IsEA
        {
            get
            {
                return _isEA;
            }
            set
            {
                if (_isEA == value)
                    return;
                _isEA = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsEA"));
                if (IsEA == true)
                {
                    IsOk = false;
                    IsNotOk = false;
                    TextColor = Xamarin.Forms.Color.Gray;
                    IndicatorColor = Xamarin.Forms.Color.Blue;
                    //AddErrorCodesIsVisible = false;
                    //AddPhotoIsVisible = false;
                }

            }
        }

        #endregion

        #region Is Clickabled
        private bool _isClickabled;
        public bool IsClickabled
        {
            get
            {
                return _isClickabled;
            }
            set
            {
                if (_isClickabled == value)
                    return;
                _isClickabled = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsClickabled"));

            }
        }

        #endregion

        #region Text Color

        private Xamarin.Forms.Color _textColor;
        public Xamarin.Forms.Color TextColor
        {
            get
            {
                return _textColor;
            }
            set
            {
                if (_textColor == value)
                    return;
                _textColor = value;
                OnPropertyChanged(new PropertyChangedEventArgs("TextColor"));

            }
        }

        #endregion

        #region Comment
        private string _comment;
        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                if (_comment == value)
                    return;
                _comment = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Comment"));

            }
        }

        #endregion

        #region IndicatorColor

        private Xamarin.Forms.Color _indicatorColor;
        public Xamarin.Forms.Color IndicatorColor
        {
            get
            {
                return _indicatorColor;
            }
            set
            {
                if (_indicatorColor == value)
                    return;
                _indicatorColor = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IndicatorColor"));

            }
        }

        #endregion

        #region Error Codes
        public ChooseErrorCodeVM chooseCodeVM { get; set; }
        public ObservableCollection<ICErrorCodeMainGroupCls> ErrorCodeMainGroups { get; set; }

        private ObservableCollection<ICErrorCodeCls> _selectedErrorCodes;
        public ObservableCollection<ICErrorCodeCls> SelectedErrorCodes
        {
            get
            {
                return _selectedErrorCodes;
            }
            set
            {
                if (_selectedErrorCodes == value)
                    return;
                _selectedErrorCodes = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedErrorCodes"));
               
            }
        }

        private ICErrorCodeCls _selectedErrorCode;
        public ICErrorCodeCls SelectedErrorCode
        {
            get
            {
                return _selectedErrorCode;
            }
            set
            {
                if (_selectedErrorCode == value)
                    return;
                _selectedErrorCode = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedErrorCode"));
                if(SelectedErrorCode != null)
                    DeleteErrorCodesIsVisible = true;
                else
                    DeleteErrorCodesIsVisible = false;


            }
        }

        private bool _addErrorCodesIsVisible;
        public bool AddErrorCodesIsVisible
        {
            get
            {
                return _addErrorCodesIsVisible;
            }
            set
            {
                if (_addErrorCodesIsVisible == value)
                    return;
                _addErrorCodesIsVisible = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddErrorCodesIsVisible"));
            }
        }

        private bool _deleteErrorCodesIsVisible;
        public bool DeleteErrorCodesIsVisible
        {
            get
            {
                return _deleteErrorCodesIsVisible;
            }
            set
            {
                if (_deleteErrorCodesIsVisible == value)
                    return;
                _deleteErrorCodesIsVisible = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DeleteErrorCodesIsVisible"));
            }
        }
        #endregion

        #region Photos

        private ObservableCollection<PhotoCls> _addedPhotos;
        public ObservableCollection<PhotoCls> AddedPhotos
        {
            get
            {
                return _addedPhotos;
            }
            set
            {
                if (_addedPhotos == value)
                    return;
                _addedPhotos = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddedPhotos"));

            }
        }

        private PhotoCls _selectedPhoto;
        public PhotoCls SelectedPhoto
        {
            get
            {
                return _selectedPhoto;
            }
            set
            {
                if (_selectedPhoto == value)
                    return;
                _selectedPhoto = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedPhoto"));
                if (SelectedPhoto != null)
                {
                    ImagePage page = new ImagePage();
                    _imagesVM = new ImagePageVM(SelectedPhoto.Path);
                    page.BindingContext = _imagesVM;
                    page.Disappearing += Page_Disappearing1;
                    Application.Current.MainPage.Navigation.PushModalAsync(page);
                    DeletePhotoIsVisible = true;

                    //SelectedPhoto = null;
                }
                else
                    DeletePhotoIsVisible = false;


            }
        }

        private void Page_Disappearing1(object sender, EventArgs e)
        {
            if (SelectedPhoto != null)
                if (_imagesVM.DeletePhoto == true)
                    if (AddedPhotos != null && AddedPhotos.Count > 0 && AddedPhotos.Contains(SelectedPhoto))
                        AddedPhotos.Remove(SelectedPhoto);

            SelectedPhoto = null;
        }

        private bool _addPhotoIsVisible;
        public bool AddPhotoIsVisible
        {
            get
            {
                return _addPhotoIsVisible;
            }
            set
            {
                if (_addPhotoIsVisible == value)
                    return;
                _addPhotoIsVisible = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddPhotoIsVisible"));
            }
        }

        private bool _deletePhotoIsVisible;
        public bool DeletePhotoIsVisible
        {
            get
            {
                return _deletePhotoIsVisible;
            }
            set
            {
                if (_deletePhotoIsVisible == value)
                    return;
                _deletePhotoIsVisible = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DeletePhotoIsVisible"));
            }
        }
        #endregion

        #endregion

        #region Constructors

        public InternalControlPerformRow()
        {
           
            AddFileCommand = new DelegateCommand(OpenAddFile, CanOpenAddFile);
            AddCommentCommand = new DelegateCommand(OpenAddComment, CanOpenAddComment);
            //  DeleteCommentCommand = new DelegateCommand(OpenDeleteComment, CanOpenDeleteComment);
            SelectedErrorCodes = new ObservableCollection<ICErrorCodeCls>();
            AddCodeCommand = new DelegateCommand(OpenSelectErrorCode, CanOpenSelectErrorCode);
            DeleteCodeCommand = new DelegateCommand(OpenDeleteErrorCode, CanOpenDeleteErrorCode);

            AddPictureCommand = new DelegateCommand(OpenAddPicture, CanOpenAddPicture);
        }
        #endregion

        #region Methods

        #region  Open Add File

        private bool CanOpenAddFile(object param)
        {
            return true;
        }


        private void OpenAddFile(object obj)
        {
           //OpenfileDialog


        }

        #endregion

        #region  Open Add Comment

        private bool CanOpenAddComment(object param)
        {
            return true;
        }

     
        private async void OpenAddComment(object obj)
        {

           Comment = await Application.Current.MainPage.DisplayPromptAsync("Kommentar", "Skriv en en kommentar", "Spara", "Avbryt", null, 300, null, Comment);

        }

        #endregion

        #region  Open Add Rrror Code

        private bool CanOpenSelectErrorCode(object param)
        {
            return true;
        }


        private void OpenSelectErrorCode(object obj)
        {
            ErrorCodesPage page = new ErrorCodesPage();
            chooseCodeVM = new ChooseErrorCodeVM(ErrorCodeMainGroups);
            page.BindingContext = chooseCodeVM;
            page.Disappearing += Page_Disappearing;
            Application.Current.MainPage.Navigation.PushModalAsync(page);

        }

        private void Page_Disappearing(object sender, EventArgs e)
        {
            if(chooseCodeVM.ReturnResult == true)
                if(!SelectedErrorCodes.Select(er => er.Id).Contains(chooseCodeVM.SelectedCode.Id))
                    SelectedErrorCodes.Add(chooseCodeVM.SelectedCode);
        }

        #endregion

        #region  Open Delete Error Code

        private bool CanOpenDeleteErrorCode(object param)
        {
            return true;
        }


        private void OpenDeleteErrorCode(object obj)
        {
            if (SelectedErrorCode != null && SelectedErrorCodes.Contains(SelectedErrorCode))
                SelectedErrorCodes.Remove(SelectedErrorCode);

        }

        #endregion

        #region  Open Add Pictue 

        private bool CanOpenAddPicture(object param)
        {
            return true;
        }


        private async void OpenAddPicture(object obj)
        {
            await CrossMedia.Current.Initialize();

            if(!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("Ingen kamera", "Ingen kamera tillgänlig! :(", "Stäng");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()//);
            {
                CompressionQuality = 90

            });

            if (file == null)
            {
                return;

            }

            UploadPhotoToServer(file);

           



        }

        #region Upload Photo To Server

        private async void UploadPhotoToServer(MediaFile file)
        {
            try
            {
                string[] temp = file.Path.Split('/');
                string[] tempName = temp[temp.Length - 1].Split('.');
                string filename = tempName[0];

                string foldername = DateTime.Now.ToString("yyyy-MM-dd") + "/" + DateTime.Now.ToString("H-mm-ss");
                string filepath = "Files/InternalControl/" + Application.Current.Properties["CompanyId"].ToString() + "/" + Application.Current.Properties["OfficeId"].ToString() + "/"+ foldername + "/"+ temp[temp.Length - 1];
                var content = new MultipartFormDataContent();
                Uri host = new Uri("http://www.bisoft.se/Bisoft/receiver.ashx");
                UriBuilder ub = new UriBuilder(host)
                {
                    Query = string.Format("filename={0}", filepath)
                };

                Stream data = file.GetStream();

                WebClient c = new WebClient();
                c.OpenWriteCompleted += (sender, e) =>
                {
                    PushData(data, e.Result);
                    e.Result.Close();
                    data.Close();

                    PhotoCls photo = new PhotoCls
                    {
                        Name = filename,
                        Path = filepath
                    };

                    AddedPhotos.Add(photo);
                };
                c.OpenWriteAsync(ub.Uri);


                // content.Add(new StreamContent(file.GetStream()), 
                //    "\"file\"", 
                //     $"\"{file.Path}\"");

                // var _httpClient = new HttpClient();
                //// _httpClient.PutAsync()

                // //_httpClient.Timeout = TimeSpan.FromSeconds(520);
                // //_httpClient.DefaultRequestHeaders.Accept.




                // var resp = await _httpClient.PostAsync(ub.Uri, content);

                
            }
            catch(Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("", e.Message, "Stäng");
            }


        }

        private void PushData(Stream input, Stream output)
        {
            byte[] buffer = new byte[4096];
            int bytesRead;

            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) != 0)
            {
                output.Write(buffer, 0, bytesRead);
            }
        }

        #endregion

        #endregion

        #endregion
    }
}
