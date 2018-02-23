using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Language:BaseModel
    {
        public int LanguageId { get; set; }

        public string LanguageName { get; set; }


    }
}