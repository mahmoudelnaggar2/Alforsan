using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class StudentStatus:BaseModel
    {
        public int StudentStatusId { get; set; }
        public string StudentStatusName { get; set; }
    }
}
