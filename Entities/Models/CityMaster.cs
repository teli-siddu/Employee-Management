using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class CityMaster
    {
        public int Id { get; set; }
        public string City { get; set; }

        //public ICollection<PermanentAddress> PermanentAddresses { get; set; }

        public ICollection<Address> Addresses { get; set; }


        public StateMaster State { get; set; }
        public int? StateMasterId { get; set; }

    }
}
