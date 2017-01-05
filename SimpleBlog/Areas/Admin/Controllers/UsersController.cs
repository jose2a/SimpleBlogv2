using SimpleBlog.Areas.Admin.ViewModels;
using SimpleBlog.Core;
using SimpleBlog.Core.Domain;
using SimpleBlog.Infrastructure;
using SimpleBlog.Persistence;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTab("users")]
    public class UsersController : Controller
    {
        // GET: Admin/Users
        public ActionResult Index()
        {
            return View(new UsersIndex
            {
                Users = Database.UnitOfWork.Users.GetAll()
            });
        }

        public ActionResult New()
        {
            return View(new UsersNew
            {
                Roles = Database.UnitOfWork.Roles.GetAll().Select(r => new RoleCheckbox
                {
                    Id = r.Id,
                    IsChecked = false,
                    Name = r.Name
                }).ToList()
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        //[TransactionFilter]
        public ActionResult New(UsersNew form)
        {
            var user = new User();
            Database.UnitOfWork.Users.UpdateUserRoles(form.Roles, user.Roles);

            //if (Database.UnitOfWork.Users.SingleOrDefault(u => u.Username == form.Username) != null)
            //{
            //    ModelState.AddModelError("Username", "Username must be unique.");
            //}

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            user.Username = form.Username;
            user.Email = form.Email;

            user.SetPassword(form.Password);
            Database.UnitOfWork.Users.Add(user);

            Database.UnitOfWork.Complete();

            return RedirectToAction("index");

        }

        public ActionResult Edit(int id)
        {
            var user = Database.UnitOfWork.Users.Get(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(new UsersEdit
            {
                Username = user.Username,
                Email = user.Email,
                Roles = Database.UnitOfWork.Roles.GetAll().Select(r => new RoleCheckbox
                {
                    Id = r.Id,
                    IsChecked = user.Roles.Contains(r),
                    Name = r.Name
                }).ToList()
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(UsersEdit form)
        {
            var user = Database.UnitOfWork.Users.Get(form.Id);
            Database.UnitOfWork.Users.UpdateUserRoles(form.Roles, user.Roles);

            if (user == null)
            {
                return HttpNotFound();
            }

            if (Database.UnitOfWork.Users.SingleOrDefault(u => u.Username == form.Username && u.Id != form.Id) != null)
            {
                ModelState.AddModelError("Username", "Username must be unique.");
            }

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            user.Username = form.Username;
            user.Email = form.Email;

            Database.UnitOfWork.Complete();

            return RedirectToAction("index");
        }

        public ActionResult ResetPassword(int id)
        {
            var user = Database.UnitOfWork.Users.Get(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(new UsersResetPassword
            {
                Username = user.Username
            });
        }
        
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ResetPassword(int id, UsersResetPassword form)
        {
            var user = Database.UnitOfWork.Users.Get(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            form.Username = user.Username;

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            user.SetPassword(form.Password);

            Database.UnitOfWork.Complete();

            return RedirectToAction("index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var user = Database.UnitOfWork.Users.Get(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            Database.UnitOfWork.Users.Remove(user);
            Database.UnitOfWork.Complete();

            return RedirectToAction("index");
        }
    }
}