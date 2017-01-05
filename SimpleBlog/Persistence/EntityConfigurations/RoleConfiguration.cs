using SimpleBlog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace SimpleBlog.Persistence.EntityConfigurations
{
    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            // Keys
            HasKey(r => r.Id);

            // Properties
            Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(300);
        }
    }
}