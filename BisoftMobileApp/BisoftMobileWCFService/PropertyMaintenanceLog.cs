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
    
    public partial class PropertyMaintenanceLog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PropertyMaintenanceLog()
        {
            this.PropertyLogs = new HashSet<PropertyLog>();
        }
    
        public int Id { get; set; }
        public int PerformedById { get; set; }
        public System.DateTime PerformedDate { get; set; }
        public bool MaintenanceOk { get; set; }
        public bool MaintenanceNotOk { get; set; }
        public string MaintenanceDoc { get; set; }
        public Nullable<int> QualityReportId { get; set; }
        public string Description { get; set; }
    
        public virtual Employee Employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PropertyLog> PropertyLogs { get; set; }
        public virtual QualityReport QualityReport { get; set; }
    }
}
