using EmployeeManagementBlazorServer.Extensions;
using EmployeeManagementBlazorServer.Services;
using Entities.ViewModels.Employee;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementBlazorServer.Pages.Employees
{
    public partial class EmployeeDetails
    {


        [Inject]
        public IEmployeeService  EmployeeService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        EmployeeViewModel employee =null;

        protected async override Task OnInitializedAsync()
        {
             int id;
            navigationManager.TryGetQueryString<int>("Id", out id);
            if (id != default) 
            {
              employee=  await EmployeeService.GetEmployeeDetails(id);
            }
            
        }
    }
}
