using SimpleBlog.Areas.Admin.ViewModels;
using SimpleBlog.Core.Domain;
using System.Collections.Generic;

namespace SimpleBlog.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        void UpdateUserRoles(IList<RoleCheckbox> checkboxes, IList<Role> roles);
    }
}
