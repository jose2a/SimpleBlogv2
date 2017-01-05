using SimpleBlog.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository Posts { get; }
        IRoleRepository Roles { get; }
        ITagRepository Tags { get; }
        IUserRepository Users { get; }
        int Complete();
    }
}
