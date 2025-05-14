using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gumburger.Application.Models.DTOs;

namespace Gumburger.Application.Services.MenuServices
{
    public interface IMenuService
    {
        Task Create(MenuDTO model);

        Task Delete(Guid id);

        Task Update(MenuDTO model);

        Task<List<MenuDTO>> GetAll();
    }
}
