using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAuthorization
    {
         Task<AccessTokenViewModel> GetToken(string userName, string password);
       





    }
}
