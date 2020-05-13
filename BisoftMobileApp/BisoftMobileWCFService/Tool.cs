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
    
    public partial class Tool
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tool()
        {
            this.ACReports = new HashSet<ACReport>();
            this.Tool1 = new HashSet<Tool>();
            this.ToolBorroweds = new HashSet<ToolBorrowed>();
            this.ToolCalibrationDocuments = new HashSet<ToolCalibrationDocument>();
            this.ToolGroupInventoryLogRows = new HashSet<ToolGroupInventoryLogRow>();
            this.ToolGroupRows = new HashSet<ToolGroupRow>();
            this.ToolLogs = new HashSet<ToolLog>();
            this.ToolMaintenanceDocuments = new HashSet<ToolMaintenanceDocument>();
            this.ToolMaintenanceRutineDeviants = new HashSet<ToolMaintenanceRutineDeviant>();
            this.ToolManualPartDocuments = new HashSet<ToolManualPartDocument>();
            this.ToolReplacings = new HashSet<ToolReplacing>();
            this.ToolWebLinks = new HashSet<ToolWebLink>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string SerieNr { get; set; }
        public string CalibrationDoc { get; set; }
        public string CalibrationIntervalUnit { get; set; }
        public Nullable<int> CalibrationIntervalNr { get; set; }
        public Nullable<System.DateTime> CalibrationNext { get; set; }
        public int EmployeeId { get; set; }
        public string ExternalNr { get; set; }
        public string MaintenanceIntervalUnit { get; set; }
        public Nullable<int> MaintenanceIntervalNr { get; set; }
        public string MaintenanceDoc { get; set; }
        public Nullable<System.DateTime> MaintenanceNext { get; set; }
        public Nullable<int> ManufacturerId { get; set; }
        public int Nr { get; set; }
        public int OfficeId { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<int> ProductTypeId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public int StatusId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> DepartmentPlaceId { get; set; }
        public Nullable<System.DateTime> BoughtDate { get; set; }
        public bool BorrowedOut { get; set; }
        public string MaintenanceInstructions { get; set; }
        public Nullable<int> ToolCalibrationFormId { get; set; }
        public Nullable<int> ToolMaintenanceRutineId { get; set; }
        public Nullable<int> ToolMaintenanceRutineOfficeId { get; set; }
        public Nullable<int> ToolMaintenanceRutineCompanyId { get; set; }
        public string VAGToolNr { get; set; }
        public string VAGOrderNr { get; set; }
        public Nullable<System.DateTime> CalibrationReminderDate { get; set; }
        public Nullable<System.DateTime> MaintenanceReminderDate { get; set; }
        public Nullable<int> CalibrationResponsibleId { get; set; }
        public Nullable<int> MaintenanceResponsibleId { get; set; }
        public Nullable<int> NrOfTools { get; set; }
        public Nullable<int> DepartmentPlaceLocationId { get; set; }
        public Nullable<int> ManualPartDocumentId { get; set; }
        public Nullable<bool> IsSwedac { get; set; }
        public Nullable<int> CalibrationIsApprovedBy { get; set; }
        public Nullable<bool> OnAcReport { get; set; }
        public Nullable<int> ACReportGasId { get; set; }
        public string InfoText { get; set; }
        public string ToolLink { get; set; }
        public Nullable<bool> IsMaintenance { get; set; }
        public Nullable<int> ConnectedToolId { get; set; }
        public Nullable<bool> IsCalibrationDocMandantory { get; set; }
        public Nullable<int> BorrowToOfficeId { get; set; }
        public Nullable<int> ToolCalibratorId { get; set; }
        public Nullable<bool> IsOutOfOrder { get; set; }
        public Nullable<bool> IsInWarehouse { get; set; }
        public Nullable<int> MaintenanceCount { get; set; }
        public Nullable<int> MaintenanceToolCalibratorId { get; set; }
        public string ToolDocument { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACReport> ACReports { get; set; }
        public virtual ACReportGa ACReportGa { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Employee Employee1 { get; set; }
        public virtual Employee Employee2 { get; set; }
        public virtual Employee Employee3 { get; set; }
        public virtual ManualPartDocument ManualPartDocument { get; set; }
        public virtual Office Office { get; set; }
        public virtual Office Office1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tool> Tool1 { get; set; }
        public virtual Tool Tool2 { get; set; }
        public virtual ToolCalibrationForm ToolCalibrationForm { get; set; }
        public virtual ToolCalibrator ToolCalibrator { get; set; }
        public virtual ToolCalibrator ToolCalibrator1 { get; set; }
        public virtual ToolDepartment ToolDepartment { get; set; }
        public virtual ToolDepartmentPlace ToolDepartmentPlace { get; set; }
        public virtual ToolDepartmentPlaceLocation ToolDepartmentPlaceLocation { get; set; }
        public virtual ToolMaintenanceRutine ToolMaintenanceRutine { get; set; }
        public virtual ToolMaintenanceRutineCompany ToolMaintenanceRutineCompany { get; set; }
        public virtual ToolMaintenanceRutineOffice ToolMaintenanceRutineOffice { get; set; }
        public virtual ToolManufacturer ToolManufacturer { get; set; }
        public virtual ToolProduct ToolProduct { get; set; }
        public virtual ToolProductType ToolProductType { get; set; }
        public virtual ToolStatu ToolStatu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ToolBorrowed> ToolBorroweds { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ToolCalibrationDocument> ToolCalibrationDocuments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ToolGroupInventoryLogRow> ToolGroupInventoryLogRows { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ToolGroupRow> ToolGroupRows { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ToolLog> ToolLogs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ToolMaintenanceDocument> ToolMaintenanceDocuments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ToolMaintenanceRutineDeviant> ToolMaintenanceRutineDeviants { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ToolManualPartDocument> ToolManualPartDocuments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ToolReplacing> ToolReplacings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ToolWebLink> ToolWebLinks { get; set; }
    }
}
