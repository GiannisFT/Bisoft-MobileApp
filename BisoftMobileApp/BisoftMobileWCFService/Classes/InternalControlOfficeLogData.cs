using BisoftMobileWCFService.Classes.InternalControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BisoftMobileWCFService.Classes
{
    [DataContract]
    public class InternalControlOfficeLogData
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public int InternalControlOfficeId { get; set; }
        [DataMember]
        public InternalControlOfficeData InternalControlOffice { get; set; }
        [DataMember]
        public DateTime Created { get; set; }
        [DataMember]
        public int CreatedById { get; set; }
        [DataMember]
        public EmployeeData Employee { get; set; }
        [DataMember]
        public bool Deleted { get; set; }
        [DataMember]
        public int? VehicleBrandId { get; set; }
        [DataMember]
        public VehicleBrandData VehicleBrand { get; set; }
        [DataMember]
        public bool IsPartSaved { get; set; }

        [DataMember]
        public string AoNr { get; set; }

        [DataMember]
        public string RegNr { get; set; }

        [DataMember]
        public int? CreatedOnId { get; set; }

        [DataMember]
        public List<InternalControlOfficeLogRowData> Rows { get; set; }

        /// <summary>
        /// Use Finish or PartSave
        /// </summary>
        public string Action { get; set; }

    }
}