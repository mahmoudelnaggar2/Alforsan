using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class Gender:BaseModel
    {
        public int GenderId { get; set; }
        public string GenderName { get; set; }
    }
}
