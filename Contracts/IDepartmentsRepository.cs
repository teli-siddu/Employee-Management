using Entities.Helper;
using Entities.Models;
using Entities.ViewModels;
using Entities.ViewModels.Departments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
   public  interface IDepartmentsRepository
    {
        Task<ReturnResult> AddDepartment(Department department);
        Task<ReturnResult> UpdateDepartment(Department department, int departmentId);
        Task<ReturnResult> UpdateDepartment(Department department);
        Task<ReturnResult> DeleteDepartmentById(int departmentId);
        Task<ReturnResult> DeleteDepartmentByName(string departmentName);
        Task<List<KeyValue<int,string>>> GetDepartments();
        Task<DepartmentViewModel> GetDepartmentById(int Id);

        Task<List<DepartmentViewModel>> AllDepartments();




    }
}
