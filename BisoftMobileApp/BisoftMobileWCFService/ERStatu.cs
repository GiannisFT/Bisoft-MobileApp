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
    
    public partial class ERStatu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ERStatu()
        {
            this.EnviromentalReports = new HashSet<EnviromentalReport>();
            this.ERModuleOffices = new HashSet<ERModuleOffice>();
            this.EROwnMeasures = new HashSet<EROwnMeasure>();
            this.ERReportMeasures = new HashSet<ERReportMeasure>();
            this.ERReportMeasures1 = new HashSet<ERReportMeasure>();
            this.ERStatusEmailNotifications = new HashSet<ERStatusEmailNotification>();
            this.ERStatusOffices = new HashSet<ERStatusOffice>();
            this.ERStatusOffices1 = new HashSet<ERStatusOffice>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMeasureEnabled { get; set; }
        public string Icon { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EnviromentalReport> EnviromentalReports { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ERModuleOffice> ERModuleOffices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EROwnMeasure> EROwnMeasures { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ERReportMeasure> ERReportMeasures { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ERReportMeasure> ERReportMeasures1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ERStatusEmailNotification> ERStatusEmailNotifications { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ERStatusOffice> ERStatusOffices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ERStatusOffice> ERStatusOffices1 { get; set; }
    }
}