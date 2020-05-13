using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Runtime.Serialization;
using System.Web;

namespace BisoftMobileWCFService.Classes.InternalControl
{
    [DataContract]
    public class InternalControlBrandGoalData
    {
        [DataMember]
        public string BrandName { get; set; }
        [DataMember]
        public int MonthGoal { get; set; }
        [DataMember]
        public int MonthResult { get; set; }
        [DataMember]
        public int YearGoal { get; set; }
        [DataMember]
        public int YearResult { get; set; }
    }
}