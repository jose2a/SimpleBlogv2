using SimpleBlog.Core.Domain;
using SimpleBlog.Infrastructure;
using SimpleBlog.Persistence;
using SimpleBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Controllers
{
    public class PostsController : Controller
    {
        private const int PostPerPage = 10;
        // GET: Posts
        public ActionResult Index(int page = 1)
        {
            var context = ((UnitOfWork)Database.UnitOfWork).Context;

            var baseQuery = context.Posts
                .Where(t => t.DeletedAt == null)
                .OrderByDescending(t => t.CreatedAt);

            var totalPostCount = baseQuery.Count();
            var postIds = baseQuery
                .Skip((page - 1) * PostPerPage)
                .Take(PostPerPage)
                .Select(p => p.Id)
                .ToList();

            var posts = baseQuery
                .Where(p => postIds.Contains(p.Id))
                .Include(u => u.User)
                .Include(t => t.Tags)
                .ToList();


            return View(new PostsIndex
            {
                Posts = new PageData<Post>(posts, totalPostCount, page, PostPerPage)
            });
        }

        public ActionResult Tag(string idAndSlug, int page = 1)
        {
            var parts = SeparateIdAndSlug(idAndSlug);

            if (parts == null)
            {
                return HttpNotFound();
            }

            var tag = Database.UnitOfWork.Tags.Get(parts.Item1);
            if (tag == null)
            {
                return HttpNotFound();
            }

            if (!tag.Slug.Equals(parts.Item2, StringComparison.CurrentCultureIgnoreCase))
            {
                return RedirectToActionPermanent("tag", new { id = parts.Item1, slug = tag.Slug });
            }

            var totalPostCount = tag.Posts.Count;
            var postIds = tag.Posts
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * PostPerPage)
                .Take(PostPerPage)
                .Where(t => t.DeletedAt == null)
                .Select(t => t.Id)
                .ToList();

            var posts = ((UnitOfWork)Database.UnitOfWork).Context.Posts
                .Where(p => postIds.Contains(p.Id))
                .OrderByDescending(p => p.CreatedAt)
                .Include(u => u.User)
                .Include(t => t.Tags)
                .ToList();

            return View(new PostsTag
            {
                Tag = tag,
                Posts = new PageData<Post>(posts, totalPostCount, page, PostPerPage)
            });
        }

        public ActionResult Show(string idAndSlug)
        {
            var parts = SeparateIdAndSlug(idAndSlug);

            if (parts == null)
            {
                return HttpNotFound();
            }

            var post = Database.UnitOfWork.Posts.Get(parts.Item1);
            if (post == null || post.IsDeleted)
            {
                return HttpNotFound();
            }

            if (!post.Slug.Equals(parts.Item2, StringComparison.CurrentCultureIgnoreCase))
            {
                return RedirectToRoutePermanent("Post", new { id = parts.Item1, slug = post.Slug });
            }

            return View(new PostShow
            {
                Post = post
            });
        }

        private Tuple<int, string> SeparateIdAndSlug(string idAndSlug)
        {
            var matches = Regex.Match(idAndSlug, @"^(\d+)\-(.*)?$");

            if (!matches.Success)
            {
                return null;
            }

            var id = int.Parse(matches.Result("$1"));
            var slug = matches.Result("$2");

            return Tuple.Create(id, slug);
        }
    }
}