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
    
    public partial class QROfficeGoal
    {
        public int Id { get; set; }
        public int OfficeId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public Nullable<int> Goal { get; set; }
    
        public virtual Office Office { get; set; }
    }
}
