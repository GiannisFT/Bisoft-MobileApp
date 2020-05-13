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
    
    public partial class IncidentReport
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IncidentReport()
        {
            this.IncidentReportDocuments = new HashSet<IncidentReportDocument>();
            this.IncidentReportLogs = new HashSet<IncidentReportLog>();
            this.IncidentReportOutsiders = new HashSet<IncidentReportOutsider>();
            this.IncidentReportVictums = new HashSet<IncidentReportVictum>();
        }
    
        public int Id { get; set; }
        public System.DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime IncidentDateTime { get; set; }
        public int ResponsibleId { get; set; }
        public int ReportTypeId { get; set; }
        public Nullable<int> LocationId { get; set; }
        public string LocationDescription { get; set; }
        public Nullable<int> IncidentId { get; set; }
        public string IncidentDescription { get; set; }
        public Nullable<int> InjuryTypeId { get; set; }
        public string InjuryTypeDecription { get; set; }
        public Nullable<int> FinalDecisionId { get; set; }
        public string FinalDecisionDescription { get; set; }
        public Nullable<int> WorkTaskId { get; set; }
        public string WorkTaskDescription { get; set; }
        public int OfficeId { get; set; }
        public bool Deleted { get; set; }
        public string FinalDecisionSuggestion { get; set; }
        public int StatusId { get; set; }
        public int Nr { get; set; }
        public int Year { get; set; }
        public Nullable<bool> FinalApproved { get; set; }
        public Nullable<int> FinalApprovedById { get; set; }
        public string NotFinalApprovedText { get; set; }
        public Nullable<System.DateTime> FinalApproveDate { get; set; }
        public Nullable<bool> IsArbVerketContacted { get; set; }
        public Nullable<bool> IsFKassanContacted { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Employee Employee1 { get; set; }
        public virtual Employee Employee2 { get; set; }
        public virtual IncidentReportFinalDecision IncidentReportFinalDecision { get; set; }
        public virtual IncidentReportIncident IncidentReportIncident { get; set; }
        public virtual IncidentReportInjuryType IncidentReportInjuryType { get; set; }
        public virtual IncidentReportLocation IncidentReportLocation { get; set; }
        public virtual IncidentReportStatu IncidentReportStatu { get; set; }
        public virtual IncidentReportType IncidentReportType { get; set; }
        public virtual IncidentReportWorkTask IncidentReportWorkTask { get; set; }
        public virtual Office Office { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IncidentReportDocument> IncidentReportDocuments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IncidentReportLog> IncidentReportLogs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IncidentReportOutsider> IncidentReportOutsiders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IncidentReportVictum> IncidentReportVictums { get; set; }
    }
}