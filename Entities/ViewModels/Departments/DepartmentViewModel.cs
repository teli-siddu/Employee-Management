using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.ViewModels.Departments
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="This field is required")]
        public string Name { get; set; }
    }
}
