using Gumburger.Application.Models.DTOs;
using Gumburger.Application.Services.Abstract;
using Gumburger.Application.Services.AppUserServices;
using Gumburger.Application.Services.ExtraServices;
using Gumburger.Application.Services.MenuServices;
using Gumburger.Application.Services.OrderServices;
using Gumburger.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gumburger.Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly IExtraService _extraService;
        private readonly IOrderService _orderService;
        private readonly IBaseService<Menu> _baseMenuService;
        private readonly IBaseService<Extra> _baseExtraService;
        private readonly IBaseService<AppUser> _baseAppUserService;
        private readonly IAppUserService _appUserService;


        public AdminController(IMenuService menuService, IBaseService<AppUser> baseAppUserService, IBaseService<Menu> baseMenuService, IOrderService orderService, IAppUserService appUserService, IExtraService extraService, IBaseService<Extra> baseExtraService)
        {
            _baseMenuService = baseMenuService;
            _menuService = menuService;
            _baseAppUserService = baseAppUserService;
            _orderService = orderService;
            _appUserService = appUserService;
            _extraService = extraService;
            _baseExtraService = baseExtraService;
        }

        // MENU ACTIONS
        public IActionResult Index()
        {
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> Menus()
        {
            return View(await _menuService.GetAll());

        }

        //TODO Create Metotları Servisten Getirilecek
        public async Task<IActionResult> AddMenu()
        {
            return View(new MenuDTO());
        }


        [HttpPost]
        public async Task<IActionResult> AddMenu(MenuDTO menu)
        {
            if (!ModelState.IsValid)
            {
                return View(menu);
            }

            await _menuService.Create(menu);
            return RedirectToAction("Menus");
        }


        public IActionResult DeleteMenu(Guid id)
        {
            return View(_baseMenuService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMenu(MenuDTO menu)
        {
            await _baseMenuService.Delete(menu.Id);
            return RedirectToAction("Menus");
        }
        public IActionResult EditMenu(Guid id)
        {
            Menu menu = _baseMenuService.GetById(id);
            MenuDTO menuDTO = new MenuDTO();
            menuDTO.Id = menu.Id;
            menuDTO.MenuName = menu.MenuName;
            menuDTO.Status = menu.Status;
            menuDTO.MenuImagePath = menu.MenuImagePath;
            menuDTO.MenuSize = menu.MenuSize;
            menuDTO.MenuPrice = menu.MenuPrice;

            return View(menuDTO);
        }

        [HttpPost]
        public async Task<IActionResult> EditMenu(MenuDTO menu)
        {
            await _menuService.Update(menu);
            return RedirectToAction("Menus");
        }



        //EXTRAS
        public async Task<IActionResult> Extras()
        {
            return View(await _extraService.GetAll());

        }

        public async Task<IActionResult> AddExtra()
        {
            return View(new ExtraDTO());
        }


        [HttpPost]
        public async Task<IActionResult> AddExtra(ExtraDTO extra)
        {
            if (!ModelState.IsValid)
            {
                return View(extra);
            }

            await _extraService.Create(extra);
            return RedirectToAction("Extras");
        }


        public IActionResult DeleteExtra(Guid id)
        {
            return View(_baseExtraService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteExtra(ExtraDTO extra)
        {
            await _extraService.Delete(extra.Id);
            return RedirectToAction("Extras");
        }
        public IActionResult EditExtra(Guid id)
        {
            Extra extra = _baseExtraService.GetById(id);
            ExtraDTO extraDTO = new ExtraDTO();
            extraDTO.Id = extra.Id;
            extraDTO.ExtraName = extra.ExtraName;
            extraDTO.ExtraPrice = extra.ExtraPrice;
            extraDTO.ExtraImageUrl = extra.ExtraImageUrl;
            extraDTO.Status = extra.Status;

            return View(extraDTO);
        }

        [HttpPost]
        public async Task<IActionResult> EditExtra(ExtraDTO extra)
        {
            await _extraService.Update(extra);
            return RedirectToAction("Extras");
        }



        // CUSTOMER ACTIONS
        public async Task<IActionResult> Customers()
        {
            return View(await _appUserService.GetCustomers());
        }

        public async Task<IActionResult> CustomerOrders(Guid id)
        {
            return View(await _orderService.GetCustomerOrders(id));
        }

        public async Task<IActionResult> OrderDetails(Guid id)
        {
            return RedirectToAction("Customers");
        }


        [HttpGet]
        public async Task<IActionResult> AddCustomer()
        {

            return View(new CustomerDTO());
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(CustomerDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _appUserService.Create(model);
            return RedirectToAction("Customers");
        }



        public IActionResult DeleteCustomer(Guid id)
        {
            return View(_baseAppUserService.GetById(id));
        }


        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(AppUser appUser)
        {
            await _baseAppUserService.Delete(appUser.Id);
            return RedirectToAction("Customers");
        }


        


        public IActionResult EditCustomer(Guid id)
        {
            return View(_baseAppUserService.GetById(id));
        }


        [HttpPost]
        public async Task<IActionResult> EditCustomer(AppUser appUser)
        {
            await _baseAppUserService.Update(appUser);
            return RedirectToAction("Customers");
        }



        public async Task<IActionResult> Orders()
        {
            return View(await _orderService.GetAll());
        }




        public IActionResult UploadPicture()
        {
            return View();
        }

        public IActionResult DeletePicture()
        {
            return View();
        }
    }
}
