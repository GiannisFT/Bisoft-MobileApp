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
    
    public partial class QRResponsible
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QRResponsible()
        {
            this.QRActionPlans = new HashSet<QRActionPlan>();
            this.QualityReports = new HashSet<QualityReport>();
        }
    
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int OfficeId { get; set; }
        public bool Deleted { get; set; }
        public Nullable<int> OfficeDepartmentId { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual OfficeDepartment OfficeDepartment { get; set; }
        public virtual Office Office { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QRActionPlan> QRActionPlans { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QualityReport> QualityReports { get; set; }
    }
}
