using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Model;

namespace Data.Repositories
{
     public class CustodyRepository:BaseRepository<Custody>,ICustodyRepository
    {
        public CustodyRepository(IDbFactory dbFactory):base(dbFactory)
        {
                
        }
    }
}
