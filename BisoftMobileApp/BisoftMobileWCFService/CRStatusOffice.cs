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
    
    public partial class CRStatusOffice
    {
        public int CRStatusId { get; set; }
        public int OfficeId { get; set; }
        public Nullable<int> ControlStatusId { get; set; }
        public Nullable<bool> IsBeforeControlStatus { get; set; }
        public string TimeUnit { get; set; }
        public Nullable<int> TimeNr { get; set; }
        public Nullable<bool> Activated { get; set; }
        public bool IsReportMeasure { get; set; }
    
        public virtual CRStatu CRStatu { get; set; }
        public virtual CRStatu CRStatu1 { get; set; }
        public virtual Office Office { get; set; }
    }
}