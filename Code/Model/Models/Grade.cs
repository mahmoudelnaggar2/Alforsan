using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Grade:BaseModel
    {
        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public int Order { get; set; }
        public int SchoolId { get; set; }

        public School School { get; set; }        
    }
}
