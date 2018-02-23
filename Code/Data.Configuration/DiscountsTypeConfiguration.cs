using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Data.Configuration
{
    public class DiscountsTypeConfiguration :EntityTypeConfiguration<DiscountsType>
    {
        public DiscountsTypeConfiguration()
        {
            ToTable("DiscountsTypes");
            Property(d => d.DiscountName).IsRequired().IsMaxLength(); 
        }
    }
}
