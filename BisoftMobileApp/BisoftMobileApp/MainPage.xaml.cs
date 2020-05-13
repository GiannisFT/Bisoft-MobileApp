using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BisoftMobileApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            LoginEmployee();
        }

        private void LoginEmployee()
        {
            try
            {
                //ServiceReference1.Service1Client client = new ServiceReference1.Service1Client(ServiceReference1.Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);

                //var result =  client.LoginEmployee("as12", "as12", "xxxyyyzzz");

                //lbLogin.Text = result.FName + " " + result.LName;


            }catch(Exception e)
            {
                lbLogin.Text = e.Message;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new TabbedPage1());
        }
    }
}
