using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Model;

namespace Data.Repositories
{
    public class ReligionRepository:BaseRepository<Religion>,IReligionRepository
    {
        public ReligionRepository(IDbFactory dbFactory) :base(dbFactory)
        {

        }
    }
}
