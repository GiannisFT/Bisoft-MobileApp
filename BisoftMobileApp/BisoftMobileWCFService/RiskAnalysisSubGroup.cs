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
    
    public partial class RiskAnalysisSubGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RiskAnalysisSubGroup()
        {
            this.RiskAnalysisSubGroupRows = new HashSet<RiskAnalysisSubGroupRow>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int CompanyId { get; set; }
    
        public virtual Company Company { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RiskAnalysisSubGroupRow> RiskAnalysisSubGroupRows { get; set; }
    }
}
