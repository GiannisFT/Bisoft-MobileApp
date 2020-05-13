using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BisoftMobileWCFService.Classes
{
    [DataContract]
    public class EmployeeData
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FName { get; set; }
        [DataMember]
        public string LName { get; set; }
        [DataMember]
        public int OfficeId { get; set; }

        [DataMember]
        public int CompanyId { get; set; }

        [DataMember]
        public string Message { get; set; }

    }
}