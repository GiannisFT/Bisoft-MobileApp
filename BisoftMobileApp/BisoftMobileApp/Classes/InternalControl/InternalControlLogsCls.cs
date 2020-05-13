using System;
using System.Collections.Generic;
using System.Text;

namespace BisoftMobileApp.Classes.InternalControl
{
    class InternalControlLogsCls
    {
        public int Id { get; set; }
        public string Icon { get; set; }
        public DateTime Created { get; set; }
        public string CreatedString { get; set; }
        public Employee CreatedBy { get; set; }

        public string CreatedByName { get; set; }
        public string RegNr { get; set; }
        public string AoNr { get; set; }
        public string VehicleBrand { get; set; }
        public bool IsPartSaved { get; set; }
    }
}
