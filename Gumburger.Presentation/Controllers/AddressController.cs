using Gumburger.Application.Models.DTOs;
using Gumburger.Application.Services.Abstract;
using Gumburger.Application.Services.AdressServices;
using Gumburger.Domain.Entities.Concrete;
using Gumburger.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gumburger.Presentation.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAdressService _addressService;
        private readonly IBaseService<Address> _baseAddressService;


        public AddressController(IAdressService addresService, IBaseService<Address> baseAddressService)
        {
            _addressService = addresService;
            _baseAddressService = baseAddressService;
        }
        public async Task<IActionResult> Index()
        {
            var address = await _baseAddressService.GetFilteredList(
                select: x => new AddressDTO()
                {
                    Id = x.Id,
                    FullAddress = x.FullAddress,
                },
                where: x => x.AppUserId == Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) && x.Status == Status.Active
                );
            return View(address);
        }



        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddressDTO model)
        {

            await _addressService.Create(model);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Edit(Guid id)
        {
            await _addressService.GetById(id);
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddressDTO model)
        {
            await _addressService.Update(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            await _addressService.GetById(id);
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> DeleteAddress(AddressDTO model)
        {
            await _addressService.Delete(model.Id);
            return RedirectToAction("Index");
        }
    }
}

