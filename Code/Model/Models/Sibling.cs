using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Sibling:BaseModel
    {
        public int SiblingId { get; set; }
      //  public string SiblingName { get; set; }
       // public int SchoolId { get; set; }
       // public int GradeId { get; set; }
        public int StudentId { get; set; }
        public int SiblingStudentId { get; set; }

        //   public School School { get; set; }
        //   public Grade Grade { get; set; }

        public Student Student { get; set; }
        public Student SiblingStudent { get; set; }

    }
}
