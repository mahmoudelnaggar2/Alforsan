using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class User : BaseModel
    {
        
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public int? SchoolId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public bool IsActive { get; set; }

        public Role Role { get; set; }
        public School School { get; set; }
      
        public User()
        {
          
        }
    }
}
