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
    
    public partial class OfficeDepartmentTaskGroupRow
    {
        public int Id { get; set; }
        public int OfficeDepartmentTaskId { get; set; }
        public int OfficeDepartmentTaskGroupId { get; set; }
        public int OrderNr { get; set; }
    
        public virtual OfficeDepartmentTask OfficeDepartmentTask { get; set; }
        public virtual OfficeDepartmentTaskGroup OfficeDepartmentTaskGroup { get; set; }
    }
}