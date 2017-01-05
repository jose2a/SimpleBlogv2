using SimpleBlog.Core;
using SimpleBlog.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SimpleBlog
{
    public class Database
    {
        private const string SessionKey = "SimpleBlog.Database.SessionKey";

        public static IUnitOfWork UnitOfWork
        {
            get
            {
                return (UnitOfWork)HttpContext.Current.Items[SessionKey];                
            }
        }

        public static void CreateUnitOfWork()
        {
            if (HttpContext.Current.Items[SessionKey] == null)
            {
                HttpContext.Current.Items[SessionKey] = new UnitOfWork(new SimpleBlogContext());
            }
        }

        public static void DisposeUnitOfWork()
        {
            var unitOfWork = HttpContext.Current.Items[SessionKey] as UnitOfWork;

            if (unitOfWork != null)
            {
                unitOfWork.Dispose();
            }
            HttpContext.Current.Items.Remove(SessionKey);
        }
    }
}