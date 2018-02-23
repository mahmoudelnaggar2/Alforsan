using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class StudentLanguage:BaseModel
    {
        public int StudentLanguageId { get; set; }

        public int StudentId { get; set; }
        public int LanguageId { get; set; }

        public Student Student  { get; set; }
        public Language Language { get; set; }
    }
}
