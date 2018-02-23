using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class StudentVoucherRefund:BaseModel
    {
        public int StudentVoucherRefundId { get; set; }
        public int StudentId { get; set; }
        public DateTime? VoucherRefundDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public bool IsPaid { get; set; }
        public decimal TotalRefund { get; set; }
        public int StudentVoucherId { get; set; }

        public Student Student { get; set; }
        public StudentVoucher StudentVoucher { get; set; }
    }
}
