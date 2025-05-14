using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gumburger.Domain.Entities.Abstract;

namespace Gumburger.Domain.Entities.Concrete
{
    public class Extra : BaseEntity, IEntity<Guid>
    {
        public Extra()
        {
            OrdersExtras = new List<OrdersExtras>();
        }
        public Guid Id { get; set; }
        public string ExtraName { get; set; }
        public string? ExtraImageUrl { get; set; }
        private decimal _extraPrice;

        public decimal ExtraPrice
        {
            get { return _extraPrice; }
            set { _extraPrice = (value < 0) ? 0 : value; }
        }

        //Navigation Property
        public ICollection<OrdersExtras>? OrdersExtras { get; set; }
    }
}
