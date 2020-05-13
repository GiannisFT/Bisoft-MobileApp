using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BisoftMobileWCFService.Classes.InternalControl
{
    [DataContract]
    public class InternalControlOfficeResultsData
    {
        [DataMember]
        public OfficeData OfficeData { get; set; }
        [DataMember]
        public List<InternalControlOfficeData> Controls { get; set; }
    }
}