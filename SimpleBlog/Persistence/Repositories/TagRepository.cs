using System;
using System.Collections.Generic;
using System.Data.Entity;
using SimpleBlog.Areas.Admin.ViewModels;
using SimpleBlog.Core.Domain;
using SimpleBlog.Core.Repositories;
using System.Linq;
using SimpleBlog.Infrastructure;

namespace SimpleBlog.Persistence.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(DbContext context)
            : base(context)
        {
        }

        public IEnumerable<Tag> ReconsileTags(IEnumerable<TagCheckBox> tags)
        {
            foreach (var tag in tags.Where(t => t.IsChecked))
            {
                if (tag.Id != null)
                {
                    yield return Get(tag.Id.Value);
                    continue;
                }

                var existingTag = SingleOrDefault(t => t.Name == tag.Name);
                if (existingTag != null)
                {
                    yield return existingTag;
                    continue;
                }

                var newTag = new Tag
                {
                    Name = tag.Name,
                    Slug = tag.Name.Slugify()
                };

                Add(newTag);
                context.SaveChanges();

                yield return newTag;
            }
        }
    }
}