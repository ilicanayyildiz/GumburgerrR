using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gumburger.Domain.Entities.Concrete
{
    public class OrdersExtras : BaseEntity
    {
        private decimal _extraPrice;
        public decimal ExtraPrice
        {
            get { return _extraPrice; }
            set { _extraPrice = (decimal)((value < 0) ? 0 : value); }
        }

        private short _extraQuantity;
        public short ExtraQuantity
        {
            get { return _extraQuantity; }
            set { _extraQuantity = (short)((value < 0) ? 0 : value); }
        }

        public decimal ExtraTotalPrice
        {
            get
            {
                return ExtraPrice * ExtraQuantity;
            }
            private set { }
        }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid ExtraId { get; set; }
        public Extra Extra { get; set; }
    }
}
