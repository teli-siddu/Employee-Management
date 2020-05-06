using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
