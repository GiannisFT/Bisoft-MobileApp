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
    
    public partial class ERStatusEmailNotification
    {
        public int Id { get; set; }
        public int OfficeId { get; set; }
        public int StatusId { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsMailContact { get; set; }
        public bool IsReportRole { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> EmailContactId { get; set; }
        public string ReportRoll { get; set; }
    
        public virtual EmailContact EmailContact { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ERStatu ERStatu { get; set; }
        public virtual Office Office { get; set; }
    }
}
