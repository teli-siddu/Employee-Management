using EmployeeManagementBlazorServer.Services;
using Entities.Helper;
using Entities.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementBlazorServer.Pages.Roles
{
    public partial class Roles
    {
        [Inject]
        public IRolesService RolesService { get; set; }

        EditRoleVieModel editRoleVieModel = new EditRoleVieModel();
        CreateRoleViewModel createRoleViewModel = new CreateRoleViewModel();

        List<RoleViewModel> roles = new List<RoleViewModel>();

        public bool ShowAddRole { get; set; } = false;
        protected async override Task OnInitializedAsync()
        {
            roles = await RolesService.GetRoles();
        }

        protected async Task EditRole(int id) 
        {
            createRoleViewModel=  await RolesService.GetRoleById(id);
            ShowAddRole = true;
        }

        protected async Task AddRole(EditContext editContext)
        {
            if (!editContext.Validate()) 
            {
                return;
            }
            ReturnResult result;
            if (createRoleViewModel.RoleId == 0)
            {
                 result = await RolesService.AddRole(createRoleViewModel);
                
               

            }
            else 
            {
              result=  await RolesService.UpdateRole(createRoleViewModel);
            }
            if (result!=null && result.Succeeded)
            {
                roles = await RolesService.GetRoles();
                ShowAddRole = false;
            }

        }

        protected void ShowHideAddRole() 
        {
            createRoleViewModel=new CreateRoleViewModel();
            ShowAddRole = !ShowAddRole;
        }
        protected async Task DeleteRole(int roleId)
        {
            ReturnResult result= await RolesService.DeleteRole(roleId);
            if (result!=null && result.Succeeded)
            {
                roles.Remove(roles.Select(x => x).Where(x => x.RoleId == roleId).FirstOrDefault());
            }

        }
        protected void CloseWindow()
        {
            createRoleViewModel = new CreateRoleViewModel();
            ShowAddRole = false;
        }
    }
}
