using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configuration
{
    public class InterviewStatusConfiguration:EntityTypeConfiguration<Model.InterviewStatus>
    {
        public InterviewStatusConfiguration()
        {
            ToTable("InterviewStatus");
        }
    }
}
