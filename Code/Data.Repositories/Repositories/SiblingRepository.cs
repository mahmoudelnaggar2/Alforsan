using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Model;

namespace Data.Repositories
{
    public class SiblingRepository:BaseRepository<Sibling>,ISiblingRepository
    {
        public SiblingRepository(IDbFactory dbFactory) :base(dbFactory)
        {

        }
    }
}
