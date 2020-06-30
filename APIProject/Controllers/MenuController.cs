using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MenuController : ControllerBase
    {
        private readonly IMenuRepository _menuRepository;

        public MenuController(IMenuRepository menuRepository)
        {
            this._menuRepository = menuRepository;
        }

        [HttpGet("Index")]
        public IActionResult Index() 
        {
            var y= _menuRepository.GetMenu();
            return Ok();
        }

        //public Task<IActionResult> GetMenu()
        //{

        //}

            [HttpGet("TopNavMenuItems/{roleId}")]
        public async Task<IActionResult> GetTopNavMenuItems(int roleId)
        {
            List<MenuViewModel> menuItems = await _menuRepository.GetTopNavMenuItems(roleId);
            return Ok(menuItems);
         }

        [HttpGet("TopNavMenuItems")]
        public async Task<IActionResult> GetTopNavMenuItems()
        {
            List<MenuViewModel> menuItems = await _menuRepository.GetTopNavMenuItems();
            return Ok(menuItems);
        }
    }
}