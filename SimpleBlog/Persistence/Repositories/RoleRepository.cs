using System.Data.Entity;
using SimpleBlog.Core.Domain;
using SimpleBlog.Core.Repositories;

namespace SimpleBlog.Persistence.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext context)
            : base(context)
        {
        }
    }
}