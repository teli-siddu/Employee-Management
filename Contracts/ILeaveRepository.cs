using Entities.Helper;
using Entities.ViewModels.Leave;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ILeaveRepository
    {
        Task<List<LeaveViewModel>> GetAllLeaves();
        Task<ReturnResult> AddLeave(AddLeaveViewModel addLeaveView);
        Task<ReturnResult> DeleteLeave(int id);

        Task<ReturnResult> UpdateLeave(AddLeaveViewModel addLeaveView);
    }
}
