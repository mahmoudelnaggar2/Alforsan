using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class FeesType:BaseModel
    {
        public int FeesTypeId { get; set; }
        public string FeeName { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsBank { get; set; }
        public bool IsPayOnce { get; set; }
        public bool AllowDiscounts { get; set; }
    }
}
