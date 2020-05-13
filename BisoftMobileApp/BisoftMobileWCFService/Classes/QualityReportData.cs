using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BisoftMobileWCFService.Classes
{
    [DataContract]
    public class QualityReportData
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int MyProperty { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int ReportNr { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public DateTime Created { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public int QRReportResponsibleId { get; set; }
        [DataMember]
        public int Year { get; set; }
        [DataMember]
        public int OfficeId { get; set; }
        [DataMember]
        public int StatusId { get; set; }
        [DataMember]
        public Nullable<int> QRAnalysisHeaderId { get; set; }
        [DataMember]
        public string AnalysisText { get; set; }
        [DataMember]
        public Nullable<int> TotalCost { get; set; }
        [DataMember]
        public string FinalDecisionText { get; set; }
        [DataMember]
        public Nullable<int> QRFinalDecisionHeaderId { get; set; }
        [DataMember]
        public Nullable<int> FinalDecisionCost { get; set; }
        [DataMember]
        public bool Deleted { get; set; }
        [DataMember]
        public Nullable<int> AnalysisCausedById { get; set; }
        [DataMember]
        public int CustomerReportId { get; set; }
        [DataMember]
        public string RegNr { get; set; }
        [DataMember]
        public string AoNr { get; set; }
        [DataMember]
        public int OfficeDepartmentTaskId { get; set; }
        [DataMember]
        public List<QRAttachedFileData> QRAttachedFileData { get; set; }


    }
}