using System;
using System.Collections.Generic;
using System.Text;

namespace BisoftMobileApp.Classes.QualityReports
{
    class QRLog
    {
        public DateTime Created { get; set; }

        public Employee CreatedByEmployee { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }

        public int LogTypeId { get; set; }

        public string LogTypeText { get; set; }

        public int QRId { get; set; }

        public string Subject { get; set; }
    }
}
