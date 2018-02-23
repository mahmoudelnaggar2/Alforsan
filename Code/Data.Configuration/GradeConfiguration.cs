using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Data.Configuration
{
    public class GradeConfiguration:EntityTypeConfiguration<Grade>
    {
        public GradeConfiguration()
        {
            ToTable("Grades");
            Property(g => g.GradeName).IsRequired();
            Property(g => g.SchoolId).IsRequired();
        }
    }
}
