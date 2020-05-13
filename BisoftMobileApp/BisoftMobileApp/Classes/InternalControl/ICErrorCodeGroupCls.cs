using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BisoftMobileApp.Classes.InternalControl
{
    class ICErrorCodeGroupCls
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<ICErrorCodeCls> Codes { get; set; }

        public ICErrorCodeMainGroupCls MainGroup { get; set; }
    }
}
