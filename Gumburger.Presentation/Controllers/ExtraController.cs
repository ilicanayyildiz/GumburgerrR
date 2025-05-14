using Gumburger.Application.Models.VMs;
using Gumburger.Application.Services.Abstract;
using Gumburger.Domain.Entities.Concrete;
using Gumburger.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gumburger.Presentation.Controllers
{
    [AllowAnonymous]
    public class ExtraController : Controller
    {
        private static IBaseService<Extra> _extraService;

        public ExtraController(IBaseService<Extra> extraService)
        {
            _extraService = extraService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _extraService.GetFilteredList(
                    select: x => new ExtraVM()
                    {
                        Id = x.Id,
                        ExtraName = x.ExtraName,
                        ExtraPrice = x.ExtraPrice,
                        ExtraImageUrl = x.ExtraImageUrl
                    },
                    where: x => x.Status == Status.Active,
                    orderBy: x => x.OrderBy(x => x.ExtraName))
            );
        }
    }
}
