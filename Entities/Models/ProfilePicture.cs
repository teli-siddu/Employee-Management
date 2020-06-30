using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("ProfilePictures")]
    public class ProfilePicture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string path { get; set; }

        [ForeignKey("Employee_UserId")]
        public Employee Employee { get; set; }

        public int Employee_UserId { get; set; }
    }
}
