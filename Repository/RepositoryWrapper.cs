using Contracts;
using Entities;
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
        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
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


        public IAccountRepository Account => _owner == null ? new AccountRepository(_repositoryContext) : _account;

        public void Save()
        {
            _repositoryContext.SaveChanges();
        }
    }
}
