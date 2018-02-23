using Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Data.Repositories
{
    public class VoucherTypeRepository : BaseRepository<VoucherType>, IVoucherTypeRepository
    {
        public VoucherTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
