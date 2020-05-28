using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Helper;
using Entities.Models;
using Entities.ViewModels;
using Entities.ViewModels.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepository employeeRepository,IMapper mapper)
        {
            this._employeeRepository = employeeRepository;
            this._mapper = mapper;
        }

        [HttpGet("Employees")]
        public async Task<IActionResult> Employees() 
        {
            try
            {

              List<EmployeeViewModel> employees=   await _employeeRepository.Employees();

                return Ok(employees);

            }
            catch (Exception x) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, x.Message);
            }
        }

        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee(AddEmployeeViewModel employeeView)
        {
            
            try
            {
            //    Employee employee = new Employee
            //    {
            //        FirstName=employeeView.FirstName,
            //        LastName=employeeView.LastName,
            //        DateOfBirth= employeeView.DateOfBirth,
            //        FatherName=employeeView.FatherName,
            //        GenderId=employeeView.GenderId,
            //        MaritialStatusId=employeeView.MaritialStatusId,
            //        DepartmentId=employeeView.DepartmentId,
            //        NationalityId=employeeView.NationalityId,
            //        PassportNumber=employeeView.PassportNumber,
            //        Mobiles=employeeView.Mobiles.Select(x=>new Mobile() 
            //        {
            //            MobileNumber=x.MobileNumber
            //        }).ToList(),
            //        Addresses=employeeView.Addresses.Select(x=>new Address 
            //        {
            //            CityMasterId=x.CityId,
            //            AddressTypeId=x.AddressTypeId,
            //            CountryMasterId=x.CountryId,
            //            StateMasterId=x.StateId,
            //            LandMark=x.LandMark
            //        }).ToList(),
            //        Emails=employeeView.Emails.Select(x=>new Email 
            //        {
            //            EmailId=x.EmailId
            //        }).ToList(),
            //        Designation=employeeView.Designation,
            //        EmployeeId=employeeView.EmployeeId,
            //        DateofJoining=employeeView.DateofJoining,
                    
            //    };

                 Employee employee = _mapper.Map<Employee>(employeeView);
                //Employee employee1 = new Employee()
                //{
                //    FirstName = "siddu",
                //    LastName = "T",
                //    DateOfBirth = new DateTime(1195, 06, 02),
                //    Mobiles = new List<Mobile>
                //    {
                //        new Mobile
                //        {
                //            MobileNumber="8497848656",

                //        }
                //    },
                //    Emails = new List<Email>
                //    {
                //        new Email
                //        {
                //            EmailId="aaa@gmail.com"
                //        }
                //    },
                //    Addresses = new List<Address>
                //    {
                //        new Address
                //        {

                //            LandMark="landmark",
                //            CityMasterId=102,
                //            CountryMasterId=1,
                //            StateMasterId=8,
                //            AddressTypeId=3
                //        }
                //    },



                //};



                ReturnResult result =await  _employeeRepository.AddEmployee(employeeView);

                return Ok(result);

            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, x.Message);
            }
        }


        [HttpGet("EditEmployee/{id}")]
        public async Task<IActionResult> EditEmployee(int id) 
        {
            try
            {
                AddEmployeeViewModel editEmployeeView = await _employeeRepository.GetEmployeeForEdit(id);
                return Ok(editEmployeeView);
            }
            catch(Exception x) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
         
        }
        [HttpPost("Updatemployee")]
        public async Task<IActionResult> Updatemployee(AddEmployeeViewModel employeeView)
        {
            try
            {
                Employee employee = new Employee
                {
                    Id=employeeView.Id,
                    FirstName = employeeView.FirstName,
                    LastName = employeeView.LastName,
                    DateOfBirth = employeeView.DateOfBirth,
                    FatherName = employeeView.FatherName,
                    GenderId = employeeView.GenderId,
                    MaritialStatusId = employeeView.MaritialStatusId,
                    DepartmentId = employeeView.DepartmentId,
                    NationalityId = employeeView.NationalityId,
                    PassportNumber = employeeView.PassportNumber,
                    Mobiles = employeeView.Mobiles.Select(x => new Mobile()
                    {
                        Employee_UserId = employeeView.Id,
                        MobileNumber = x.MobileNumber
                    }).ToList(),
                    Addresses = employeeView.Addresses.Select(x => new Address
                    {
                        CityMasterId = x.CityMasterId,
                        AddressTypeId = x.AddressTypeId,
                        CountryMasterId = x.CountryMasterId,
                        StateMasterId = x.StateMasterId,
                        LandMark = x.LandMark,
                        Employee_UserId=employeeView.Id
                    }).ToList(),
                    Emails = employeeView.Emails.Select(x => new Email
                    {
                        Employee_UserId = employeeView.Id,
                        EmailId = x.EmailId
                    }).ToList(),
                    Designation = employeeView.Designation,
                    EmployeeId = employeeView.EmployeeId,
                    DateofJoining = employeeView.DateofJoining,



                };


           


                ReturnResult result =  _employeeRepository.UpdateEmployee(employeeView).Result;

                return Ok(result);

            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, x.Message);
            }
        }

        [HttpGet("DeleteEmployee/{Id}")]
        public async Task<IActionResult> DeleteEmployee(int Id)
        {
            try
            {

                ReturnResult result = await _employeeRepository.DeleteEmployee(Id);

                return Ok(result);
                
            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, x.Message);
            }
        }


        [HttpGet("EmployeeDetails/{id}")]
        public async Task<IActionResult> EmployeeDetails(int Id) 
        {
            try
            {
                EmployeeViewModel employeeView = await _employeeRepository.GetEmployeeDetails(Id);
                return Ok(employeeView);
            }
            catch (Exception x) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }


    }
}