using Entities.Helper;
using Entities.ViewModels;
using Entities.ViewModels.Employee;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeManagementBlazorServer.Services
{
    public interface IEmployeeService
    {
        Task<ReturnResult> AddEmployee(AddEmployeeViewModel employee);
        Task<List<EmployeeViewModel>> Employees();
        //Task<ReturnResult> AddEmployee(Employee employee);
        Task<ReturnResult> UpdateEmployee(AddEmployeeViewModel employee);
        Task<ReturnResult> DeleteEmployee(int employeeId);

        Task<EmployeeViewModel> GetEmployeeById(int employeeId);
        Task<AddEmployeeViewModel> GetEmployeeForEdit(int id);

        Task<EmployeeViewModel> GetEmployeeDetails(int id);
        Task<AddEmployeeDropdowns> GetEmployeeDropdowns();
        Task<List<RoleViewModel>> GetEmployeeRoles();
        //Task<List<KeyValue<int, string>>> GetStates(int id);
        //Task<List<KeyValue<int, string>>> GetCities(int id);
    }
}
