using Gumburger.Application.Models.VMs;
using Gumburger.Application.Services.Abstract;
using Gumburger.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gumburger.Presentation.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private static IBaseService<Menu> _menuService;
        private static IBaseService<Extra> _extraService;
        private readonly IBaseService<Address> _addressService;
        private readonly IBaseService<Order> _orderService;
        private readonly IBaseService<OrdersMenus> _ordersMenusService;
        private readonly IBaseService<OrdersExtras> _ordersExtrasService;


        public CartController(IBaseService<Menu> menuService, IBaseService<Extra> extraService, IBaseService<Address> addressService, IBaseService<Order> orderService, IBaseService<OrdersMenus> ordersMenusService, IBaseService<OrdersExtras> ordersExtrasService)
        {
            _menuService = menuService;
            _extraService = extraService;
            _addressService = addressService;
            _orderService = orderService;
            _ordersMenusService = ordersMenusService;
            _ordersExtrasService = ordersExtrasService;
        }

        public async Task<IActionResult> Index()
        {
            List<CartItemVM> cart = HttpContext.Session.Get<List<CartItemVM>>("cart");
            if (cart != null)
            {
                ViewBag.total = cart.Sum(s => s.Quantity * s.Price);
            }
            else
            {
                cart = new List<CartItemVM>();
                ViewBag.total = 0;
            }


            ViewBag.Addresses = await _addressService.GetDefaults(x => x.AppUserId == Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            return View(cart);
        }

        [HttpPost]
        public IActionResult Buy(Guid id)
        {
            var menuItem = _menuService.GetById(id);
            var cart = HttpContext.Session.Get<List<CartItemVM>>("cart");

            if (menuItem != null) //if it is a menu
            {
                cart = AddMenuToCart(id, menuItem, cart);
            }
            else //if it is a extra sauce
            {
                cart = AddExtraToCart(id, cart);
            }

            HttpContext.Session.Set<List<CartItemVM>>("cart", cart);
            return RedirectToAction("Index");
        }

        private static List<CartItemVM> AddExtraToCart(Guid id, List<CartItemVM>? cart)
        {
            var extraItem = _extraService.GetById(id);
            if (cart == null) //no item in the cart
            {
                cart = new List<CartItemVM>();
                cart.Add(new CartItemVM()
                {
                    Id = id,
                    ProductName = extraItem.ExtraName,
                    Price = extraItem.ExtraPrice,
                    ProductImage = extraItem.ExtraImageUrl,
                    Quantity = 1
                });
            }
            else
            {
                int index = cart.FindIndex(w => w.Id == id); //if there exists, in which index we have the same product
                                                             //​
                if (index != -1) //if item already in the cart
                {
                    cart[index].Quantity++; //increment by 1
                }
                else
                {
                    cart.Add(new CartItemVM()
                    {
                        Id = id,
                        ProductName = extraItem.ExtraName,
                        Price = extraItem.ExtraPrice,
                        ProductImage = extraItem.ExtraImageUrl,
                        Quantity = 1
                    });
                }
            }

            return cart;
        }

        private static List<CartItemVM> AddMenuToCart(Guid id, Menu? menuItem, List<CartItemVM>? cart)
        {
            if (cart == null) //no item in the cart
            {
                cart = new List<CartItemVM>();
                cart.Add(new CartItemVM()
                {
                    Id = id,
                    ProductName = $"{menuItem.MenuName} {menuItem.MenuSize}",
                    Price = menuItem.MenuPrice,
                    ProductImage = menuItem.MenuImagePath,
                    Quantity = 1
                });
            }
            else
            {
                int index = cart.FindIndex(w => w.Id == id); //if there exists, in which index we have the same product
                                                             //​
                if (index != -1) //if item already in the cart
                {
                    cart[index].Quantity++; //increment by 1
                }
                else
                {
                    cart.Add(new CartItemVM()
                    {
                        Id = id,
                        ProductName = $"{menuItem.MenuName} {menuItem.MenuSize}",
                        Price = menuItem.MenuPrice,
                        ProductImage = menuItem.MenuImagePath,
                        Quantity = 1
                    });
                }
            }

            return cart;
        }

        [HttpPost]
        public IActionResult Add(Guid id)
        {
            //var product = _menuService.GetById(id);
            var cart = HttpContext.Session.Get<List<CartItemVM>>("cart");

            int index = cart.FindIndex(w => w.Id == id);
            cart[index].Quantity++; //increment by 1

            HttpContext.Session.Set<List<CartItemVM>>("cart", cart);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Subtract(Guid id)
        {
            //var product = _menuService.GetById(id);
            var cart = HttpContext.Session.Get<List<CartItemVM>>("cart");

            int index = cart.FindIndex(w => w.Id == id);

            if (cart[index].Quantity == 1) //last item of a product
            {
                cart.RemoveAt(index); //remove it
            }
            else
            {
                cart[index].Quantity--; //reduce by 1
            }

            HttpContext.Session.Set<List<CartItemVM>>("cart", cart);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Remove(Guid id)
        {
            //var product = _menuService.GetById(id);
            var cart = HttpContext.Session.Get<List<CartItemVM>>("cart");

            int index = cart.FindIndex(w => w.Id == id);
            cart.RemoveAt(index);

            HttpContext.Session.Set<List<CartItemVM>>("cart", cart);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Order(string selectedAddressId)
        {
            var cart = HttpContext.Session.Get<List<CartItemVM>>("cart");

            Guid addressId = Guid.Parse(selectedAddressId);

            Order newOrder = new Order()
            {
                AppUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                ShippedAddress = _addressService.GetById(addressId).FullAddress,
            };
            await _orderService.Insert(newOrder);


            foreach (var item in cart)
            {
                Menu menuItem = await _menuService.GetDefault(x => x.Id == item.Id);
                if (menuItem != null) //eğer menu ise
                {
                    OrdersMenus ordersMenus = new OrdersMenus()
                    {
                        OrderId = newOrder.Id,
                        MenuId = item.Id,
                        MenuPrice = item.Price,
                        MenuQuantity = (short)item.Quantity
                    };
                    await _ordersMenusService.Insert(ordersMenus);

                }
                else //eğer extra ise  
                {
                    OrdersExtras ordersExtras = new OrdersExtras()
                    {
                        OrderId = newOrder.Id,
                        ExtraId = item.Id,
                        ExtraPrice = item.Price,
                        ExtraQuantity = (short)item.Quantity
                    };
                    await _ordersExtrasService.Insert(ordersExtras);
                }
            }
            HttpContext.Session.Clear(); //Clear the Cart

            return RedirectToAction("Index", "Home"); //TODO: OrderPlace adında başka bir View oluşturulabilir.
        }
    }
}
