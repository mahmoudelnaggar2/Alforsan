using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class StudentVoucher : BaseModel
    {
        public int StudentVoucherId { get; set; }
        public int StudentId { get; set; }
        public int YearId { get; set; }
        public DateTime? VoucherDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public bool IsPaid { get; set; }
        public string Notes { get; set; }
        public decimal TotalVoucher { get; set; }
        public decimal TotalVoucherRefund { get; set; }


        public Student Student { get; set; }
        public Year Year { get; set; }
        public List<StudentVoucherDetails> StudentVoucherDetails { get; set; }
        public List<StudentFinancial> StudentFinancials { get; set; }
        public List<StudentVoucherRefund> StudentVoucherRefunds { get; set; }
        

        public StudentVoucher()
        {
            StudentVoucherDetails = new List<StudentVoucherDetails>();
            StudentFinancials=new List<StudentFinancial>();
            StudentVoucherRefunds=new List<StudentVoucherRefund>();
        }
    }
}