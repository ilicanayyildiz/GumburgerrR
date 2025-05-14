using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gumburger.Domain.Entities.Abstract;

namespace Gumburger.Domain.Entities.Concrete
{
    public class Address : BaseEntity, IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string FullAddress { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}
