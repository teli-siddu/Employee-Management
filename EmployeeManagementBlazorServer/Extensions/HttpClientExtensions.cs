using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementBlazorServer.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<TRet> PostAsJsonAsync<TInput,TRet>(this HttpClient httpClient, string uri, TInput model) 
        {
          string modelString=   JsonConvert.SerializeObject(model);
          StringContent stringContent = new StringContent(modelString, Encoding.UTF8, "application/json");

          HttpResponseMessage httpResponse= await  httpClient.PostAsync(uri, stringContent);
            if (httpResponse.IsSuccessStatusCode)
            {
                var strJson = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TRet>(strJson);
            }
            else 
            {
                return default;
            }
        
        }
        public static async Task<TRet> GetAsJsonAsync<TRet>(this HttpClient httpClient, string uri)
        {
          

            HttpResponseMessage httpResponse= await httpClient.GetAsync(uri);

            if (httpResponse.IsSuccessStatusCode)
            {
                var strJson = await httpResponse.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TRet>(strJson);
            }
            else 
            {
                return default;
            }
           
          
        }

        public static async Task<TRet> PutAsJsonAsync<TInput, TRet>(this HttpClient httpClient, string uri, TInput model)
        {
            string modelString = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(modelString, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await httpClient.PutAsync(uri, stringContent);
            if (httpResponse.IsSuccessStatusCode)
            {
                var strJson = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TRet>(strJson);
            }
            else
            {
                return default;
            }

        }
        public static async Task<TRet> DeleteAsJsonAsync<TRet>(this HttpClient httpClient, string uri)
        {


            HttpResponseMessage httpResponse = await httpClient.DeleteAsync(uri);

            if (httpResponse.IsSuccessStatusCode)
            {
                var strJson = await httpResponse.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TRet>(strJson);
            }
            else
            {
                return default;
            }


        }




    }
}
