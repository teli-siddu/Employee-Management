using AutoMapper;
using Contracts;
using Entities;
using Entities.Helper;
using Entities.Models;
using Entities.ViewModels;
using Entities.ViewModels.Employee;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        private readonly IDropdownsRepository _dropdownsRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;

        public EmployeeRepository(RepositoryContext repositoryContext,IDropdownsRepository dropdownsRepository,IMapper mapper,Microsoft.AspNetCore.Identity.UserManager<Employee> userManager):base(repositoryContext)
        {
            this._dropdownsRepository = dropdownsRepository;
            this._mapper = mapper;
            this._userManager = userManager;
            this._dropdownsRepository = dropdownsRepository;
        }
        public async  Task<ReturnResult> AddEmployee(AddEmployeeViewModel employeeView)
        {


            //RepositoryContext.Addresses.AddRange(new List<Address>
            //{
            //    new Address
            //    {
            //        CountryMasterId=1,
            //        CityMasterId=2,
            //        StateMasterId=2,
            //        AddressTypeId=1,
            //        LandMark="sdsa",
            //        Employee_Id=1
            //    }
            //});
            //RepositoryContext.SaveChanges();

            try
            {
                Employee employee = _mapper.Map<Employee>(employeeView);
                IdentityResult userResult= await _userManager.CreateAsync(employee,employeeView.Password);
                //Create(employee);
                //employee= await _userManager.FindByNameAsync(employee.UserName);
                IdentityResult result = await _userManager.AddToRolesAsync(employee, employeeView.Roles.Where(x=>x.IsSelected).Select(x=>x.RoleName).ToArray());
                await SaveChangesAsync();
                return new ReturnResult
                {
                    Error = "",
                    Succeeded = true
                };

            }
            catch (Exception x) 
            {
                return new ReturnResult
                {
                    Error = x.Message,
                    Succeeded = false
                };
            }
           
            
        }

        public async Task<ReturnResult> DeleteEmployee(int employeeId)
        {
            ReturnResult result;



            Employee employeeExists = await GetEmployeeById(employeeId);

         


            if (employeeExists != null) 
            {
                
                Delete(employeeExists);
                await RepositoryContext.SaveChangesAsync();
                return new ReturnResult
                {
                    Error = "",
                    Succeeded = true
                };
            }
                        
           
           return new  ReturnResult
            {
                Error = "Employee deos not exist",
                Succeeded = false
            };

        }

        public async  Task<List<EmployeeViewModel>> Employees()
        {
            List<EmployeeViewModel> employeeViewModels = await FindAll()
               //.Where(emp => emp.UserRoles.Any(roles => roles.Role.NormalizedName == "Employee"))
               .Select(x => new EmployeeViewModel()
               {
                   DateOfBirth = x.DateOfBirth,
                   FirstName = x.FirstName,
                   Designation = x.Designation,
                   Mobiles = x.Mobiles.Select(x => new MobileViewModel { MobileNumber = x.MobileNumber }).ToList(),
                   Emails = x.Emails.Select(x => new EmailViewModel() { EmailId = x.EmailId }).ToList(),
                   EmployeeId = x.EmployeeId,
                   DateofJoining = x.DateofJoining,
                   Id = x.Id

               }).ToListAsync();
            //List<Employee> employees = FindAll().ToList();
            //List<Mobile> mobiles= RepositoryContext.Mobiles.ToList();
            //List<Email> emails = RepositoryContext.Emails.ToList();
            //employees.Join(emails, x => x.Id, y => y.Employee_UserId, (x, y) => new EmployeeViewModel
            //{
            //    DateOfBirth = x.DateOfBirth,
            //    Emails = y
            //});

            //employees.

            return employeeViewModels;

        }

        public async Task<Employee> GetEmployeeById(int employeeId)
        {
            return await FindByCondition(x => x.Id == employeeId)
                .Include(x=>x.Addresses)
                    .ThenInclude(x=>x.AddressType)

                .Include(x=>x.Emails)
                .Include(x=>x.Mobiles)
                .FirstOrDefaultAsync();
        }

        public async Task<ReturnResult> UpdateEmployee(AddEmployeeViewModel employeeView)
        {
            //var employeeExists= await GetEmployeeById(employee.Id);
            //if (employeeExists == null) 
            //{
            //    return new ReturnResult
            //    {
            //        Error = "Employee does not exist",
            //        Succeeded = false
            //    };
            //}
         var employeeExists=  FindByCondition(x => x.Id == employeeView.Id)
                .Include(x => x.Mobiles)
                .Include(x => x.Emails)
                .Include(x => x.Addresses)
                
                .FirstOrDefault();

         //Employee employee = _mapper.Map<Employee>(employeeView);
            _mapper.Map(employeeView, employeeExists);
            

            IdentityResult result= await _userManager.UpdateAsync(employeeExists);

            if (result.Succeeded) 
            {
                var SelectedRoles = employeeView.Roles.Where(x => x.IsSelected).Select(x => x.RoleName).ToArray();

                var roles = await _userManager.GetRolesAsync(employeeExists);
                result = await _userManager.RemoveFromRolesAsync(employeeExists, roles);
                result = await _userManager.AddToRolesAsync (employeeExists, SelectedRoles);
            }

            //Update(employeeExists);

            
            //await RepositoryContext.SaveChangesAsync();

            return new ReturnResult
            {
                Succeeded = true,
                Error = ""
            };
        }

        public async Task<AddEmployeeViewModel> GetEmployeeForEdit(int id) 
        {

            //AddEmployeeViewModel addEmployeeView= FindByCondition(x => x.Id == id)
            //    .Include(x=>x.UserRoles)
            //    .Include(x=>x.Addresses)
            //    .Include(x=>x.Mobiles)
            //    .Include(x=>x.Emails)
            //    .Select(x => new AddEmployeeViewModel()
            //    {
            //        Id = x.Id,
            //        Addresses = x.Addresses.Select(x => new AddAddressViewModel
            //        {
            //            AddressTypeId = x.AddressTypeId,
            //            CityMasterId = x.CityMasterId,
            //            CountryMasterId = x.CountryMasterId,
            //            StateMasterId = x.StateMasterId,
            //            LandMark = x.LandMark
            //        }).ToList(),
            //        DateOfBirth = x.DateOfBirth,
            //        DateofJoining = x.DateofJoining,
            //        DepartmentId = x.DepartmentId,
            //        Designation = x.Designation,
            //        Emails = x.Emails.Select(x => new EmailViewModel { EmailId = x.EmailId }).ToList(),
            //        Mobiles = x.Mobiles.Select(x => new MobileViewModel { MobileNumber = x.MobileNumber }).ToList(),
            //        GenderId = x.GenderId,
            //        FirstName = x.FirstName,
            //        LastName = x.LastName,
            //        MaritialStatusId = x.MaritialStatusId,
            //        NationalityId = x.NationalityId,
            //        PassportNumber = x.PassportNumber,
            //        EmployeeId = x.EmployeeId,
            //        FatherName = x.FatherName,
            //        Roles = x.UserRoles.Select(x => new RoleViewModel
            //        {
            //            IsSelected = true,
            //            RoleId = x.RoleId,
            //            RoleName = x.Role.Name
            //        }).ToList()
            //    }).FirstOrDefault();



            //AddEmployeeViewModel addEmployeeView = FindByCondition(x => x.Id == id).Select(x => new AddEmployeeViewModel()
            //{
            //    Id = x.Id,

            //    DateOfBirth = x.DateOfBirth,
            //    DateofJoining = x.DateofJoining,
            //    DepartmentId = x.DepartmentId,
            //    Designation = x.Designation,
            //    GenderId = x.GenderId,
            //    FirstName = x.FirstName,
            //    LastName = x.LastName,
            //    MaritialStatusId = x.MaritialStatusId,
            //    NationalityId = x.NationalityId,
            //    PassportNumber = x.PassportNumber,
            //    EmployeeId = x.EmployeeId,
            //    FatherName = x.FatherName,

            //}).FirstOrDefault();

            Employee employee = await _userManager.FindByIdAsync(id.ToString());

            AddEmployeeViewModel addEmployeeView = _mapper.Map<AddEmployeeViewModel>(employee);

            addEmployeeView.Roles = UserSelectedRoles(employee);
            addEmployeeView.Addresses = await GetAddressesAsync(id);
            addEmployeeView.Mobiles = await GetMobilesAsync(id);
            addEmployeeView.Emails = await GetEmailsAsync(id);
            addEmployeeView.Countries=await _dropdownsRepository.Countries();
            addEmployeeView.MaritialStatuses = await _dropdownsRepository.MaritialStatuses();
            addEmployeeView.Genders = await _dropdownsRepository.Genders();
            addEmployeeView.Deapartments = await _dropdownsRepository.Departments();
            addEmployeeView.Nationalities = await _dropdownsRepository.Nationalities();

            return addEmployeeView;

        }
        public List<RoleViewModel> UserSelectedRoles(Employee user)
        {
            var roles = _userManager.GetRolesAsync(user).Result;
            List<ApplicationRole> allRoles = RepositoryContext.Roles.ToList();
            List<RoleViewModel> roleView = allRoles.Select(x => new RoleViewModel()
            {
                RoleId = x.Id,
                RoleName = x.Name,
                IsSelected = roles.Any(y => y.ToUpper() == x.NormalizedName)
            }).ToList();

            return roleView;
        }

        public async Task<IList<string>> GetRolesById(int userId) 
        {
            Employee employee = await _userManager.FindByIdAsync(userId.ToString());
           return await  _userManager.GetRolesAsync(employee);
        }
        public async Task<IList<string>> GetUserRoles(Employee employee)
        {
            return await _userManager.GetRolesAsync(employee);
        }



        public async Task<List<MobileViewModel>> GetMobilesAsync(int userId) 
        {
            return await RepositoryContext.Mobiles.Where(x => x.Employee_UserId == userId).Select(x=>new MobileViewModel() {MobileNumber=x.MobileNumber }).ToListAsync();
        }

        public async Task<List<EmailViewModel>> GetEmailsAsync(int userId) 
        {
            return await RepositoryContext.Emails.Where(x => x.Employee_UserId == userId).Select(x=>new EmailViewModel{}) .ToListAsync();
        }
        public async Task<List<AddAddressViewModel>> GetAddressesAsync(int userId)
        {
            return await RepositoryContext.Addresses
                .Where(x => x.Employee_UserId == userId)
                .Select(x=>new AddAddressViewModel 
                            {
                                   AddressTypeId=x.AddressTypeId,
                                   LandMark=x.LandMark,
                                   CityMasterId=x.CityMasterId,
                                   CountryMasterId=x.CountryMasterId,
                                   StateMasterId=x.StateMasterId
                            }).ToListAsync();
        }



        //public Address AddEmployeeAddress(Address address) 
        //{

        //}

        public async Task<EmployeeViewModel> GetEmployeeDetails(int id) 
        {
            EmployeeViewModel employeeView = await FindByCondition(x => x.Id == id).Select(x => new EmployeeViewModel()
            {
                Id = x.Id,
                Addresses = x.Addresses.Select(x => new AddressViewModel
                {
                    AddressType = x.AddressType.Name,
                    CityName = x.City.City,
                    CountryName = x.Country.Country,
                    StateName = x.State.State,
                    LandMark = x.LandMark
                }).ToList(),
                DateOfBirth = x.DateOfBirth,
                DateofJoining = x.DateofJoining,
                Department = x.Department.Name,
                Designation = x.Designation,
                Emails = x.Emails.Select(x => new EmailViewModel { EmailId = x.EmailId }).ToList(),
                Mobiles = x.Mobiles.Select(x => new MobileViewModel { MobileNumber = x.MobileNumber }).ToList(),
                Gender = x.Gender.Name,
                FirstName = x.FirstName,
                LastName = x.LastName,
                MaritialStatus = x.MaritialStatus.Name,
                Nationality = x.Nationality.Nationality,
                PassportNumber = x.PassportNumber,
                EmployeeId = x.EmployeeId,
                FatherName = x.FatherName

            }).FirstOrDefaultAsync();

            return employeeView;
        }
    }
}
