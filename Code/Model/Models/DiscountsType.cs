using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DiscountsType : BaseModel
    {
        public int DiscountsTypeID { get; set; }
        public string DiscountName { get; set; }
        public decimal DiscountPercentage { get; set; }
    }
}
