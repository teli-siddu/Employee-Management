using Entities.Helper;
using Entities.Models;
using Entities.ViewModels.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        Task<ReturnResult> AddEmployee( AddEmployeeViewModel employee);
        Task<List<EmployeeViewModel>> Employees();
        //Task<ReturnResult> AddEmployee(Employee employee);
        Task<ReturnResult> UpdateEmployee(AddEmployeeViewModel employee);
        Task<ReturnResult> DeleteEmployee(int employeeId);

        Task<Employee> GetEmployeeById(int employeeId);
        Task<AddEmployeeViewModel> GetEmployeeForEdit(int id);

        Task<EmployeeViewModel> GetEmployeeDetails(int id);
        Task<EmployeeViewModel> GetEmployeeDetails(string userName);
        string GetHighPriorityRole(string[] roles);

    }
}
