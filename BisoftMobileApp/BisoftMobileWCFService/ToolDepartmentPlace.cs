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
    
    public partial class ToolDepartmentPlace
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ToolDepartmentPlace()
        {
            this.Properties = new HashSet<Property>();
            this.Tools = new HashSet<Tool>();
            this.ToolBorroweds = new HashSet<ToolBorrowed>();
            this.ToolDepartmentPlaceLocations = new HashSet<ToolDepartmentPlaceLocation>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int ToolDepartmentId { get; set; }
        public bool Deleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Property> Properties { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tool> Tools { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ToolBorrowed> ToolBorroweds { get; set; }
        public virtual ToolDepartment ToolDepartment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ToolDepartmentPlaceLocation> ToolDepartmentPlaceLocations { get; set; }
    }
}
