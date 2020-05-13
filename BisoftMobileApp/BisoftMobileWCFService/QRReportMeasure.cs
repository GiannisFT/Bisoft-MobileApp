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
    
    public partial class QRReportMeasure
    {
        public int Id { get; set; }
        public int QualityReportId { get; set; }
        public int ControlStatusId { get; set; }
        public string FinishUnit { get; set; }
        public Nullable<int> FinishNr { get; set; }
        public bool IsBeforeControlStatus { get; set; }
        public bool Activated { get; set; }
        public bool IsOwnMeasure { get; set; }
        public bool IsModule { get; set; }
        public bool IsStatus { get; set; }
        public Nullable<int> QRModuleId { get; set; }
        public Nullable<int> QRStatusId { get; set; }
        public Nullable<int> QROwnMeasureId { get; set; }
        public Nullable<System.DateTime> FinishBefore { get; set; }
        public Nullable<System.DateTime> Finished { get; set; }
        public Nullable<System.DateTime> ReminderDate { get; set; }
    
        public virtual QRModule QRModule { get; set; }
        public virtual QROwnMeasure QROwnMeasure { get; set; }
        public virtual QRStatu QRStatu { get; set; }
        public virtual QRStatu QRStatu1 { get; set; }
        public virtual QualityReport QualityReport { get; set; }
    }
}
