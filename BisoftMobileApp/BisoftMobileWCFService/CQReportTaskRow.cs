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
    
    public partial class CQReportTaskRow
    {
        public int Id { get; set; }
        public Nullable<int> CustomerReportId { get; set; }
        public int OfficeDepartmentTaskId { get; set; }
        public int QualityReportId { get; set; }
    
        public virtual CustomerReport CustomerReport { get; set; }
        public virtual OfficeDepartmentTask OfficeDepartmentTask { get; set; }
        public virtual QualityReport QualityReport { get; set; }
    }
}
