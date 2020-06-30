using AutoMapper;
using Entities.Models;
using Entities.ViewModels.Employee;
using Entities.ViewModels.Leave;
using Entities.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIProject.MappingConfigurations
{
    public class DomainToViewModelMappingProfile:Profile
    {
        //public override string ProfileName => "DomainToViewModelMapping";

           
        public DomainToViewModelMappingProfile()
        {
            //CreateMap<Entities.Test.AddressViewModel, Entities.Test.Address>()
            //    .ForMember(x => x.FirstLine, opt => opt.MapFrom(source => source.PersonAddressLineOne))
            //    .ForMember(x => x.Country, opt => opt.MapFrom(source => source.PersonCountryOfResidence));
            CreateMap<MobileViewModel, Mobile>();
            CreateMap<List<MobileViewModel>, List<Mobile>>();
            CreateMap<List<EmailViewModel>, List<Email>>();
            CreateMap<EmailViewModel, Email>();
            CreateMap<AddAddressViewModel, Address>();
            CreateMap<List<AddAddressViewModel>, List<Address>>() ;
            CreateMap<AddEmployeeViewModel, Employee >();
            CreateMap<AddLeaveViewModel, Leave>();
            CreateMap<LeaveViewModel, Leave>();
            CreateMap<AddUserViewModel, Employee>();



        }

       
       
    }
}
