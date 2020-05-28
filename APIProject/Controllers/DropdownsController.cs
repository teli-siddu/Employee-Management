using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DropdownsController : ControllerBase
    {
        private readonly IDropdownsRepository _dropdownsRepository;

        public DropdownsController(IDropdownsRepository dropdownsRepository)
        {
            this._dropdownsRepository = dropdownsRepository;
        }

        [HttpGet("Countries")]
        public async Task<IActionResult> Countries() 
        {
            try
            {
                return  Ok( await  _dropdownsRepository.Countries());

            }
            catch (Exception x) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("States/{countryId}")]
        public async Task<IActionResult> States( int countryId)
        {
            try
            {
                return Ok(await _dropdownsRepository.States(countryId));

            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Cities/{stateId}")]
        public async Task<IActionResult> Cities(int stateId)
        {
            try
            {
                return Ok(await _dropdownsRepository.Cities(stateId));

            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("Deapartments")]
        public async Task<IActionResult> Deapartments()
        {
            try
            {
                return Ok(await _dropdownsRepository.Departments());

            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("Genders")]
        public async Task<IActionResult> Genders()
        {
            try
            {
                return Ok(await _dropdownsRepository.Genders());

            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("MaritialStatuses")]
        public async Task<IActionResult> MaritialStatuses()
        {
            try
            {
                return Ok(await _dropdownsRepository.MaritialStatuses());

            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Nationalities")]
        public async Task<IActionResult> Nationalities()
        {
            try
            {
                return Ok(await _dropdownsRepository.Nationalities());

            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



        [HttpGet("GetAddEmployeeDropdowns")]
        public async Task<IActionResult> GetAddEmployeeDropdowns() 
        {
            try
            {
                var countries = await _dropdownsRepository.Countries();
                var departments = await _dropdownsRepository.Departments();
                var genders = await _dropdownsRepository.Genders();
                var maritialStatuses = await _dropdownsRepository.MaritialStatuses();
                var nationalities= await _dropdownsRepository.Nationalities();
                return Ok(new AddEmployeeDropdowns
                {
                    MaritialStatuses = maritialStatuses,
                    Genders = genders,
                    Departments = departments,
                    Countries = countries,
                    Nationalities=nationalities
                });
            }
            catch (Exception x) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }

        [HttpGet("LeaveTypes")]
        public async Task<IActionResult> LeaveTypes() 
        {
            try
            {
                List<KeyValue<int, string>> leaveTypes = await _dropdownsRepository.LeaveTypes();

                return Ok(leaveTypes);
            }
            catch (Exception x) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
        }


    }
}