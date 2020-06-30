using Contracts;
using Entities;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly RepositoryContext _repositoryContext;
       
        private readonly IHttpContextAccessor _httpContextAccessor;
        List<MenuViewModel> menuItemsg = new List<MenuViewModel>();
        public MenuRepository(RepositoryContext repositoryContext,IHttpContextAccessor httpContextAccessor)
        {
            this._repositoryContext = repositoryContext;
          
            this._httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<MenuViewModel>> GetTopNavMenuItems(int roleId)
        {
            //IQueryable<Menu> menus= FindAll();
            //var y=    menus.Select(x => new {menu=x.Name,MenuItems=x.MenuItems,Role=x.RoleMenus.Select(x=>x.Role.Name) }).ToList();

            //List<MenuViewModel> menuItems = await _repositoryContext.MenuItems
            //                                     .Where(x => x.RoleId == roleId && x.MenuType==2)
            //                                     .Select(x => new MenuViewModel
            //                                     {
            //                                         Id = x.Id,
            //                                         Name = x.Name,
            //                                         ParentId = x.ParentId,
            //                                         URL = x.URL
            //                                     }).ToListAsync();

            //return menuItems;
            return null;
        }

        public UserMenuViewModel GetMenu(string roleName)
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

            //_repositoryContext.MenuItems.Where(x => x.Role.Name == roleName);

            return new UserMenuViewModel();
            //return userMenuView;
        }

        public async Task<List<MenuViewModel>> GetTopNavMenuItems()
         {

            var role = _httpContextAccessor.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;


            if (role == null)
            {
                return new List<MenuViewModel>();
            }
            List<MenuViewModel> menuItems = await _repositoryContext.RoleMenus
                                                              .Include(x => x.Role)
                                                              .Include(x => x.MenuItem)
                                                              .Where(x => x.Role.NormalizedName == role.ToUpper())
                                                              .OrderBy(x=>x.MenuItem.Order)
                                                              .Select(x => new MenuViewModel
                                                              {
                                                                  Name = x.MenuItem.Name,
                                                                  ParentId = x.MenuItem.ParentId,
                                                                  URL = x.MenuItem.URL,
                                                                  Id=x.MenuItem.Id
                                                                  
                                                              }).ToListAsync();


            List<MenuViewModel> HierarchicalMenuItems = getMenuItems(menuItems);


            return HierarchicalMenuItems;
            return null;
        }

        public List<MenuViewModel> getMenuItems(List<MenuViewModel> menus) 
        {
          
            var groups = menus.GroupBy(i => i.ParentId);

            var grp = groups.ToList();

            var dict1= groups.ToDictionary(x => x.Key.Value, y => y.ToList());

            var roots = groups.FirstOrDefault(g =>
            {
                return g.Key == -1;
            }).ToList();

            if (roots.Count > 0)
            {
                var dict = groups.Where(g => g.Key.HasValue).ToDictionary(g => g.Key.Value, g => g.ToList());
                for (int i = 0; i < roots.Count; i++)
                    AddChildren(roots[i], dict);
            }

            return roots;


            

        }
        private static void AddChildren(MenuViewModel node, IDictionary<int, List<MenuViewModel>> source)
        {
            if (source.ContainsKey(node.Id))
            {
                node.Children = source[node.Id];
                for (int i = 0; i < node.Children.Count; i++)
                    AddChildren(node.Children[i], source);
            }
            else
            {
                node.Children = new List<MenuViewModel>();
            }
        }
        public MenuViewModel GetMenu()
        {
            
            throw new NotImplementedException();
        }
    }
}
