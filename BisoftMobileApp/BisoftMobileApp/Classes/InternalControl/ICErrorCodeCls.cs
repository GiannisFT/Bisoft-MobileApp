using System;
using System.Collections.Generic;
using System.Text;

namespace BisoftMobileApp.Classes.InternalControl
{
    class ICErrorCodeCls
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public ICErrorCodeGroupCls Group { get; set; }
    }
}
