using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Model;

namespace Data.Repositories
{
    public class NationalityRepository:BaseRepository<Nationality>,INationalityRepository
    {
        public NationalityRepository(IDbFactory dbFactory):base(dbFactory)
        {
            
        }

        public bool NationalityNameExists(string name, int id)
        {
            return DbContext.Nationalities.Any(s => s.NationalityName == name && s.NationalityId != id);

        }
    }
}
