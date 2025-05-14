using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gumburger.Domain.Entities.Concrete;
using Gumburger.Domain.Repositories;

namespace Gumburger.Application.Services.Concrete
{
    public class OrderManager : BaseManager<Order>
    {
        public OrderManager(IBaseRepository<Order> repository) : base(repository)
        {
        }
    }
}
