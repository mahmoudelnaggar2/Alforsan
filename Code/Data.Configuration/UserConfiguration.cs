using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configuration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users");
            Property(u => u.FullName).IsRequired().IsMaxLength();
            Property(u => u.UserName).IsRequired().HasMaxLength(150);
            Property(u => u.UserEmail).IsRequired().HasMaxLength(255);
            Property(u => u.UserPassword).IsRequired().HasMaxLength(200);
            Property(u => u.UserEmail).IsOptional();
        }
    }
}
