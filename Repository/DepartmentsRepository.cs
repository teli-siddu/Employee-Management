using Contracts;
using Entities;
using Entities.Helper;
using Entities.Models;
using Entities.ViewModels;
using Entities.ViewModels.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DepartmentsRepository : RepositoryBase<Department>, IDepartmentsRepository
    {


        public DepartmentsRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public async Task<ReturnResult> AddDepartment(Department department)
        {
            ReturnResult result;

            var ExistDept = FindByCondition(x => x.Name == department.Name).FirstOrDefault();
            if (ExistDept != null)
            {
                result = new ReturnResult()
                {
                    Succeeded = false,
                    Error = "Department Already Exist"
                };
            }
            Create(department);
            int retVal = await RepositoryContext.SaveChangesAsync();

            result = new ReturnResult()
            {
                Succeeded = true,
                Error = ""
            };



            return result;


        }

        public async Task<ReturnResult> DeleteDepartmentById(int departmentId)
        {
            ReturnResult result;

            var ExistDept = FindByCondition(x => x.Id == departmentId).FirstOrDefault();
            if (ExistDept == null)
            {
                return new ReturnResult()
                {
                    Succeeded = false,
                    Error = "Department Not Exist"
                };
            }
            

            Delete(ExistDept);

            int retVal = await RepositoryContext.SaveChangesAsync();

            result = new ReturnResult()
            {
                Succeeded = true,
                Error = ""
            };



            return result;


        }
        public async Task<ReturnResult> DeleteDepartmentByName(string DepartmentName)
        {
            ReturnResult result;

            var ExistDept = FindByCondition(x => x.Name == DepartmentName).FirstOrDefault();
            if (ExistDept != null)
            {
                result = new ReturnResult()
                {
                    Succeeded = false,
                    Error = "Department Not Exist"
                };
            }

            Delete(ExistDept);

            int retVal = await RepositoryContext.SaveChangesAsync();

            result = new ReturnResult()
            {
                Succeeded = false,
                Error = ""
            };


            return result;
        }

        public async Task<ReturnResult> UpdateDepartment(Department department, int departmentId)
        {
            ReturnResult result;

            var ExistDept = FindByCondition(x => x.Id == departmentId).FirstOrDefault();
            if (ExistDept != null)
            {
                result = new ReturnResult()
                {
                    Succeeded = false,
                    Error = "Department Not Exist"
                };
            }

            Update(ExistDept);

            int retVal = await RepositoryContext.SaveChangesAsync();

            result = new ReturnResult()
            {
                Succeeded = false,
                Error = ""
            };


            return result;
        }

        public async Task<ReturnResult> UpdateDepartment(Department department)
        {
            ReturnResult result;
            //try
            //{
            var ExistDept = FindByCondition(x => x.Id == department.Id).AsNoTracking().FirstOrDefault();
            if (ExistDept == null)
            {
                result = new ReturnResult()
                {
                    Succeeded = false,
                    Error = "Department Not Exist"
                };
            }

            Update(department);

            int retVal = await RepositoryContext.SaveChangesAsync();

            result = new ReturnResult()
            {
                Succeeded = true,
                Error = ""
            };
            //}
            //catch (Exception x)
            //{
            //    result = new ReturnResult()
            //    {
            //        Succeeded = false,
            //        Error = x.Message
            //    };
            //}

            return result;
        }

        public async Task<List<KeyValue<int,string>>> GetDepartments()
        {
       
           var departments=  await FindAll().Select(x => new KeyValue<int, string>(x.Id, x.Name)).ToListAsync();
            return departments;
        }

        public async Task<List<DepartmentViewModel>> AllDepartments() 
        {
            List<DepartmentViewModel> departments = await FindAll().Select(x => new DepartmentViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return departments;

        }
        public async Task<DepartmentViewModel> GetDepartmentById(int Id)
        {
            DepartmentViewModel department= await FindByCondition(x => x.Id == Id).Select(x=>new DepartmentViewModel() 
            {
                Id=x.Id,
                Name=x.Name
            }).FirstOrDefaultAsync();
            return department;
        }


    }
}
