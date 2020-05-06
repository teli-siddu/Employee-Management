using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
    public class ApplicationUserViewModel
    {
        public ApplicationUser  ApplicationUser { get; set; }
        public string Token { get; set; }
    }
}
