using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Helper
{
    public class AddEmployeeDropdowns
    {
        public List<KeyValue<int,string>> Countries { get; set; }
        public List<KeyValue<int,string>> Departments { get; set; }

        public List<KeyValue<int,string>> Genders { get; set; }

        public List<KeyValue<int,string>> MaritialStatuses { get; set; }

        public List<KeyValue<int,string>> Nationalities { get; set; }



    }
}
