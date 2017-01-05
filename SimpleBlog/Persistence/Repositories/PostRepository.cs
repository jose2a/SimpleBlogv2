using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleBlog.Core.Domain;
using SimpleBlog.Core.Repositories;
using System.Data.Entity;

namespace SimpleBlog.Persistence.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(SimpleBlogContext contex)
            : base(contex)
        {
        }       

        public void UpdatePostTags(IEnumerable<Tag> selectedTags, IList<Tag> postTags)
        {
            foreach (var toAdd in selectedTags.Where(t => !postTags.Contains(t)))
            {
                postTags.Add(toAdd);
            }

            foreach (var toRemove in postTags.Where(t => !selectedTags.Contains(t)).ToList())
            {
                postTags.Remove(toRemove);
            }
        }
    }
}