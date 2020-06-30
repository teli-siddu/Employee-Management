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
        Task<List<MenuViewModel>> GetTopNavMenuItems(int roleId);
        Task<List<MenuViewModel>> GetTopNavMenuItems();

    }
}
