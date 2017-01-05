using SimpleBlog.Core.Domain;
using SimpleBlog.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.ViewModels
{
    public class PostsIndex
    {
        public PageData<Post> Posts { get; set; }
    }

    public class PostShow
    {
        public Post Post { get; set; }
    }

    public class PostsTag
    {
        public Tag Tag { get; set; }
        public PageData<Post> Posts { get; set; }
    }
}