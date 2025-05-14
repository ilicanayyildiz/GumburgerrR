using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gumburger.Domain.Entities.Concrete;
using Gumburger.Domain.Repositories;

namespace Gumburger.Application.Services.Concrete
{
    public class OrdersMenusManager : BaseManager<OrdersMenus>
    {
        public OrdersMenusManager(IBaseRepository<OrdersMenus> repository) : base(repository)
        {
        }
    }
}
