using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class ReportInterviewDTO
    {
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }
        public int SchoolId { get; set; }
        public int GradeId { get; set; }

        public int InterViewStatusID { get; set; }

    }
}
