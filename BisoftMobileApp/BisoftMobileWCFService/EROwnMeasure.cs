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
    
    public partial class EROwnMeasure
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EROwnMeasure()
        {
            this.ERReportMeasures = new HashSet<ERReportMeasure>();
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
    
        public virtual ERStatu ERStatu { get; set; }
        public virtual Office Office { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ERReportMeasure> ERReportMeasures { get; set; }
    }
}
