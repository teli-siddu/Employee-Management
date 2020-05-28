using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
    public class EditRoleVieModel
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
       
        public List<RoleUsersViewModel> Users { get; set; }
    }
}
