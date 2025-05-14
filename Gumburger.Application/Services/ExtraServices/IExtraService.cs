using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gumburger.Application.Models.DTOs;

namespace Gumburger.Application.Services.ExtraServices
{
    public interface IExtraService
    {
        Task<List<ExtraDTO>> GetAll();

        Task Create(ExtraDTO model);

        Task Update(ExtraDTO model);

        Task Delete(Guid id);
    }
}
