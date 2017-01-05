using SimpleBlog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleBlog.Core.Repositories;
using SimpleBlog.Persistence.Repositories;

namespace SimpleBlog.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SimpleBlogContext _contex;

        public UnitOfWork(SimpleBlogContext context)
        {
            _contex = context;

            Posts = new PostRepository(context);
            Roles = new RoleRepository(context);
            Tags = new TagRepository(context);
            Users = new UserRepository(context);            
        }

        public IPostRepository Posts { get; private set; }
        public IRoleRepository Roles { get; private set; }
        public ITagRepository Tags { get; private set; }
        public IUserRepository Users { get; private set; }

        public int Complete()
        {
            return _contex.SaveChanges();
        }

        public void Dispose()
        {
            _contex.Dispose();
        }

        public SimpleBlogContext Context {
            get {
                return _contex;
            }
        }
    }
}