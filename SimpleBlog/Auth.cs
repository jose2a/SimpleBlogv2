using SimpleBlog.Core.Domain;
using SimpleBlog.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog
{
    public class Auth
    {
        private const string UserKey = "SimpleBlog.Auth.UserKey";

        public static User User {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return null;
                }

                var user = HttpContext.Current.Items[UserKey] as User;

                if (user == null)
                {
                    user = Database.UnitOfWork.Users.SingleOrDefault(u => u.Username == HttpContext.Current.User.Identity.Name);

                    if (user == null)
                    {
                        return null;
                    }

                    HttpContext.Current.Items[UserKey] = user;
                }
                return user;
            }
        }
    }
}