using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Parent:BaseModel
    {
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string HomeNumber { get; set; }
        public string Email { get; set; }
        public string IdNumber { get; set; }
        public string Occupation { get; set; }
        public string Employer { get; set; }
        public string School { get; set; }
        public string University { get; set; }
        public string HomeAddress { get; set; }

        public int? NationalityId { get; set; }
        public Nationality Nationality { get; set; }
        public List<Student> StudentsFather { get; set; }
        public List<Student> StudentsMother { get; set; }
    }
}
