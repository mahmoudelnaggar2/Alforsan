using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.Entity.ModelConfiguration;

namespace Data.Configuration
{
    public class StudetFinancialConfiguration : EntityTypeConfiguration<StudentFinancial>
    {
        public StudetFinancialConfiguration()
        {
            ToTable("StudentFinancials");
            Property(s => s.StudentId).IsRequired();
            Property(s => s.StudentVoucherId).IsRequired();
            Property(s => s.VoucherTypeId).IsRequired();
        }
    }

}
