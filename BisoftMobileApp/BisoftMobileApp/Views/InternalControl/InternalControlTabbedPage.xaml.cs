using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BisoftMobileApp.Views.InternalControl
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InternalControlTabbedPage : TabbedPage
    {
        public InternalControlTabbedPage()
        {
            InitializeComponent();
        }
    }
}