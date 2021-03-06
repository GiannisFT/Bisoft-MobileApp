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
    
    public partial class ManualPart
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ManualPart()
        {
            this.EmployeeManualPartDocs = new HashSet<EmployeeManualPartDoc>();
            this.ManualOfficeFastLinks = new HashSet<ManualOfficeFastLink>();
            this.ManualPart1 = new HashSet<ManualPart>();
            this.ManualPart11 = new HashSet<ManualPart>();
            this.ManualPartDocuments = new HashSet<ManualPartDocument>();
            this.ManualPartVersions = new HashSet<ManualPartVersion>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string PartContent { get; set; }
        public int Version { get; set; }
        public Nullable<int> ParentId { get; set; }
        public int OrderPostion { get; set; }
        public Nullable<int> ManualPartTemplateId { get; set; }
        public int ManualId { get; set; }
        public bool Deleted { get; set; }
        public System.DateTime Approved { get; set; }
        public int ApprovedBy { get; set; }
        public bool IsOnlyHeader { get; set; }
        public Nullable<int> OfficeDepartmentTaskId { get; set; }
        public Nullable<bool> IsLink { get; set; }
        public Nullable<int> ManualPartLinkId { get; set; }
        public Nullable<int> OfficeDepartmentId { get; set; }
        public Nullable<int> OfficeDepartmentTaskGroupId { get; set; }
        public Nullable<int> RadDocumentId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeManualPartDoc> EmployeeManualPartDocs { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Manual Manual { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ManualOfficeFastLink> ManualOfficeFastLinks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ManualPart> ManualPart1 { get; set; }
        public virtual ManualPart ManualPart2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ManualPart> ManualPart11 { get; set; }
        public virtual ManualPart ManualPart3 { get; set; }
        public virtual ManualPartTemplate ManualPartTemplate { get; set; }
        public virtual ManualRadDocument ManualRadDocument { get; set; }
        public virtual OfficeDepartment OfficeDepartment { get; set; }
        public virtual OfficeDepartmentTask OfficeDepartmentTask { get; set; }
        public virtual OfficeDepartmentTaskGroup OfficeDepartmentTaskGroup { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ManualPartDocument> ManualPartDocuments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ManualPartVersion> ManualPartVersions { get; set; }
    }
}
