using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Helper;
using Entities.Models;
using Entities.ViewModels;
using Entities.ViewModels.Departments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentsRepository _departmentsRepository;

        public DepartmentsController(IDepartmentsRepository departmentsRepository)
        {
            this._departmentsRepository = departmentsRepository;
        }
        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDepartment(DepartmentViewModel departmentView)
        {
            try
            {
                Department department = new Department
                {
                    Name = departmentView.Name,

                };
                var result = await _departmentsRepository.AddDepartment(department);
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment(DepartmentViewModel departmentView)
        {
            try
            {
                Department department = new Department()
                {
                    Id = departmentView.Id,
                    Name = departmentView.Name
                };
                var result = await _departmentsRepository.UpdateDepartment(department);
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost("UpdateDepartment/{Id}")]
        public async Task<IActionResult> UpdateDepartment(int Id)
        {
            try
            {
                var result = await _departmentsRepository.GetDepartmentById(Id);
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("GetDepartmentById/{Id}")]
        public async Task<IActionResult> GetDepartmentById(int Id) 
        {
            try
            {
                var result = await _departmentsRepository.GetDepartmentById(Id);
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("DeleteDepartment/{Id}")]
        public async Task<IActionResult> DeleteDepartment(int Id)
        {
            try
            {
                var result = await _departmentsRepository.DeleteDepartmentById(Id);
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Departments")]
        public async Task<IActionResult> Departments()
        {
            try
            {
               List<DepartmentViewModel> departments = await _departmentsRepository.AllDepartments();
                return Ok(departments);
            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}