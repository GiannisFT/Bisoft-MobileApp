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
    
    public partial class SwedPowerOfAttorneyER
    {
        public int Id { get; set; }
        public System.DateTime IssuedDate { get; set; }
        public int EmployeeTakerId { get; set; }
        public int EmployeeGiverId { get; set; }
        public string Description { get; set; }
        public int OfficeId { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Employee Employee1 { get; set; }
        public virtual Office Office { get; set; }
    }
}
