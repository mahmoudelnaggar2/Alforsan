using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Emergency:BaseModel
    {
        public int EmergencyId { get; set; }
        public string ContactName { get; set; }
        public string Relationship { get; set; }
        public string MobileNumber { get; set; }
        public int StudentId { get; set; }

        public Student Student { get; set; }

    }
}
