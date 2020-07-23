using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BisoftMobileApp.Classes.QualityReports
{
    class OfficeDepartments
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int OfficeId { get; set; }

        public int ResponsibleEmployeeId { get; set; }

        public ObservableCollection<OfficeDepartmentTasks> OfficeDepartmentTasks { get; set; }
    }
}
