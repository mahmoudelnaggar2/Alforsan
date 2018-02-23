using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class StudentFinancial : BaseModel
    {
        public int StudentFinancialId { get; set; }
        public int StudentId { get; set; }
        public int StudentVoucherId { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Notes { get; set; }
        
        public int VoucherTypeId { get; set; }

        public Student Student { get; set; }
        public StudentVoucher StudentVoucher { get; set; }
        public VoucherType VoucherType { get; set; }
    }
}
