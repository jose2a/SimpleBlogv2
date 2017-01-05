using SimpleBlog.Core.Domain;
using System.Collections.Generic;

namespace SimpleBlog.Core.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        void UpdatePostTags(IEnumerable<Tag> selectedTags, IList<Tag> postTags);
    }
}
