using EmployeeManagementBlazorServer.Extensions;
using Entities.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeManagementBlazorServer.Services
{
    public class DropdownsService : IDropdownsService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public DropdownsService(IHttpClientFactory httpClient)
        {
            this.httpClientFactory = httpClient;
        }
        public async Task<List<KeyValue<int, string>>> Cities(int StateId)
        {
             var httpClent= httpClientFactory.CreateClient("EmpMGMTClient");
             HttpResponseMessage httpResponse = await httpClent.GetAsync("api/Dropdowns/Cities/" + StateId);
            if (httpResponse.IsSuccessStatusCode)
            {

               return JsonConvert.DeserializeObject<List<KeyValue<int, string>>>(await httpResponse.Content.ReadAsStringAsync());
            }
            else 
            {
                return  new List<KeyValue<int, string>>();
            }
           
           
           
           
        }

        public async Task<List<KeyValue<int, string>>> Countries()
        {
            var httpClent = httpClientFactory.CreateClient("");
            HttpResponseMessage httpResponse = await httpClent.GetAsync("");
            if (httpResponse.IsSuccessStatusCode)
            {

              return  JsonConvert.DeserializeObject<List<KeyValue<int, string>>>(await httpResponse.Content.ReadAsStringAsync());
            }
            else
            {
                return new List<KeyValue<int, string>>();
            }
        }

        public async Task<List<KeyValue<int, string>>> Departments()
        {
            var httpClent = httpClientFactory.CreateClient("");
            HttpResponseMessage httpResponse = await httpClent.GetAsync("");
            if (httpResponse.IsSuccessStatusCode)
            {

                return JsonConvert.DeserializeObject<List<KeyValue<int, string>>>(await httpResponse.Content.ReadAsStringAsync());
            }
            else
            {
                return new List<KeyValue<int, string>>();
            }
        }

        public async Task<List<KeyValue<int, string>>> Genders()
        {
            var httpClent = httpClientFactory.CreateClient("");
            HttpResponseMessage httpResponse = await httpClent.GetAsync("");
            if (httpResponse.IsSuccessStatusCode)
            {

                return JsonConvert.DeserializeObject<List<KeyValue<int, string>>>(await httpResponse.Content.ReadAsStringAsync());
            }
            else
            {
                return new List<KeyValue<int, string>>();
            }
        }

        public async Task<List<KeyValue<int, string>>> LeaveTypes()
        {
            var httpClent = httpClientFactory.CreateClient("");
            HttpResponseMessage httpResponse = await httpClent.GetAsync("");
            if (httpResponse.IsSuccessStatusCode)
            {

                return JsonConvert.DeserializeObject<List<KeyValue<int, string>>>(await httpResponse.Content.ReadAsStringAsync());
            }
            else
            {
                return new List<KeyValue<int, string>>();
            }
        }

        public async Task<List<KeyValue<int, string>>> MaritialStatuses()
        {
            var httpClent = httpClientFactory.CreateClient("");
            HttpResponseMessage httpResponse = await httpClent.GetAsync("");
            if (httpResponse.IsSuccessStatusCode)
            {

                return JsonConvert.DeserializeObject<List<KeyValue<int, string>>>(await httpResponse.Content.ReadAsStringAsync());
            }
            else
            {
                return new List<KeyValue<int, string>>();
            }
        }

        public async Task<List<KeyValue<int, string>>> Nationalities()
        {
            var httpClent = httpClientFactory.CreateClient("");
            HttpResponseMessage httpResponse = await httpClent.GetAsync("");
            if (httpResponse.IsSuccessStatusCode)
            {

                return JsonConvert.DeserializeObject<List<KeyValue<int, string>>>(await httpResponse.Content.ReadAsStringAsync());
            }
            else
            {
                return new List<KeyValue<int, string>>();
            }
        }

        public async Task<List<KeyValue<int, string>>> States(int CountryId)
        {
            var httpClent = httpClientFactory.CreateClient("EmpMGMTClient");
            HttpResponseMessage httpResponse = await httpClent.GetAsync("api/Dropdowns/States/" + CountryId);
            if (httpResponse.IsSuccessStatusCode)
            {

                return JsonConvert.DeserializeObject<List<KeyValue<int, string>>>(await httpResponse.Content.ReadAsStringAsync());
            }
            else
            {
                return new List<KeyValue<int, string>>();
            }
        }
    }
}
