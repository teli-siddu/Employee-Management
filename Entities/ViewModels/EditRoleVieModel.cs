using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
    public class EditRoleVieModel
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<string> Users { get; set; }
    }
}
