using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class NewApplicantsDTO
    {
        public int SchoolId { get; set; }
        public int? GradeId { get; set; }
        public int UserId { get; set; }
        public bool IsPDF { get; set; }
    }
}
