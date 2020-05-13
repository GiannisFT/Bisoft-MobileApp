using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;

namespace BisoftMobileApp.Classes
{
    class Office
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int CompanyId { get; set; }

        public ObservableCollection<Employee> Employees { get; set; }
    }
}
