using Gumburger.Application.Models.VMs;
using Gumburger.Application.Services.Abstract;
using Gumburger.Domain.Entities.Concrete;
using Gumburger.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gumburger.Presentation.Controllers
{
    [AllowAnonymous]
    public class MenuController : Controller
    {
        private static IBaseService<Menu> _menuService;

        public MenuController(IBaseService<Menu> menuService)
        {
            _menuService = menuService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _menuService.GetFilteredList(
                     select: x => new MenuVM()
                     {
                         MenuId = x.Id,
                         MenuName = x.MenuName,
                         MenuPrice = x.MenuPrice,
                         MenuSize = x.MenuSize,
                         MenuImagePath = x.MenuImagePath
                     },
                     where: x => x.Status == Status.Active,
                     orderBy: x => x.OrderBy(x => x.MenuName).ThenBy(x => x.MenuSize)));
        }
    }
}
