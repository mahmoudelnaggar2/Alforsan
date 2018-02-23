using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class School:BaseModel
    {
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public int Prefix { get; set; }

        public List<Grade> Grades { get; set; }

        public School()
        {
            Grades=new List<Grade>();
        }
    }
}
