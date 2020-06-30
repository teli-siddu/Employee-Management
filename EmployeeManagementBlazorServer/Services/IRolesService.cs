using Entities.Helper;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementBlazorServer.Services
{
    public interface IRolesService
    {
        Task<ReturnResult> AddRole(CreateRoleViewModel createRoleViewModel);

        Task<ReturnResult> DeleteRole(int roleId);

        Task<ReturnResult> DeleteRoleById(int roleId);
        Task<ReturnResult> EditRole(EditRoleVieModel createRoleViewModel);
        Task<CreateRoleViewModel> GetRoleById(int roleId);
        Task<CreateRoleViewModel> FindRoleById(int roleId);
        Task<List<RoleViewModel>> GetRoles();
        Task<ReturnResult> UpdateRole(CreateRoleViewModel createRoleViewModel);
    }
}
