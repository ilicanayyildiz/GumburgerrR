using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gumburger.Domain.Entities.Concrete;
using Gumburger.Domain.Repositories;
using Gumburger.Infrastructure.Context;

namespace Gumburger.Infrastructure.Repositories
{
    public class AppUserRepository : BaseRepository<AppUser>
    {
        public AppUserRepository(AppDbContext context) : base(context)
        {
        }
    }
}
