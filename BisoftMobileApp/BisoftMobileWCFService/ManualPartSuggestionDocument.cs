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
    
    public partial class ManualPartSuggestionDocument
    {
        public int Id { get; set; }
        public int ManualPartSuggestionId { get; set; }
        public int ManualDocumentId { get; set; }
    
        public virtual ManualDocument ManualDocument { get; set; }
        public virtual ManualPartSuggestion ManualPartSuggestion { get; set; }
    }
}