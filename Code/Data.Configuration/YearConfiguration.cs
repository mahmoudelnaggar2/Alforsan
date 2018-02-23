using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Model;

namespace Data.Configuration
{
    public class YearConfiguration:EntityTypeConfiguration<Year>
    {
        public YearConfiguration()
        {
            ToTable("Years");
            //Property(y => y.YearName).IsRequired();
            //Property(y => y.ShortName).IsRequired();
            //Property(y => y.IsCurrent).IsRequired();
        }
    }
}
