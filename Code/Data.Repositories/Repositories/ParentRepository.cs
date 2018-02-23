using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Model;

namespace Data.Repositories
{
    public class ParentRepository:BaseRepository<Parent>,IParentRepository
    {
        public ParentRepository(IDbFactory dbFactory) :base(dbFactory)
        {

        }
    }
}
