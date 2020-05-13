using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BisoftMobileWCFService.Classes.CarPreSales
{
    [DataContract]
    public class CarPreSalesMaintenaceLagerData
    {
        [DataMember]
        public int CarPreSalesId { get; set; }
        [DataMember]
        public DateTime PerformedDate { get; set; }
        [DataMember]
        public int PerformedById { get; set; }
        [DataMember]
        public bool BatteriCheckOK { get; set; }
        [DataMember]
        public string BatteriCheckInfo { get; set; }
        [DataMember]
        public bool BrakeCheckOK { get; set; }
        [DataMember]
        public string BrakeCheckInfo { get; set; }
        [DataMember]
        public bool TireCheckOK { get; set; }
        [DataMember]
        public string TireCheckInfo { get; set; }
        [DataMember]
        public bool HighVoltCheckOK { get; set; }
        [DataMember]
        public string HighVoltCheckInfo { get; set; }
        [DataMember]
        public EmployeeData PerformedByEmployee { get; set; }
    }
}