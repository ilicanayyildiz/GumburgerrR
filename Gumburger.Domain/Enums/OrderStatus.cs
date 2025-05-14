using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gumburger.Domain.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "Order Placed")]
        OrderPlaced = 1,
        [Display(Name = "In The Kitchen")]
        InTheKitchen,
        [Display(Name = "On The Way")]
        OnTheWay,
        [Display(Name = "Delivered")]
        Delivered
    }
}
