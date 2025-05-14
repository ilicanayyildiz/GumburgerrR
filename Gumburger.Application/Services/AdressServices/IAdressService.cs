using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gumburger.Application.Models.DTOs;

namespace Gumburger.Application.Services.AdressServices
{
    public interface IAdressService
    {
        Task<List<AddressDTO>> GetAll();

        Task Create(AddressDTO model);

        Task Update(AddressDTO model);

        Task Delete(Guid id);

        Task<AddressDTO> GetById(Guid id);
    }
}
