using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Custody:BaseModel
    {
        public int CustodyId  { get; set; }
        public string CustodyName { get; set; }

    }
}
