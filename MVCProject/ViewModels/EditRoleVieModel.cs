using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVCProject.ViewModels
{
    public class EditRoleVieModel
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        [BindProperty]
        public List<RoleUsersViewModel> Users { get; set; }
    }
}
