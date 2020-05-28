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
    public class ViewToDomainMappingProfile:Profile
    {
        public ViewToDomainMappingProfile()
        {
            
            CreateMap<Mobile,MobileViewModel>();
            CreateMap< List <Mobile> ,List <MobileViewModel>>();
            CreateMap< List < Email > ,List <EmailViewModel> >();
            CreateMap<Email,EmailViewModel>();
            CreateMap<Address,AddAddressViewModel>();
            CreateMap< List < Address > ,List <AddAddressViewModel> >();
            CreateMap<Employee,AddEmployeeViewModel>();
            CreateMap<Leave,AddLeaveViewModel>();
            CreateMap<Leave,LeaveViewModel>();
            CreateMap<Employee,AddUserViewModel>();

        }
          
    }
}
