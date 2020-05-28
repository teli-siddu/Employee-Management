using Entities.ViewModels.Attendance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAttendanceRepository
    {

        public int MyProperty { get; set; }
        Task<List<AttendenceViewModel>> GetEmployeeAttendance(int Id);


    }
}
