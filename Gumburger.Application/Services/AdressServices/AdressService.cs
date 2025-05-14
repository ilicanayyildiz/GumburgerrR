using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Gumburger.Application.Models.DTOs;
using Gumburger.Domain.Repositories;
using Address = Gumburger.Domain.Entities.Concrete.Address;

namespace Gumburger.Application.Services.AdressServices
{
    public class AdressService : IAdressService
    {
        private readonly IBaseRepository<Address> _repository;

        public AdressService(IBaseRepository<Address> repository)
        {
            _repository = repository;
        }

        public Task Create(AddressDTO model)
        {
            Address address = new Address()
            {
                FullAddress = model.FullAddress
            };
            return _repository.Insert(address);
        }

        public async Task Delete(Guid id)
        {
            Address address = await _repository.GetDefault(x => x.Id == id);
            await _repository.Delete(address.Id);
        }

        public async Task<AddressDTO> GetById(Guid id)
        {
            Address address = await _repository.GetDefault(x => x.Id == id);
            AddressDTO addressDTO = new AddressDTO()
            {
                FullAddress = address.FullAddress,
                Id = address.Id
            };

            return addressDTO;

        }

        public async Task<List<AddressDTO>> GetAll()
        {
            List<Address> adresses = await _repository.GetAll();
            List<AddressDTO> adressDTOs = adresses.Select(adress => new AddressDTO
            {
                FullAddress = adress.FullAddress,
            }).ToList();

            return adressDTOs;
        }

        public async Task Update(AddressDTO model)
        {
            Address address = await _repository.GetDefault(x => x.Id == model.Id);
            await _repository.Update(address);
        }

    }
}
