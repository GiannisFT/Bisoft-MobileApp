//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BisoftMobileWCFService
{
    using System;
    using System.Collections.Generic;
    
    public partial class ACReport
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ACReport()
        {
            this.ACReportGasLogs = new HashSet<ACReportGasLog>();
        }
    
        public int Id { get; set; }
        public int VehicleCompanyId { get; set; }
        public decimal EmptyFromVehicle { get; set; }
        public decimal RefillInVehicle { get; set; }
        public System.DateTime Created { get; set; }
        public int CreatedById { get; set; }
        public int Year { get; set; }
        public int Nr { get; set; }
        public int OfficeId { get; set; }
        public bool Deleted { get; set; }
        public bool DirectMethod { get; set; }
        public Nullable<int> ToolId { get; set; }
        public string ToolTypeBet { get; set; }
        public string RefrigerantType { get; set; }
        public string RefrigerantKg { get; set; }
        public string RefrigerantInfo { get; set; }
        public Nullable<bool> IsInstallation { get; set; }
        public Nullable<bool> IsMaintenance { get; set; }
        public Nullable<bool> IsPeriodic { get; set; }
        public int ACReportGasId { get; set; }
        public Nullable<bool> IsFollowUp { get; set; }
        public Nullable<decimal> OilLiter { get; set; }
        public string OilType { get; set; }
        public bool NotLeaking { get; set; }
        public string LeakingInfo { get; set; }
        public string UnitType { get; set; }
        public string UnitName { get; set; }
        public string UnitNr { get; set; }
        public Nullable<decimal> UnitAmount { get; set; }
        public bool LeakingFixDone { get; set; }
        public Nullable<int> ACOliTypeId { get; set; }
        public Nullable<int> CustomerCompanyId { get; set; }
        public Nullable<decimal> OilRefillLiter { get; set; }
    
        public virtual ACOilType ACOilType { get; set; }
        public virtual ACReportGa ACReportGa { get; set; }
        public virtual CustomerCompany CustomerCompany { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Office Office { get; set; }
        public virtual Tool Tool { get; set; }
        public virtual VehicleCompany VehicleCompany { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACReportGasLog> ACReportGasLogs { get; set; }
    }
}
