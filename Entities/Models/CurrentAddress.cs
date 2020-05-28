using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("CurrentAddresses")]
    public class CurrentAddress
    {
        public int Id { get; set; }
        public string DoorNo { get; set; }
        public string Street { get; set; }

        [ForeignKey("FK_CurrentAddress_City")]
        public int? CityMasterId { get; set; }
        public CityMaster City { get; set; }

        [ForeignKey("FK_CurrentAddress_State")]
        public int? StateMasterId { get; set; }
        public StateMaster State { get; set; }


        [ForeignKey("FK_CurrentAddress_Country")]
        public int? CountryMasterId { get; set; }

        public CountryMaster Country { get; set; }


        public bool PermnentAddress { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
