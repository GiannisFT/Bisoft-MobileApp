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
    
    public partial class ToolGroupInventoryLogRow
    {
        public int Id { get; set; }
        public int ToolGroupInventoryLogId { get; set; }
        public bool ToolExists { get; set; }
        public bool ToolExistsNot { get; set; }
        public System.DateTime Inventoried { get; set; }
        public int ToolId { get; set; }
        public int PerformedById { get; set; }
        public bool ToolBroken { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Tool Tool { get; set; }
        public virtual ToolGroupInventoryLog ToolGroupInventoryLog { get; set; }
    }
}
