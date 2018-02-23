using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Model;

namespace Data.Repositories
{
    public class FeesTypeRepository : BaseRepository<FeesType>, IFeesTypeRepository
    {
        public FeesTypeRepository(IDbFactory dbFactory): base(dbFactory)
        {
            
        }
        public bool FeesNameExists(string name,int id)
        {
            return DbContext.FeesTypes.Any(f => f.FeeName == name &&f.FeesTypeId!=id);
        }
    }
}
