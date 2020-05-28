using Entities.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels.Attendance
{
    class AddAttendanceViewModelWrapper
    {
        public List<KeyValue<int,string>> Departments { get; set; }

        //public List<AttendenceViewModel> AttendenceViews { get; set; }
    }
}
