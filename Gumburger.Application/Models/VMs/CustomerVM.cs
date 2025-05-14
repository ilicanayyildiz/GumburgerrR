using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gumburger.Domain.Entities.Concrete;
using Gumburger.Domain.Enums;

namespace Gumburger.Application.Models.VMs
{
    public class CustomerVM
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public string Phone { get; set; }
        public string? ProfileImagePath { get; set; }

        public ICollection<Address>? Addresses { get; set; }
        public ICollection<Order>? Orders { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Status Status { get; set; }
    }
}
