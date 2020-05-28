using Contracts;
using Entities;
using Entities.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DropdownsRepository : IDropdownsRepository
    {
        private readonly RepositoryContext _repositoryContext;

        public DropdownsRepository(RepositoryContext repositoryContext)
        {
            this._repositoryContext = repositoryContext;
        }

        public async Task<List<KeyValue<int,string>>> Cities(int StateId)
        {
            return await _repositoryContext.CityMaster.AsNoTracking().Where(x=>x.StateMasterId==StateId).Select(x => new KeyValue<int,string>(x.Id, x.City)).ToListAsync();
        }

        public async Task<List<KeyValue<int,string>>> Countries()
        {
            return await _repositoryContext.CountryMaster.AsNoTracking().Select(x => new KeyValue<int,string>(x.Id, x.Country)).ToListAsync();
        }

        public async Task<List<KeyValue<int,string>>> Departments()
        {
            return await _repositoryContext.Departments.AsNoTracking().Select(x => new KeyValue<int,string>(x.Id, x.Name)).ToListAsync();
        }

        public async Task<List<KeyValue<int,string>>> Genders()
        {
            return await _repositoryContext.Genders.AsNoTracking().Select(x => new KeyValue<int,string>(x.Id, x.Name)).ToListAsync();
        }

        public async Task<List<KeyValue<int,string>>> MaritialStatuses()
        {
            //var x = new KeyValue<int,string>();
            //x.
            return await _repositoryContext.MaritialStatuses.AsNoTracking().Select(x => new KeyValue<int,string>(x.Id, x.Name)).ToListAsync();
        }

        public async Task<List<KeyValue<int,string>>> States(int countryId)
        {
            return await _repositoryContext.StateMaster.AsNoTracking().Where(x=>x.CountryMasterId==countryId).Select(x => new KeyValue<int,string>(x.Id, x.State)).ToListAsync();
        }

        public async Task<List<KeyValue<int,string>>> Nationalities()
        {
            return await _repositoryContext.NationalityMaster.AsNoTracking().Select(x => new KeyValue<int,string>(x.Id, x.Nationality)).ToListAsync();
        }

        public async Task<List<KeyValue<int,string>>> LeaveTypes()
        {
            return await _repositoryContext.LeaveTypes.Select(x => new KeyValue<int, string>()
            {
                Key = x.Id,
                Value = x.Name
            }).ToListAsync();
        }



    }
}
