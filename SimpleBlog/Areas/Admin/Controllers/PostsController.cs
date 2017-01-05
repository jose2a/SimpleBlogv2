using SimpleBlog.Areas.Admin.ViewModels;
using SimpleBlog.Core.Domain;
using SimpleBlog.Infrastructure;
using SimpleBlog.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTab("posts")]
    public class PostsController : Controller
    {
        private const int PostPerPage = 10;

        // GET: Admin/Posts
        public ActionResult Index(int page = 1)
        {
            //var context = ((UnitOfWork)Database.UnitOfWork).Context;

            //var totalPostCount = context.Posts.Count();

            //var baseQuery = context.Posts.OrderByDescending(p => p.CreatedAt);

            //var postIds = baseQuery
            //    .Skip((page - 1) * PostPerPage)
            //    .Take(PostPerPage)
            //    .Select(p => p.Id)
            //    .ToList();

            //var currentPostPage = baseQuery
            //    .Where(p => postIds.Contains(p.Id))
            //    .Include(u => u.User)
            //    .Include(t => t.Tags)
            //    .ToList();
            ViewBag.Page = page;

            var totalPostCount = Database.UnitOfWork.Posts.GetAll().Count();

            var postIds = Database.UnitOfWork.Posts
                .GetAll()
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * PostPerPage)
                .Take(PostPerPage)
                .Select(p => p.Id)
                .ToList();

            var currentPostPage = ((UnitOfWork)Database.UnitOfWork).Context.Posts                
                .Where(p => postIds.Contains(p.Id))
                .Include(u => u.User)
                .Include(t => t.Tags)
                .OrderByDescending(p => p.CreatedAt)
                .ToList();

            return View(new PostsIndex
            {
                Posts = new PageData<Post>(currentPostPage, totalPostCount, page, PostPerPage)
            });
        }

        public ActionResult New()
        {
            return View("Form", new PostForm
            {
                IsNew = true,
                Tags = Database.UnitOfWork.Tags.GetAll().Select(t => new TagCheckBox
                {
                    Id = t.Id,
                    Name = t.Name,
                    IsChecked = false
                }).ToList()
            });
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Form(PostForm form)
        {
            form.IsNew = form.PostId == null;

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var selectedTags = Database.UnitOfWork.Tags.ReconsileTags(form.Tags);

            Post post;

            if (form.IsNew)
            {
                post = new Post
                {
                    CreatedAt = DateTime.UtcNow,
                    User = Auth.User
                };

                foreach (var tag in selectedTags)
                {
                    post.Tags.Add(tag);
                }
            }
            else
            {
                post = Database.UnitOfWork.Posts.Get(form.PostId.Value);

                if (post == null)
                {
                    return HttpNotFound();
                }

                post.UpdatedAt = DateTime.UtcNow;

                Database.UnitOfWork.Posts.UpdatePostTags(selectedTags, post.Tags);

            }

            post.Title = form.Title;
            post.Slug = form.Slug;
            post.Content = form.Content;

            if (form.IsNew)
            {
                Database.UnitOfWork.Posts.Add(post);
            }

            Database.UnitOfWork.Complete();

            return RedirectToAction("index");
        }

        public ActionResult Edit(int id)
        {
            var post = Database.UnitOfWork.Posts.Get(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            return View("Form", new PostForm
            {
                IsNew = false,
                PostId = id,
                Content = post.Content,
                Slug = post.Slug,
                Title = post.Title,
                Tags = Database.UnitOfWork.Tags.GetAll().Select(t => new TagCheckBox
                {
                    Id = t.Id,
                    Name = t.Name,
                    IsChecked = post.Tags.Contains(t)
                }).ToList()
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Trash(int id, int page)
        {
            var post = Database.UnitOfWork.Posts.Get(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            post.DeletedAt = DateTime.UtcNow;

            Database.UnitOfWork.Complete();

            return RedirectToAction("index", new { page = page });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int page)
        {
            var post = Database.UnitOfWork.Posts.Get(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            Database.UnitOfWork.Posts.Remove(post);

            Database.UnitOfWork.Complete();

            return RedirectToAction("index", new { page = page });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Restore(int id, int page)
        {
            var post = Database.UnitOfWork.Posts.Get(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            post.DeletedAt = null;

            Database.UnitOfWork.Complete();

            return RedirectToAction("index", new { page = page });
        }
    }
}