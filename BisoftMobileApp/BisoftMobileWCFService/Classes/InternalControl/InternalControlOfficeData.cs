using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BisoftMobileWCFService.Classes.InternalControl
{
    [DataContract]
    public class InternalControlOfficeData
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int OfficeId { get; set; }
        [DataMember]
        public Nullable<int> AmountBudget { get; set; }
        [DataMember]
        public string AmountUnit { get; set; }
        [DataMember]
        public int PerformedThisYear { get; set; }
        [DataMember]
        public int BudgetThisYear { get; set; }
        [DataMember]
        public int PerformedThisMonth { get; set; }
        [DataMember]
        public int BudgetThisMonth { get; set; }
        [DataMember]
        public bool HasVehicleBrand { get; set; }
        [DataMember]
        public bool HasEmployee { get; set; }

        [DataMember]
        public List<InternalControlOfficeRowData> ControlRows { get; set; }

        [DataMember]
        public List<InternalControlOfficeLogData> Logs { get; set; }

        [DataMember]
        public List<InternalControlBrandGoalData> Goals { get; set; }

    }
}