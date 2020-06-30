using Contracts;
using EmployeeManagementBlazorServer.Extensions;
using Entities.Helper;
using Entities.ViewModels;
using Entities.ViewModels.Employee;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementBlazorServer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IHttpClientFactory _httpClientFactory;


        public EmployeeService(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }

        public async Task<ReturnResult> AddEmployee(AddEmployeeViewModel employee)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
            string serializedEmployee = JsonConvert.SerializeObject(employee);
            StringContent content = new StringContent(serializedEmployee, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync("api/Employees/AddEmployee", content);
            var jsonResult = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<ReturnResult>(jsonResult);
           

            return result;
        }

        public async Task<ReturnResult> DeleteEmployee(int employeeId)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            var result = await httpClient.DeleteAsJsonAsync<ReturnResult>("api/Employees/DeleteEmployee/" + employeeId);
            return result;
        }

        public async Task<List<EmployeeViewModel>> Employees()
        {


            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token")?.ToString());
            HttpResponseMessage response = await httpClient.GetAsync("api/Employees/Employees");
            var jsonResult = response.Content.ReadAsStringAsync().Result;
            var employees = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(jsonResult);

            return employees;





        }

        public Task<EmployeeViewModel> GetEmployeeById(int employeeId)
        {
            throw new NotImplementedException();
        }

        public async Task<EmployeeViewModel> GetEmployeeDetails(int id)
        {
            
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            HttpResponseMessage response = await httpClient.GetAsync("api/Employees/EmployeeDetails/" + id);
           
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var jsonResonse = JsonConvert.DeserializeObject<EmployeeViewModel>(jsonResult);
            return jsonResonse;
        }

        public async Task<AddEmployeeViewModel> GetEmployeeForEdit(int id)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            HttpResponseMessage response = await httpClient.GetAsync("api/Employees/EditEmployee/" + id);
           
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var jsonResonse = JsonConvert.DeserializeObject<AddEmployeeViewModel>(jsonResult);

            return jsonResonse;
        }

        public async Task<ReturnResult> UpdateEmployee(AddEmployeeViewModel employee)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
       
            ReturnResult result= await httpClient.PostAsJsonAsync<AddEmployeeViewModel,ReturnResult>("api/Employees/Updatemployee",employee);
            return result;

        }

        public async Task<AddEmployeeDropdowns> GetEmployeeDropdowns()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            HttpResponseMessage response = await httpClient.GetAsync("api/Dropdowns/GetAddEmployeeDropdowns");

            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var jsonResonse = JsonConvert.DeserializeObject<AddEmployeeDropdowns>(jsonResult);

            return jsonResonse;
        }

        public async Task<List<RoleViewModel>> GetEmployeeRoles()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            HttpResponseMessage response1 = await httpClient.GetAsync("api/Roles/GetRoles");
            string jsonData = response1.Content.ReadAsStringAsync().Result;
            var roles = JsonConvert.DeserializeObject<List<RoleViewModel>>(jsonData);
            return roles;
         }
    }
}
