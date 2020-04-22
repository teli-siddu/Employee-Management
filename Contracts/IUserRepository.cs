using Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string userName, string password);
        public IEnumerable<User> GetAll();

        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IdentityResult> Register(ApplicationUser user, string password);
    }
        
}
