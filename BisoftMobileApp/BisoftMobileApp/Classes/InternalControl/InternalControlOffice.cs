using BisoftMobileApp.Classes.InternalControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BisoftMobileApp.Classes.InternalControl
{
    public class InternalControlOffice
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string CountThisMonth { get; set; }

        public string CountThisYear { get; set; }

        public ObservableCollection<InternalControlOfficeGoal> Goals { get; set; }
    }
}
