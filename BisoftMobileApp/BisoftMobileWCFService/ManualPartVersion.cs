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
    
    public partial class ManualPartVersion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PartContent { get; set; }
        public int Version { get; set; }
        public int ManualPartId { get; set; }
        public System.DateTime Approved { get; set; }
        public int ApprovedBy { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual ManualPart ManualPart { get; set; }
    }
}
