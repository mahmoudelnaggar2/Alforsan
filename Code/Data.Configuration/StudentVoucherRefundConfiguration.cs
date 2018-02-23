using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Model;

namespace Data.Configuration
{
    public class StudentVoucherRefundConfiguration: EntityTypeConfiguration<StudentVoucherRefund>
    {
        public StudentVoucherRefundConfiguration()
        {
            ToTable("StudentVoucherRefunds");
            Property(s => s.StudentId).IsRequired();
            Property(s => s.StudentVoucherId).IsRequired();
        }
    }
}
