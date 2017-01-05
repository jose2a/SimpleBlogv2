using SimpleBlog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace SimpleBlog.Persistence.EntityConfigurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            // Keys
            HasKey(u => u.Id);

            // Properties
            Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(250);

            Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(200);

            Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(2000);

            // Relationships
            HasMany(u => u.Roles)
                .WithMany(u => u.Users)
                .Map(m =>
                {
                    m.ToTable("UserRoles");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("RoleId");
                });
        }
    }
}