using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BisoftMobileWCFService.Classes
{
    [DataContract]
    public class InternalControlOfficeLogRowData
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public int? InternalControlOfficeRowId { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public string Group { get; set; }

        [DataMember]
        public string HelpText { get; set; }

        [DataMember]
        public bool IsYes { get; set; }
        [DataMember]
        public bool IsNo { get; set; }
        [DataMember]
        public bool IsEA { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public bool Deleted { get; set; }

        [DataMember]
        public List<ICErrorCodeData> ErrorCodes { get; set; }
        [DataMember]
        public List<ICOLogRowFileData> ImageFiles { get; set; }

    }
}