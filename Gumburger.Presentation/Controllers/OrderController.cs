using System.Security.Claims;
using Gumburger.Application.Models.DTOs;
using Gumburger.Application.Services.Abstract;
using Gumburger.Application.Services.OrderServices;
using Gumburger.Domain.Entities.Concrete;
using Gumburger.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Gumburger.Presentation.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IBaseService<Order> _baseOrderService;

        public OrderController(IOrderService orderService, IBaseService<Order> baseOrderService)
        {
            _orderService = orderService;
            _baseOrderService = baseOrderService;
        }


        public async Task<IActionResult> Index()
        {
            var orders = await _baseOrderService.GetFilteredList(
                select: x => new OrderDTO()
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate,
                    Notes = x.Notes,
                    Quantity = x.OrderQuantity


                },
                where: x => x.AppUserId == Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) && x.Status == Status.Active,
                orderBy: x => x.OrderBy(x => x.OrderDate)
            );

            return View(orders);
        }
    }
}
