using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementBlazorServer.Extensions
{
    public static class NavigationManagerExtensions
    {
        public static bool TryGetQueryString<T>(this NavigationManager navigationManager, string key, out T value) 
        {
            
            var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue(key, out var valueFromQuerySTring)) 
            {
                if (typeof(T) == typeof(int) && int.TryParse(valueFromQuerySTring, out var valueAsInt)) 
                {
                    value = (T)(object)valueAsInt;
                    return true;
                }

                if (typeof(T) == typeof(string))
                {
                    value = (T)(object)valueFromQuerySTring.ToString();
                    return true;
                }

                if (typeof(T) == typeof(Decimal) && Decimal.TryParse(valueFromQuerySTring, out var valueAsDecimal)) 
                {
                    value = (T)(object)valueAsDecimal;
                }

            }
            value = default;
            return false;
        }
    }
}
