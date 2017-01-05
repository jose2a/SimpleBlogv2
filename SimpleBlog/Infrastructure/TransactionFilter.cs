using SimpleBlog.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Infrastructure
{   
    [AttributeUsage(AttributeTargets.Method)] 
    public class TransactionFilter : ActionFilterAttribute, IActionFilter, IExceptionFilter
    {
        //private DbContextTransaction _transaction;

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Here the transactions are supposed to be executed
            //_transaction = Database.Context.Database.CurrentTransaction;
            //if (_transaction != null)
            //{
            //    _transaction.Commit();
            //}            
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //_transaction = Database.Context.Database.BeginTransaction();
        }

        public void OnException(ExceptionContext filterContext)
        {
            //_transaction = Database.Context.Database.CurrentTransaction;
                            
            //if (_transaction != null)
            //{                
            //    _transaction.Rollback();
            //}
        }
    }
}