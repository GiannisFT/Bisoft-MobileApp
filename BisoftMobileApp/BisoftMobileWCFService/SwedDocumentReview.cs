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
    
    public partial class SwedDocumentReview
    {
        public int Id { get; set; }
        public string DocNrs { get; set; }
        public bool ReviewDevation { get; set; }
        public Nullable<int> QualityReportId { get; set; }
        public bool Deleted { get; set; }
        public System.DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public int OfficeId { get; set; }
        public string Name { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Office Office { get; set; }
        public virtual QualityReport QualityReport { get; set; }
    }
}
