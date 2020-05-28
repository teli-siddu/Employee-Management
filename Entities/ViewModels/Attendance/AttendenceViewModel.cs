using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels.Attendance
{
    public class AttendenceViewModel
    {
        public int Employee_Id { get; set; }

        public string EmployeeName { get; set; }

        public string DepartmentName { get; set; }
        public List<Absence> Absences { get; set; }
    }
}
