using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gumburger.Domain.Enums;

namespace Gumburger.Application.Models.VMs
{
    public class MenuVM
    {

        public Guid MenuId { get; set; }
        public string MenuName { get; set; }
        public decimal MenuPrice { get; set; }
        public MenuSize MenuSize { get; set; }
        public string MenuImagePath { get; set; }


    }
}
