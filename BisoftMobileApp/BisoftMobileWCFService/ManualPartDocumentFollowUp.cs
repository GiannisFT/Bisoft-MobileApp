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
    
    public partial class ManualPartDocumentFollowUp
    {
        public int Id { get; set; }
        public Nullable<int> ManualPartDocumentId { get; set; }
        public Nullable<int> FollowUpId { get; set; }
    
        public virtual FollowUp FollowUp { get; set; }
        public virtual ManualPartDocument ManualPartDocument { get; set; }
    }
}
