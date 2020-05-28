using Entities.Helper;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels.Leave
{
   public class AddLeaveViewModel
    {
        public int Id { get; set; }

        public string EmployeeId { get; set; }
        public List<KeyValue<int,string>> LeaveTypes { get; set; }

        public int LeaveTypeId { get; set; }
        public DateTime LeaveFrom { get; set; }
        public DateTime LeaveTo { get; set; }
        public string Description { get; set; }
    }
}
