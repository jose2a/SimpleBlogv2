using SimpleBlog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace SimpleBlog.Persistence.EntityConfigurations
{
    public class PostConfiguration : EntityTypeConfiguration<Post>
    {
        public PostConfiguration()
        {
            
            // 2. Primary Keys
            HasKey(p => p.Id);

            // 3. Properties configuration
            Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(500);

            Property(p => p.Slug)
                .IsRequired()
                .HasMaxLength(500);

            Property(p => p.Content)
                .IsRequired()
                .HasMaxLength(2000);

            Property(p => p.CreatedAt)
                .IsRequired();

            Ignore(p => p.IsDeleted);

            // 4. Relationships

            HasRequired(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .WillCascadeOnDelete(false);

            HasMany(p => p.Tags)
                .WithMany(t => t.Posts)
                .Map(m =>
                {
                    m.ToTable("PostTags");
                    m.MapLeftKey("PostId");
                    m.MapRightKey("TagId");
                });
        }
    }
}