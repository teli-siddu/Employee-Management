using EmployeeManagementBlazorServer.Services;
using Entities.Helper;
using Entities.ViewModels.Departments;
using Entities.ViewModels.Employee;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementBlazorServer.Pages.Department
{
    public partial class Departments
    {
       

        [Inject]
        public IDepartmentsService DepartmentsService { get; set; }

        List<DepartmentViewModel> departments = new List<DepartmentViewModel>();

        DepartmentViewModel DepartmentViewModel = new DepartmentViewModel();
        public bool ShowAddDepartment { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            departments= await DepartmentsService.AllDepartments();
        }

        protected async  Task AddDepartment1() 
        {
            DepartmentViewModel = new DepartmentViewModel();
            ShowAddDepartment = !ShowAddDepartment;
        }
        protected async Task EditDepartment(int id)
        {
            DepartmentViewModel = await DepartmentsService.GetDepartmentById(id);
            ShowAddDepartment = true;
        }
        protected async Task AddDepartment(Microsoft.AspNetCore.Components.Forms.EditContext context)
        {
            if (!context.Validate())
            {
                return;
            }
            ReturnResult result;
            
            if (DepartmentViewModel.Id == 0)
            {
                result = await DepartmentsService.AddDepartment(DepartmentViewModel);
            }
            else
            {
                result = await DepartmentsService.UpdateDepartment(DepartmentViewModel);
            }
            if (result.Succeeded) 
            {
                DepartmentViewModel = new DepartmentViewModel();
                ShowAddDepartment =false;
                departments = await DepartmentsService.AllDepartments();
            }
            
        }

        protected async Task DeleteDepartment(int id) 
        {
            ReturnResult result= await DepartmentsService.DeleteDepartmentById(id);
            departments.Remove(departments.Select(x => x).Where(x => x.Id == id).FirstOrDefault());
        }

        protected async Task CloseWindow() 
        {
            ShowAddDepartment = false;
        }



    }
}
