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
    
    public partial class CRResponsible
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CRResponsible()
        {
            this.CustomerReports = new HashSet<CustomerReport>();
        }
    
        public int EmployeeId { get; set; }
        public int OfficeId { get; set; }
        public int Id { get; set; }
        public bool Deleted { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Office Office { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerReport> CustomerReports { get; set; }
    }
}
