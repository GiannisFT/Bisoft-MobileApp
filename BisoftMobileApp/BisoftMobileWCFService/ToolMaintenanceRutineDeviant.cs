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
    
    public partial class ToolMaintenanceRutineDeviant
    {
        public int Id { get; set; }
        public int ToolId { get; set; }
        public int MonthNr { get; set; }
        public Nullable<int> ToolMaintenanceRutineOfficeId { get; set; }
        public Nullable<int> ToolMaintenanceRutineCompanyId { get; set; }
    
        public virtual Tool Tool { get; set; }
        public virtual ToolMaintenanceRutineCompany ToolMaintenanceRutineCompany { get; set; }
        public virtual ToolMaintenanceRutineOffice ToolMaintenanceRutineOffice { get; set; }
    }
}
