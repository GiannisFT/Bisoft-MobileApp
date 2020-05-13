using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BisoftMobileWCFService.Classes
{
    [DataContract]
    public class InternalControlOfficeRowData
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string RowText { get; set; }
        [DataMember]
        public string Group { get; set; }
    }
}