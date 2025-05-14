using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gumburger.Domain.Entities.Abstract;
using Gumburger.Domain.Enums;

namespace Gumburger.Domain.Entities.Concrete
{
    public abstract class BaseEntity : IBaseEntity
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? DeletedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Status Status { get; set; } = Status.Active;
    }
}
