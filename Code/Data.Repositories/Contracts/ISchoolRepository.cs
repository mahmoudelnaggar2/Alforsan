using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Model;

namespace Data.Repositories
{
    public interface ISchoolRepository:IRepository<School>
    {
        bool SchoolNameExists(string name,int id);
        bool SchoolPrefixExists(int prefix ,int id);
    }
}
