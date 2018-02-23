using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Year:BaseModel
    {
        public int YearId { get; set; }
        public string YearName { get; set; }
        public int ShortName { get; set; }
        public bool IsCurrent { get; set; }
        //  public decimal ApplicationFees { get; set; }
        public List<Student> Students { get; set; }
        public List<GradeFees> JoiningGradeFees { get; set; }
        public List<GradeFees> GradeFees { get; set; }


        public Year()
        {
            Students = new List<Model.Student>();
        }
    }
}
