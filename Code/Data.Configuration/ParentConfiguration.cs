using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Data.Configuration
{
    public class ParentConfiguration:EntityTypeConfiguration<Parent>
    {
        public ParentConfiguration()
        {
            ToTable("Parents");
            //Property(p => p.Name).IsRequired();
            //Property(p => p.Mobile).IsRequired();
            //Property(p => p.HomeNumber).IsRequired();
            //Property(p => p.Email).IsRequired();
        }
    }
}
