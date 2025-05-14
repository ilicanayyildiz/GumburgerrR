using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gumburger.Application.Models.DTOs;
using Gumburger.Application.Models.VMs;
using Gumburger.Domain.Entities.Concrete;
using Gumburger.Domain.Enums;
using Gumburger.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gumburger.Application.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IBaseRepository<Order> _repository;

        public OrderService(IBaseRepository<Order> repository)
        {
            _repository = repository;
        }
        public async Task Create(OrderDTO model)
        {
            Order order = new Order()
            {

                OrderDate = model.OrderDate,
                Notes = model.Notes
            };

            await _repository.Insert(order);
        }

        public Task Delete(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderVM>> GetAll()
        {
            return await _repository.GetFilteredList(
                select: x => new OrderVM()
                {
                    OrderId = x.Id,
                    OrderDate = x.OrderDate,
                    ShippedAddress = x.ShippedAddress,
                    Notes = x.Notes,
                    OrderStatus = x.OrderStatus,
                    Quantity = x.OrderQuantity,
                    ShippedDate = x.ShippedDate,
                    CustomerFullName = x.AppUser.FirstName + " " + x.AppUser.LastName,
                },
                where: x => x.Status == Status.Active || x.Status == Status.Passive || x.Status == Status.Deleted,
                orderBy: x => x.OrderByDescending(x => x.OrderDate),
                include: x => x.Include(x => x.AppUser)
                );
        }

        public async Task<List<CustomerOrdersVM>> GetCustomerOrders(Guid customerId)
        {
            return await _repository.GetFilteredList(
            select: x => new CustomerOrdersVM()
            {
                OrderId = x.Id,
                CustomerFullName = x.AppUser.FirstName + " " + x.AppUser.LastName,
                OrderDate = x.OrderDate,
                OrderStatus = x.OrderStatus,
                ShippedAddress = x.ShippedAddress,
                ShippedDate = x.ShippedDate,
                Notes = x.Notes,
                Quantity = x.OrderQuantity
            },
            where: x => x.AppUserId == customerId,
            orderBy: x => x.OrderByDescending(x => x.OrderDate),
            include: x => x.Include(x => x.AppUser)
            );
        }

        public async Task Update(OrderDTO model)
        {
            Order order = await _repository.GetDefault(x => x.Id == model.Id);
            await _repository.Update(order);
        }



    }
}
