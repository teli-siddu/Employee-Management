using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Helper;
using Entities.ViewModels.Leave;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeavesController : ControllerBase
    {
        private readonly ILeaveRepository _leaveRepository;

        public LeavesController(ILeaveRepository leaveRepository)
        {
            this._leaveRepository = leaveRepository;
        }

        [HttpGet("Leaves")]
        public async Task<IActionResult> Leaves() 
        {
            try
            {
                List<LeaveViewModel> leaves = await _leaveRepository.GetAllLeaves();

                return Ok(leaves);
            }
            catch (Exception x)
            {
                ReturnResult returnResult = new ReturnResult
                {
                    Error = x.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, returnResult);
            }
           
        }


        [HttpPost("AddLeave")]
        public async Task<IActionResult> AddLeave(AddLeaveViewModel addLeaveView) 
        {
            try
            {
                ReturnResult result = await _leaveRepository.AddLeave(addLeaveView);
                return Ok(result);
            }
            catch (Exception x) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
  
        }

      
        [HttpPut("UpdateLeave")]
        public async Task<IActionResult> UpdateLeave(AddLeaveViewModel addLeaveView)
        {
            try
            {
                ReturnResult result = await _leaveRepository.UpdateLeave(addLeaveView);
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [HttpDelete("DeleteLeave/{id}")]
        public async Task<IActionResult> DeleteLeave(int id)
        {
            try
            {
                ReturnResult result = await _leaveRepository.DeleteLeave(id);

                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }



    }
}