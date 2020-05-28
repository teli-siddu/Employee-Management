using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels.Leave
{
    public class LeaveViewModel
    {
        public int Id { get; set; }
        public string LeaveType { get; set; }
        public DateTime LeaveFrom { get; set; }
        public DateTime LeaveTo { get; set; }
        public string Description { get; set; }

    }
}
