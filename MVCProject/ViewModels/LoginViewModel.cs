using System;
using System.Collections.Generic;
using System.Text;

namespace MVCProject.ViewModels
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
