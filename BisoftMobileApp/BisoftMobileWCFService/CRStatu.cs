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
    
    public partial class CRStatu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CRStatu()
        {
            this.CRLogMeasureReports = new HashSet<CRLogMeasureReport>();
            this.CRMeasureReports = new HashSet<CRMeasureReport>();
            this.CRMeasureReports1 = new HashSet<CRMeasureReport>();
            this.CRModulesOffices = new HashSet<CRModulesOffice>();
            this.CRStatusEmailNotifications = new HashSet<CRStatusEmailNotification>();
            this.CRStatusOffices = new HashSet<CRStatusOffice>();
            this.CRStatusOffices1 = new HashSet<CRStatusOffice>();
            this.CustomerReportPositives = new HashSet<CustomerReportPositive>();
            this.CustomerReports = new HashSet<CustomerReport>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMeasureEnabled { get; set; }
        public string Icon { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CRLogMeasureReport> CRLogMeasureReports { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CRMeasureReport> CRMeasureReports { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CRMeasureReport> CRMeasureReports1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CRModulesOffice> CRModulesOffices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CRStatusEmailNotification> CRStatusEmailNotifications { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CRStatusOffice> CRStatusOffices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CRStatusOffice> CRStatusOffices1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerReportPositive> CustomerReportPositives { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerReport> CustomerReports { get; set; }
    }
}
