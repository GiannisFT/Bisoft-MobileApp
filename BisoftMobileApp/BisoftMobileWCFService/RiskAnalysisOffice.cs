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
    
    public partial class RiskAnalysisOffice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RiskAnalysisOffice()
        {
            this.RiskAnalysisOfficeLogs = new HashSet<RiskAnalysisOfficeLog>();
        }
    
        public int Id { get; set; }
        public int OfficeId { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<int> ResponsibleId { get; set; }
        public int RiskAnalysisCompanyId { get; set; }
        public string IntervalUnit { get; set; }
        public Nullable<int> IntervallNr { get; set; }
        public bool Deleted { get; set; }
        public Nullable<System.DateTime> Next { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Office Office { get; set; }
        public virtual RiskAnalysisCompany RiskAnalysisCompany { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RiskAnalysisOfficeLog> RiskAnalysisOfficeLogs { get; set; }
    }
}