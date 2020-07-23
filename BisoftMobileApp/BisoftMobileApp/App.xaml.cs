using BisoftMobileApp.Views.Login;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BisoftMobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            //if (Device.Idiom == TargetIdiom.Phone)
            //{
            //    Application.Current.Resources.Add(new Style(typeof(Label))
            //    {
            //        Setters = {new Setter { Property = Label.FontSizeProperty, Value = 14} }
            //    });
            //}

            //if (Device.Idiom == TargetIdiom.Tablet)
            //{
            //    Application.Current.Resources.Add(new Style(typeof(Label))
            //    {
            //        Setters = { new Setter { Property = Label.FontSizeProperty, Value = 17 } }
            //    });
            //}
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
