using SimpleBlog.Core.Domain;
using SimpleBlog.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using System.Web;

namespace SimpleBlog.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbSet<TEntity> Entity;

        protected DbContext context;        

        public Repository(DbContext context)
        {
            this.context = context;
            Entity = context.Set<TEntity>();
        }

        public TEntity Get(int id)
        {
            return Entity.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Entity.ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Entity.Where(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Entity.SingleOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            using (var transaction = new TransactionScope())
            {
                try
                {
                    Entity.Add(entity);

                    transaction.Complete();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }        
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Entity.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            Entity.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Entity.RemoveRange(entities);
        }
    }
}