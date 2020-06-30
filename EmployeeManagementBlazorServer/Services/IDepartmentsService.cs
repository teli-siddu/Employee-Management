using Entities.Helper;
using Entities.Models;
using Entities.ViewModels;
using Entities.ViewModels.Departments;
using Entities.ViewModels.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementBlazorServer.Services
{
    public interface IDepartmentsService
    {
        Task<ReturnResult> AddDepartment(DepartmentViewModel department);
        //Task<ReturnResult> UpdateDepartmenDepartmentt( department, int departmentId);
        Task<ReturnResult> UpdateDepartment(DepartmentViewModel department);
        Task<ReturnResult> DeleteDepartmentById(int departmentId);
        Task<ReturnResult> DeleteDepartmentByName(string departmentName);
        Task<List<KeyValue<int, string>>> GetDepartments();
        Task<DepartmentViewModel> GetDepartmentById(int Id);

        Task<List<DepartmentViewModel>> AllDepartments();

    }
}
