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
    
    public partial class EmployeeManualPartDoc
    {
        public int Id { get; set; }
        public Nullable<int> ManualPartId { get; set; }
        public int EmployeeDocTypeId { get; set; }
        public int EmployeeId { get; set; }
        public string DocPath { get; set; }
        public string DocName { get; set; }
    
        public virtual EmployeeDocType EmployeeDocType { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ManualPart ManualPart { get; set; }
    }
}