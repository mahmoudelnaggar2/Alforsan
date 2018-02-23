using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class VoucherType : BaseModel
    {
        public int VoucherTypeId { get; set; }
        public string VoucherTypeName { get; set; }
    }
}
