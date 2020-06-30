using EmployeeManagementBlazorServer.Extensions;
using Entities.Helper;
using Entities.Models;
using Entities.ViewModels.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeManagementBlazorServer.Services
{
    public class DepartmentsService:IDepartmentsService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DepartmentsService(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<ReturnResult> AddDepartment(DepartmentViewModel department)
        {

            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            ReturnResult result = await httpClient.PostAsJsonAsync<DepartmentViewModel, ReturnResult>("api/Departments/AddDepartment", department);
            return result;
        }

    

        public async Task<ReturnResult> UpdateDepartment(DepartmentViewModel department)
        {
           HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
           ReturnResult result= await  httpClient.PutAsJsonAsync<DepartmentViewModel, ReturnResult>("api/Departments/UpdateDepartment", department);
           return result;

        }

        public async Task<ReturnResult> DeleteDepartmentById(int departmentId)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            ReturnResult result = await httpClient.DeleteAsJsonAsync<ReturnResult>("api/Departments/DeleteDepartment/" + departmentId);
            return result;
        }

        public Task<ReturnResult> DeleteDepartmentByName(string departmentName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<KeyValue<int, string>>> GetDepartments()
        {
            throw new NotImplementedException();
        }

        public async Task<DepartmentViewModel> GetDepartmentById(int Id)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            DepartmentViewModel department = await httpClient.GetAsJsonAsync<DepartmentViewModel>("api/Departments/GetDepartmentById/" + Id);
            return department;
        }

        public async Task<List<DepartmentViewModel>> AllDepartments()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            List<DepartmentViewModel> departments= await httpClient.GetAsJsonAsync<List<DepartmentViewModel>>("api/Departments/Departments");
            return departments;
        }
    }
}
