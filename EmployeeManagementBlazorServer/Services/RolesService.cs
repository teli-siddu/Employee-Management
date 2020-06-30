using EmployeeManagementBlazorServer.Extensions;
using Entities.Helper;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeManagementBlazorServer.Services
{
    public class RolesService : IRolesService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RolesService(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<ReturnResult> AddRole(CreateRoleViewModel createRoleViewModel)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            return await httpClient.PostAsJsonAsync<CreateRoleViewModel, ReturnResult>("api/Roles/CreateRole", createRoleViewModel);
        }

        public async Task<ReturnResult> DeleteRole(int roleId)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            return await httpClient.DeleteAsJsonAsync<ReturnResult>("api/Roles/DeleteRole/" + roleId);
        }

        public Task<ReturnResult> DeleteRoleById(int roleId)
        {
            throw new NotImplementedException();
           
        }

        public async Task<ReturnResult> EditRole(EditRoleVieModel editRoleVieModel)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            return await httpClient.PutAsJsonAsync<EditRoleVieModel, ReturnResult>("api/Users/UpdateUsersInRole", editRoleVieModel);
        }
        public async Task<EditRoleVieModel> EditRole(int roleId)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            return await httpClient.GetAsJsonAsync<EditRoleVieModel>("api/Users/EditUsersInRole/" + roleId);
        }

        public Task<CreateRoleViewModel> FindRoleById(int roleId)
        {
            throw new NotImplementedException();
        }

        public async Task<CreateRoleViewModel> GetRoleById(int roleId)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            return await httpClient.GetAsJsonAsync<CreateRoleViewModel>("api/Roles/GetRoleById/" + roleId);
        }

        public async Task<List<RoleViewModel>> GetRoles()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            return await httpClient.GetAsJsonAsync<List<RoleViewModel>>("api/Roles/GetRoles");
        }

        public async Task<ReturnResult> UpdateRole(CreateRoleViewModel createRoleViewModel)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("EmpMGMTClient");
            return await httpClient.PutAsJsonAsync<CreateRoleViewModel, ReturnResult>("api/Roles/UpdateRole", createRoleViewModel);
        }
    }
}
