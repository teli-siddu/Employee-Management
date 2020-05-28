using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels.Attendance
{
    public class AbsenceViewModel
    {
        public bool IsAbsent { get; set; } = false;
        public string Reason { get; set; }

    }
}
