using Contracts;
using Entities;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class MenuRepository :IMenuRepository
    {
        private readonly RepositoryContext _repositoryContext;

        public MenuRepository(RepositoryContext repositoryContext)
        {
            this._repositoryContext = repositoryContext;
        }
        public  MenuViewModel GetMenu()
        {
           //IQueryable<Menu> menus= FindAll();
           //var y=    menus.Select(x => new {menu=x.Name,MenuItems=x.MenuItems,Role=x.RoleMenus.Select(x=>x.Role.Name) }).ToList();
            return new MenuViewModel();
        }

        public UserMenuViewModel GetMenu(string roleId)
        {

            //IQueryable<Menu> menus = FindAll();




            //var y = menus.Select(x => new
            //{
            //    menu = x.Name,
            //    MenuItems = x.MenuItems,
            //    Roles = x.RoleMenus.Select(x => new
            //    {
            //        Role = x.Role.Name,
            //        RoleId = x.RoleId
            //    }).FirstOrDefault()
            //}).Where(x => x.Roles.RoleId == Id).ToList();



            // var menuView = menus.Select(x => new 
            // {
            //     Name = x.Name,
            //     MenuItems = x.MenuItems.ToList(),
            //     RoleName = x.RoleMenus.Select(x => x.Role.Name).FirstOrDefault()
            //}).Where(x => x.RoleName.ToLower()== roleName.ToLower())
            //.Select(x=>new MenuViewModel() 
            //{
            //    MenuItems=x.MenuItems,
            //    Name=x.Name
            //})
            //.ToList();
            // UserMenuViewModel userMenuView = new UserMenuViewModel()
            // {
            //     RoleName = roleName,
            //     menuViews = menuView
            // };

             _repositoryContext.MenuItems.Where(x => x.RoleId == roleId);

            return new UserMenuViewModel();
            //return userMenuView;
        }
    }
}
