using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.Entity.ModelConfiguration;

namespace Data.Configuration
{
    public class StudentVoucherConfiguration : EntityTypeConfiguration<StudentVoucher>
    {
        public StudentVoucherConfiguration()
        {
            ToTable("StudentVouchers");
            Property(s => s.StudentId).IsRequired();
            Property(s => s.YearId).IsRequired();
        }
    }
}