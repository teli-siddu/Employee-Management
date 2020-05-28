using AutoMapper;
using Contracts;
using Entities;
using Entities.Helper;
using Entities.Models;
using Entities.ViewModels.Leave;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class LeaveRepository :RepositoryBase<Leave>, ILeaveRepository
    {
        private readonly IMapper _mapper;

        public LeaveRepository(RepositoryContext repositoryContext,IMapper mapper):base(repositoryContext)
        {
            this._mapper = mapper;
        }

        public async Task<ReturnResult> AddLeave(AddLeaveViewModel addLeaveView)
        {
          
                Leave leave = _mapper.Map<Leave>(addLeaveView);
                
                Create(leave);
                
                await SaveChangesAsync();
                
                return new ReturnResult
                {
                    Succeeded = true,
                    Error = ""
                };
            
          
           
        }


       

        public async Task<ReturnResult> DeleteLeave(int id)
        {

            try
            {
                Leave leave = await GetLeaveById(id);

                if (leave == null)
                {
                    return new ReturnResult
                    {
                        Error = "Leave doesnot exists with this id",
                        Succeeded = false
                    };
                }

                

                Delete(leave);

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
                    Succeeded = false,
                    Error = x.Message

                };
            }
        }

        public async Task<List<LeaveViewModel>> GetAllLeaves()
        {
           List<LeaveViewModel> leaves=  await FindAll()
                                                 .Include(x=>x.LeaveType)
                                                 .Include(x=>x.Employee)
                                                 .Select(x => new LeaveViewModel
                                                 {
                                                     Description = x.Description,
                                                     LeaveFrom = x.LeaveFrom,
                                                     LeaveTo = x.LeaveTo,
                                                     LeaveType = x.LeaveType.Name

                                                 }).ToListAsync();

            return leaves;
        }


        public async Task<ReturnResult> UpdateLeave(AddLeaveViewModel addLeaveView)
        {
            try
            {
                Leave leave = await GetLeaveById(addLeaveView.Id);

                if (leave == null)
                {
                    return new ReturnResult
                    {
                        Error = "Leave doesnot exists with this id",
                        Succeeded = false
                    };
                }

                _mapper.Map(addLeaveView, leave);

                Update(leave);

                await SaveChangesAsync();
                return new ReturnResult
                {
                    Error = "",
                    Succeeded = true
                };
            }
            catch(Exception x) 
            {
                return new ReturnResult
                {
                    Succeeded = false,
                    Error = x.Message

                };
            }
            
            
        }

        public async Task<Leave> GetLeaveById(int id) 
        {
            return await FindByCondition(x => x.Id == id)
                        .FirstOrDefaultAsync();
        }

        public async Task<ReturnResult> UpdateLeaveStatus(int id,int leaveStatusId)
        {
            try
            {
                Leave leave = await GetLeaveById(id);

                leave.LeaveStatus.Id = leaveStatusId;
                if (leave == null)
                {
                    return new ReturnResult
                    {
                        Error = "Leave doesnot exists with this id",
                        Succeeded = false
                    };
                }

                Update(leave);

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
                    Succeeded = false,
                    Error = x.Message

                };
            }


        }

    }
}
