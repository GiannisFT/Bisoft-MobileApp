using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BisoftMobileApp.Classes.QualityReports
{
    class QualityReport
    {
        public int Id { get; set; }

        public int? AnalysisCausedById { get; set; }

        public string AnalysisText { get; set; }

        public string AoNr { get; set; }

        public DateTime DateCreated { get; set; }

        public Employee CreatedByEmployee { get; set; }

        public ObservableCollection<Employee> CausedByEmployees { get; set; }

        public int CustomerReportId { get; set; }

        public bool Deleted { get; set; }

        public string Description { get; set; }

        public int? FinalDecisionCost { get; set; }

        public string FinalDecisionText { get; set; }

        public bool IsRepeatRepair { get; set; }

        public int MyProperty { get; set; }

        public int OfficeDepartmentTaskId { get; set; }

        public string Department { get; set; }

        public string DepartmentTask { get; set; }

        public int OfficeId { get; set; }

        public Office Office { get; set; }

        public string OfficeName { get; set; }

        public string OfficeAddress { get; set; }

        public int? QRAnalysisHeaderId { get; set; }

        public string QRAnalysisHeader { get; set; }

        public QRAttachedFile QRAttachedFile { get; set; }

        public int? QRFinalDecisionHeaderId { get; set; }

        public string QRFinalDecisionHeader { get; set; }

        public Employee ResponsibleEmployee { get; set; }

        public string ResponsibleName { get; set; }

        public string RegNr { get; set; }

        public int ReportNr { get; set; }

        public string Status { get; set; }

        public int StatusId { get; set; }

        public int? TotalCost { get; set; }

        public int Year { get; set; }

        public string NextMeasure { get; set; }

        public DateTime NextMeasureDate { get; set; }

        public string Interval { get; set; }

        public ObservableCollection<QRLog> QRLog { get; set; }
    }
}
