using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
   public class StateMaster
    {
        public int Id { get; set; }
        public string State { get; set; }
        public CountryMaster Country { get; set; }

        [ForeignKey("FK_StateMaster_Country")]
        public int? CountryMasterId { get; set; }

        public  ICollection<Address> Addresses { get; set; }
        //public ICollection<PermanentAddress> PermanentAddresses { get; set; }
        public ICollection<CityMaster> Cities { get; set; }
    }
}
