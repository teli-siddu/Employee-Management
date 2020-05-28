using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Entities.Test
{
    public class AddressViewModel
    {
        [DisplayName("Address Line One")]
        public string PersonAddressLineOne { get; set; }
        [DisplayName("Country Of Residence")]
        public string PersonCountryOfResidence { get; set; }
        //[DisplayName("Post Code")]
        //public string PostCode { get; set; }
    }
}
