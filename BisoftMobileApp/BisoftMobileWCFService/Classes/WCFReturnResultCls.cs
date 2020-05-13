using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BisoftMobileWCFService.Classes
{
    [DataContract]
    public class WCFReturnResultCls
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Message { get; set; }

    }
}