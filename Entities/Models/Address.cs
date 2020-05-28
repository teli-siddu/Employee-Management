using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
   public class Address
    {
        public int Id { get; set; }
        public string LandMark { get; set; }
       

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

        [ForeignKey("Employee_UserId")]
        public Employee Employee { get; set; }

        public int? Employee_UserId { get; set; }

        [ForeignKey("Address_addressType")]
        public int? AddressTypeId { get; set; }

        public AddressType AddressType { get; set; }
    }
}
