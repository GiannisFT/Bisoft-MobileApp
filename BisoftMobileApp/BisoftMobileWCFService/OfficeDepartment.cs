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
    
    public partial class OfficeDepartment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OfficeDepartment()
        {
            this.CustomerReports = new HashSet<CustomerReport>();
            this.EnviromentalReports = new HashSet<EnviromentalReport>();
            this.ManualOfficeFastLinks = new HashSet<ManualOfficeFastLink>();
            this.ManualParts = new HashSet<ManualPart>();
            this.ManualPartDocuments = new HashSet<ManualPartDocument>();
            this.OfficeDepartmentTasks = new HashSet<OfficeDepartmentTask>();
            this.OfficeDepartmentVersions = new HashSet<OfficeDepartmentVersion>();
            this.QRResponsibles = new HashSet<QRResponsible>();
            this.QRStatusEmailNotifications = new HashSet<QRStatusEmailNotification>();
            this.QualityReports = new HashSet<QualityReport>();
        }
    
        public int OfficeId { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public bool Deleted { get; set; }
        public int Responsible { get; set; }
        public string PartContent { get; set; }
        public Nullable<int> Version { get; set; }
        public Nullable<int> ApprovedBy { get; set; }
        public Nullable<System.DateTime> Approved { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerReport> CustomerReports { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Employee Employee1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EnviromentalReport> EnviromentalReports { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ManualOfficeFastLink> ManualOfficeFastLinks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ManualPart> ManualParts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ManualPartDocument> ManualPartDocuments { get; set; }
        public virtual Office Office { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OfficeDepartmentTask> OfficeDepartmentTasks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OfficeDepartmentVersion> OfficeDepartmentVersions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QRResponsible> QRResponsibles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QRStatusEmailNotification> QRStatusEmailNotifications { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QualityReport> QualityReports { get; set; }
    }
}
