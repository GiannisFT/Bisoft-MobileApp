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
    
    public partial class CarPreSalesModuleDocument
    {
        public int Id { get; set; }
        public int CarPreSalesModuleId { get; set; }
        public string Path { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
    
        public virtual CarPreSalesModule CarPreSalesModule { get; set; }
    }
}