using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("BankDetails")]
    public class BankDetails
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string  AccountType { get; set; }
        public string AccountNumber { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
