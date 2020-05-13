
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BisoftMobileWCFService.Classes
{
    [DataContract]
    public class ICErrorCodeGroupData
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<ICErrorCodeData> Codes { get; set; }

        [DataMember]
        public ICErrorCodeMainGroupData MainGroup { get; set; }
    }
}