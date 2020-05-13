using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

            [HttpGet("GetMenu/{RoleName}")]
        public IActionResult GetMenu(string RoleName)
        {
            UserMenuViewModel userMenuView = _menuRepository.GetMenu(RoleName);
            return Ok(userMenuView);
         }
    }
}