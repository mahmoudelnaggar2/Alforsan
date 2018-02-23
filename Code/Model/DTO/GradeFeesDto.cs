using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class GradeFeesDto
    {
        public int GradeId { get; set; }
        public List<GradeFeeObjDto> GradeFees { get; set; }

        public GradeFeesDto()
        {
            this.GradeFees=new List<GradeFeeObjDto>();
        }         
    }
}
