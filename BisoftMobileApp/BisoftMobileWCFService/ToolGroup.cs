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
    
    public partial class ToolGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ToolGroup()
        {
            this.ToolGroupInventories = new HashSet<ToolGroupInventory>();
            this.ToolGroupRows = new HashSet<ToolGroupRow>();
            this.ToolSettingsOffices = new HashSet<ToolSettingsOffice>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int OfficeId { get; set; }
        public Nullable<bool> IsDekra { get; set; }
        public Nullable<bool> IsCarPart { get; set; }
    
        public virtual Office Office { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ToolGroupInventory> ToolGroupInventories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ToolGroupRow> ToolGroupRows { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ToolSettingsOffice> ToolSettingsOffices { get; set; }
    }
}