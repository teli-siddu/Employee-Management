using Contracts;
using Entities;
using Entities.HelperModels;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repositoryContext;
        private IOwnerRepository _owner;
        private IAccountRepository _account;
        private IUserRepository _user;
        private IOptions<AppSettings> _appSettings;


        private UserManager<ApplicationUser> _userManager;
        public RepositoryWrapper(RepositoryContext repositoryContext,IOptions<AppSettings> appSettings,UserManager<ApplicationUser> userManager)
        {
            _repositoryContext = repositoryContext;
            _appSettings = appSettings;
            _userManager = userManager;


        }


        
        public IOwnerRepository Owner
        {
            get
            {
                if (_owner is null)
                {
                    return new OwnerRepository(_repositoryContext);
                }
                return _owner;
            }
        }


        //public IAccountRepository Account => _account == null ? new AccountRepository(_repositoryContext) : _account;

      

        //public IUserRepository User => _user == null ? new UserRepository(_repositoryContext,_appSettings, _userManager) : _user;

        public AppSettings AppSettings => throw new NotImplementedException();

        public IUserRepository User => throw new NotImplementedException();

        public IAccountRepository Account => throw new NotImplementedException();




        //public IUserRepository User => _user == null ? new UserRepository(_repositoryContext) : _user;

        public void Save()
        {
            _repositoryContext.SaveChanges();
        }
    }
}
