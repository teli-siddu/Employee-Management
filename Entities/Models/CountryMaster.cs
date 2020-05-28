using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class CountryMaster
    {
        public int Id { get; set; }
        public string Country { get; set; }

        public ICollection<Address> Addresses { get; set; }
        //public ICollection<PermanentAddress> PermanentAddresses { get; set; }
        public ICollection<StateMaster> States { get; set; }
    }
}
