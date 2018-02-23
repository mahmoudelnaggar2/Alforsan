using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Model;

namespace Data.Repositories
{
    public interface IYearRepository:IRepository<Year>
    {
        bool YearNameUnique(string yearName, int yearId);
        bool ShortNameUnique(int shortName, int yearId);
        void ActiveOnlyOneYear(int shortName);
        Year GetCurrentYear();
    }
}
