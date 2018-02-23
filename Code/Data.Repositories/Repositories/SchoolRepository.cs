using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Model;

namespace Data.Repositories
{
    public class SchoolRepository:BaseRepository<School>,ISchoolRepository
    {
        public SchoolRepository(IDbFactory dbFactory):base(dbFactory)
        {
            
        }

        public bool SchoolNameExists(string name,int id)
        {
            return DbContext.Schools.Any(s => s.SchoolName == name && s.SchoolId!=id);
        }

        public bool SchoolPrefixExists(int prefix,int id)
        {
            return DbContext.Schools.Any(s => s.Prefix == prefix && s.SchoolId!=id);
        }
    }
}
