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
    
    public partial class InternalControlOfficeLogRowCode
    {
        public int Id { get; set; }
        public int ICErrorCodeId { get; set; }
        public int InternalControlOfficeLogRowId { get; set; }
        public bool Deleted { get; set; }
    
        public virtual ICErrorCode ICErrorCode { get; set; }
        public virtual InternalControlOfficeLogRow InternalControlOfficeLogRow { get; set; }
    }
}
