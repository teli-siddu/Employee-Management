using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels.Attendance
{
   public class AddAttendanceViewModel
    {
        public int Employee_Id { get; set; }

        public string Designation { get; set; }
        public AbsenceViewModel AbsenceView { get; set; }
        public DateTime DateTime { get; set; } = new DateTime();
    }
}
