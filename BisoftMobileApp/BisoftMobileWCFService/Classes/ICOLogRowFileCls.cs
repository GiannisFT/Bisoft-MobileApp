using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BisoftMobileWCFService.Classes
{
    [DataContract]
    public class ICOLogRowFileData
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string FilePath { get; set; }
    }
}