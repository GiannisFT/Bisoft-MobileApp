using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BisoftMobileWCFService.Classes
{
    [DataContract]
    public class CarPreSalesMaintenaceStandardData
    {
        [DataMember]
        public int CarPreSalesId { get; set; }
        [DataMember]
        public DateTime PerformedDate { get; set; }
        [DataMember]
        public int PerformedById { get; set; }
        [DataMember]
        public string DocPath { get; set; }

    }
}