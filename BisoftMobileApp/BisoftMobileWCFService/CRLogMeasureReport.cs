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
    
    public partial class CRLogMeasureReport
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CRLogMeasureReport()
        {
            this.CRMeasureReports = new HashSet<CRMeasureReport>();
        }
    
        public int Id { get; set; }
        public int OfficeId { get; set; }
        public string Name { get; set; }
        public Nullable<int> ControlStatusId { get; set; }
        public Nullable<bool> IsBeforeControlStatus { get; set; }
        public bool IsReportMeasure { get; set; }
        public bool Activated { get; set; }
        public string TimeUnit { get; set; }
        public Nullable<int> TimeNr { get; set; }
    
        public virtual CRStatu CRStatu { get; set; }
        public virtual Office Office { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CRMeasureReport> CRMeasureReports { get; set; }
    }
}
