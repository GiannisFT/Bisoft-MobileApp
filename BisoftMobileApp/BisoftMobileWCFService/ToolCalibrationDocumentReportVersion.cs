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
    
    public partial class ToolCalibrationDocumentReportVersion
    {
        public int Id { get; set; }
        public int ToolCalibrationDocumentReportId { get; set; }
        public string ReportContent { get; set; }
        public System.DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public bool Deleted { get; set; }
        public Nullable<int> ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> Approved { get; set; }
        public string ApprovementDescription { get; set; }
        public Nullable<int> AdjustedTime { get; set; }
        public Nullable<decimal> ServedCost { get; set; }
        public Nullable<int> AdjustedCost { get; set; }
        public int VersionNr { get; set; }
        public int ReportNr { get; set; }
        public string RegNo { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Employee Employee1 { get; set; }
        public virtual ToolCalibrationDocumentReport ToolCalibrationDocumentReport { get; set; }
    }
}
