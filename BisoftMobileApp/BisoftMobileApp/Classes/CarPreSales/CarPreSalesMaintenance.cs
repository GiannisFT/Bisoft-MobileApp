using System;
using System.Collections.Generic;
using System.Text;

namespace BisoftMobileApp.Classes.CarPreSales
{
    class CarPresalesMaintenance
    {
        public int Id { get; set; }
        public string KeyCabinet { get; set; }
        public string Parking { get; set; }
        public string RegNr { get; set; }
        public string VehicleModel { get; set; }
        public int VehicleBrandId { get; set; }
        public string VehicleBrandName { get; set; }
        public int MaintenanceFormId { get; set; }
        public DateTime? NextDate { get; set; }
        public Employee Employee { get; set; }
        public PreSalesFlowGroup PreSalesFlowGroup { get; set; }
    }
}

