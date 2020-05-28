using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("Emails")]
    public class Email
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string EmailId { get; set; }

        [ForeignKey("Employee_UserId")]
        public Employee Employee { get; set; }

    
        public int Employee_UserId { get; set; }


    }
}
