using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gumburger.Domain.Entities.Concrete;
using Gumburger.Infrastructure.Context;

namespace Gumburger.Infrastructure.Repositories
{
    public class ExtraRepository : BaseRepository<Extra>
    {
        public ExtraRepository(AppDbContext context) : base(context)
        {
        }

    }
}
