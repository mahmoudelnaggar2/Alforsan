using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Student
    {
        public string strDeliveryDate
        {
            get
            {
                if (ApplicationDate != null)
                    return ApplicationDate.Date.ToString(SettingDTO.DateFormat).Split(' ')[0];
                else
                    return string.Empty;
            }
        }

       public string ApplicantName => $"{FirstName} {MiddleName} {FamilyName}";

        public decimal Balance { get; set; }

        public DateTime? DateTimeFrom { get; set; }

        public DateTime? DateTimeTo { get; set; }

        public List<int> CheckedInterviewStatus { get; set; }
        public List<StudentStatusDto> StudentStatusDTO { get; set; }
    }
}
