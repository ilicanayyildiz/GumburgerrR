using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gumburger.Application.Models.DTOs;
using Gumburger.Application.Models.VMs;

namespace Gumburger.Application.Services.OrderServices
{
    public interface IOrderService
    {
        Task<List<OrderVM>> GetAll();

        Task Create(OrderDTO model);

        Task Update(OrderDTO model);

        Task Delete(Guid orderId);

        Task<List<CustomerOrdersVM>> GetCustomerOrders(Guid customerId);


    }
}
