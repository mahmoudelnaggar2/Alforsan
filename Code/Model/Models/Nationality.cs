using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class Nationality:BaseModel
    {
        public int NationalityId { get; set; }
        public string NationalityName { get; set; }
    }
}
