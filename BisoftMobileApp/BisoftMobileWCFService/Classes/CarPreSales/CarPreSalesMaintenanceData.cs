using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BisoftMobileWCFService.Classes.CarPreSales
{
    [DataContract]
    public class CarPreSalesMaintenanceData
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int VehicleBrandId { get; set; }
        [DataMember]
        public string VehicleBrandName { get; set; }
        [DataMember]
        public string VehicelModel { get; set; }
        [DataMember]
        public string RegNr { get; set; }
        [DataMember]
        public EmployeeData MaintenanceResponsible { get; set; }
        [DataMember]
        public string Parking { get; set; }
        [DataMember]
        public string KeyCabinet { get; set; }
        [DataMember]
        public CarPreSaleFlowGroupData CarPreSaleFlowGroupData { get; set; }
        [DataMember]
        public int MaintenanceFormId { get; set; }

        [DataMember]
        public Nullable<DateTime> MaintenanceNext { get; set; }
    }
}