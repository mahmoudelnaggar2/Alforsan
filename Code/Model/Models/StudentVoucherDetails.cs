using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class StudentVoucherDetails : BaseModel
    {
        public int StudentVoucherDetailsId { get; set; }
        public int StudentVoucherId { get; set; }
        public int FeesTypeId { get; set; }
        public decimal Fee { get; set; }

        public StudentVoucher StudentVoucher { get; set; }
        public FeesType FeesType { get; set; }
    }
}


