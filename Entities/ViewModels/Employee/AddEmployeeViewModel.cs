using Entities.Helper;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels.Employee
{
   public class AddEmployeeViewModel:BaseEmployeeViewModel
    {
     
        public int? GenderId { get; set; }

        public int? MaritialStatusId { get; set; }
        public int? NationalityId { get; set; }
        public int AddressTypeId { get; set; }
        public string ProfilePictureUrl { get; set; }
        public MobileViewModel[] Mobiles1 { get; set; } = new MobileViewModel[2] { new MobileViewModel(),new MobileViewModel()};
        public EmailViewModel[] Emails1 { get; set; } = new EmailViewModel[2] {new EmailViewModel(),new EmailViewModel() };
        public List<MobileViewModel> Mobiles { get; set; } 
        public List<EmailViewModel> Emails { get; set; } 
        public int? DepartmentId { get; set; }
        public BankDetailsViewModel BankDetails { get; set; }
        public AddAddressViewModel[] Addresses1 { get; set; } =  
        {
            new AddAddressViewModel(),new AddAddressViewModel() 
        } ;
        public List<AddAddressViewModel> Addresses { get; set; }

        public List<ProfilePicture>  ProfilePictures { get; set; }


        public List<RoleViewModel> Roles { get; set; } = new List<RoleViewModel>();

        public List<KeyValue<int,string>>  Deapartments { get; set; }
        public List<KeyValue<int,string>> Genders { get; set; }
        public List<KeyValue<int,string>> MaritialStatuses { get; set; }
        public List<KeyValue<int,string>> Countries { get; set; }
        public List<KeyValue<int,string>> Nationalities { get; set; }
    }
}
