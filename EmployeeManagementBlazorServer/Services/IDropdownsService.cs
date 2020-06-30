using Entities.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementBlazorServer.Services
{
    public interface IDropdownsService
    {
        public Task<List<KeyValue<int, string>>> Departments();
        public Task<List<KeyValue<int, string>>> Genders();
        Task<List<KeyValue<int, string>>> MaritialStatuses();

        public Task<List<KeyValue<int, string>>> Cities(int StateId);
        public Task<List<KeyValue<int, string>>> States(int CountryId);
        public Task<List<KeyValue<int, string>>> Countries();
        public Task<List<KeyValue<int, string>>> Nationalities();

        Task<List<KeyValue<int, string>>> LeaveTypes();
    }
}
