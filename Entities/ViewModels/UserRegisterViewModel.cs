using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.ViewModels
{
    public class UserRegisterViewModel:UserViewModel
    {
     
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        [Compare("Password",ErrorMessage = "password and confirm password do not match")]
        public string ConfirmPassword { get; set; }

        //public List<KeyValuePair<string,string>> roles { get; set; }

    }
}
