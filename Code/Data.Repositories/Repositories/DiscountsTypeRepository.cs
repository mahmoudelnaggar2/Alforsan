using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Model;

namespace Data.Repositories
{
    public class DiscountsTypeRepository : BaseRepository<DiscountsType>, IDiscountsTypeRepository
    {
        public DiscountsTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
        public bool DiscountsNameExists(string name, int id)
        {
            return DbContext.DiscountsTypes.Any(d => d.DiscountName == name && d.DiscountsTypeID != id);
        }
    }
}
