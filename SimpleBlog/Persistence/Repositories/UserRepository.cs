using SimpleBlog.Core.Domain;
using SimpleBlog.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SimpleBlog.Areas.Admin.ViewModels;

namespace SimpleBlog.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) 
            : base(context)
        {
        }

        public void UpdateUserRoles(IList<RoleCheckbox> checkboxes, IList<Role> roles)
        {
            var selectedRoles = new List<Role>();

            foreach (var role in ((SimpleBlogContext) context).Roles.ToList())
            {
                var checkbox = checkboxes.Single(c => c.Id == role.Id);
                checkbox.Name = role.Name;

                if (checkbox.IsChecked)
                {
                    selectedRoles.Add(role);
                }
            }

            foreach (var toAdd in selectedRoles.Where(t => !roles.Contains(t)))
            {
                roles.Add(toAdd);
            }

            foreach (var toRemove in roles.Where(r => !selectedRoles.Contains(r)).ToList())
            {
                roles.Remove(toRemove);
            }
        }

        //public void UpdateUserRoles(List<int> roleIds, User user)
        //{
        //    ((SimpleBlogContext)context).Database.ExecuteSqlCommand("DELETE FROM UserRoles WHERE UserId = @id", new { id = user.Id });

        //    var roles = (SimpleBlogContext)contex).Roles.Where(r => roleIds.Contains(r.Id)).ToList();
        //    user.Roles = roles;

        //}
    }
}