using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class RoleRight : BaseModel
    {
        public int RoleRightID { get; set; }
        public int RoleID { get; set; }
        public int RightID { get; set; }

        public Role Role { get; set; }
        public Right Right { get; set; }

        public RoleRight()
        {
            
        }
    }
}
