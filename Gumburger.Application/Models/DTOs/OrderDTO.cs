using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gumburger.Application.Models.DTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public string Notes { get; set; }

    }
}
