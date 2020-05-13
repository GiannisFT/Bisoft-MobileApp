using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BisoftMobileApp.Classes.InternalControl
{
    class ICErrorCodeMainGroupCls
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ObservableCollection<ICErrorCodeGroupCls> Groups { get; set; }
    }
}
