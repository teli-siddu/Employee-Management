using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
   public class AccessTokenViewModel
    {
        public string Token { get; set; }
        public string TokenExpiration { get; set; }
        public string RefreshToken { get; set; }
    }
}
