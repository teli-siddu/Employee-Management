using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Helper
{
    public class ApiResponse
    {
        private readonly bool succeded;
        private readonly string[] _errors;
        public ApiResponse(bool succeded,String[] errors)
        {
            this.succeded = succeded;
            this._errors = errors;
        }
        public ApiResponse()
        {
            _errors = new string[] { };
        }

        public bool Succeeded { get; set; }
        public IEnumerable<APIError> Errors { get; set; }

        
    }
}
