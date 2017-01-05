using SimpleBlog.Persistence;
using SimpleBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SimpleBlog.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToRoute("home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(AuthLogin form, string returnUrl)
        {
            var user = Database.UnitOfWork.Users.SingleOrDefault(u => u.Username == form.Username);

            if (user == null)
            {
                SimpleBlog.Core.Domain.User.FakeHash();
            }

            if (user == null || !user.CheckPassword(form.Password))
            {
                ModelState.AddModelError("Username", "Username or password is incorrect.");
            }

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            FormsAuthentication.SetAuthCookie(user.Username, true);

            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToRoute("Home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("home");
        }
    }
}