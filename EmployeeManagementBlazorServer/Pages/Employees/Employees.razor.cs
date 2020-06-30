using EmployeeManagementBlazorServer.Services;
using Entities.ViewModels.Employee;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementBlazorServer.Pages.Employees
{
    public partial class Employees
    {

        [Inject]
        public IEmployeeService _employeeService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        List<EmployeeViewModel> employees=new List<EmployeeViewModel>();
        protected override async Task OnInitializedAsync()
        {
            
            employees = await _employeeService.Employees();
        }

        protected async Task DeleteEmployee(int id) 
        {
            var result= await _employeeService.DeleteEmployee(id);
            if (result.Succeeded) 
            {
                employees = await _employeeService.Employees();
            }
        }
    }
}
