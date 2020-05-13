using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IMenuRepository
    {
        MenuViewModel GetMenu();
        UserMenuViewModel GetMenu(string roleId);

    }
}
