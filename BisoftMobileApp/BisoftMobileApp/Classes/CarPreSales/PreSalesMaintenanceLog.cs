using System;
using System.Collections.Generic;
using System.Text;

namespace BisoftMobileApp.Classes.CarPreSales
{
    class PreSalesMaintenanceLog
    {
        public int? BegId { get; set; }
        public int? LagerId { get; set; }
        public int PreSalesId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Description { get; set; }
        public string DocName { get; set; }
        public string DocPath { get; set; }
        public string Header { get; set; }
        public int Id { get; set; }
        public bool IsMaintenance { get; set; }
        public Employee Employee { get; set; }
    }
}
