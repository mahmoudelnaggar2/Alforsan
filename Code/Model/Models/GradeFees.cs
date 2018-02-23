using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class GradeFees:BaseModel
    {
        public int GradeFeesId { get; set; }
        public int YearId { get; set; }
        public int? JoiningYearId { get; set; }

        public int GradeId { get; set; }
        public int FeesTypeId { get; set; }
        public decimal Fee { get; set; }

        public Year Year { get; set; }
        public Year JoiningYear  { get; set; }

        public Grade Grade { get; set; }
        public FeesType FeesType { get; set; }

    }
}
