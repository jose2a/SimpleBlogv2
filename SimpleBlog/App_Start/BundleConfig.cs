using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace SimpleBlog
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/admin/styles")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/Admin.css"));

            bundles.Add(new StyleBundle("~/styles")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/Site.css"));

            bundles.Add(new ScriptBundle("~/admin/scripts")
                .Include("~/Scripts/jquery-3.1.0.js")
                .Include("~/Scripts/jquery.validate.js")
                .Include("~/Scripts/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/areas/admin/scripts/Forms.js"));

            bundles.Add(new ScriptBundle("~/admin/post/scripts")
                .Include("~/areas/admin/scripts/PostEditor.js"));

            bundles.Add(new ScriptBundle("~/scripts")
                .Include("~/Scripts/jquery-3.1.0.js")
                .Include("~/Scripts/jquery.timeago.js")
                .Include("~/Scripts/jquery.validate.js")
                .Include("~/Scripts/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/Frontend.js"));
        }

    }
}