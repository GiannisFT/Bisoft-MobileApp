using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BisoftMobileWCFService.Classes.CarPreSales
{
    [DataContract]
    public class CarPreSalesLogData
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public DateTime Created { get; set; }
        [DataMember]
        public EmployeeData CreatedBy { get; set; }
        [DataMember]
        public int CarPreSalesId { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Header { get; set; }

        [DataMember]
        public bool IsMaintenance { get; set; }

        [DataMember]
        public int? CarPreSaleMaintenanceBegId { get; set; }

        [DataMember]
        public int? CarPreSaleMaintenanceLagerId { get; set; }

        [DataMember]
        public string DocPath { get; set; }
        [DataMember]
        public string DocName { get; set; }



    }
}