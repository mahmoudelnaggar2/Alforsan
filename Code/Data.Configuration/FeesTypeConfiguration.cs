using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Data.Configuration
{
   public class FeesTypeConfiguration:EntityTypeConfiguration<FeesType>
    {
        public FeesTypeConfiguration()
        {
            ToTable("FeesTypes");
            Property(f => f.FeeName).IsRequired().IsMaxLength();
            Ignore(f => f.IsPaid);
        }
    }
}
