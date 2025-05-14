using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gumburger.Application.Models.VMs
{
    public class ExtraVM
    {
        public Guid Id { get; set; }
        public string ExtraName { get; set; }
        public string ExtraImageUrl { get; set; }
        public decimal ExtraPrice { get; set; }
    }
}
