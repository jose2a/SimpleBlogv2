using SimpleBlog.Areas.Admin.ViewModels;
using SimpleBlog.Core.Domain;
using System.Collections.Generic;

namespace SimpleBlog.Core.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        IEnumerable<Tag> ReconsileTags(IEnumerable<TagCheckBox> tags);
    }
}
