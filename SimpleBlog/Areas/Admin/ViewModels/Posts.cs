﻿using SimpleBlog.Core.Domain;
using SimpleBlog.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleBlog.Areas.Admin.ViewModels
{
    public class TagCheckBox
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }

    public class PostsIndex
    {
        public PageData<Post> Posts { get; set; }
    }

    public class PostForm
    {
        public bool IsNew { get; set; }
        public int? PostId { get; set; }

        [Required, MaxLength(128)]
        public string Title { get; set; }
        [Required, MaxLength(128)]
        public string Slug { get; set; }
        [Required, DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public IList<TagCheckBox> Tags { get; set; }
    }
}