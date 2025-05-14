using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gumburger.Domain.Entities.Abstract;
using Gumburger.Domain.Enums;

namespace Gumburger.Domain.Entities.Concrete
{
    public class Order : BaseEntity, IEntity<Guid>
    {
        public Order()
        {
            OrdersMenus = new List<OrdersMenus>();
            OrdersExtras = new List<OrdersExtras>();
        }
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        public short OrderQuantity { get; set; } = 1;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.OrderPlaced;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime? ShippedDate { get; set; }
        public string? Notes { get; set; }
        public string ShippedAddress { get; set; }

        //Navigation Property
        public AppUser AppUser { get; set; }
        public ICollection<OrdersMenus> OrdersMenus { get; set; }
        public ICollection<OrdersExtras>? OrdersExtras { get; set; }
    }
}
