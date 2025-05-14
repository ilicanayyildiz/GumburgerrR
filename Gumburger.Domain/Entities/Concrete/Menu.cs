using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gumburger.Domain.Entities.Abstract;
using Gumburger.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Gumburger.Domain.Entities.Concrete
{
    public class Menu : BaseEntity, IEntity<Guid>
    {
        public Menu()
        {
            OrdersMenus = new List<OrdersMenus>();
        }
        public Guid Id { get; set; }
        public string MenuName { get; set; }

        private decimal _menuPrice;

        public decimal MenuPrice
        {
            get { return _menuPrice; }
            set { _menuPrice = (value < 0) ? 0 : value; }
        }
        public MenuSize MenuSize { get; set; } = MenuSize.Small;
        public string? MenuImagePath { get; set; }

        [NotMapped]
        public IFormFile MenuUploadPath { get; set; }

        //Navigation Property
        public ICollection<OrdersMenus>? OrdersMenus { get; set; }
    }
}
