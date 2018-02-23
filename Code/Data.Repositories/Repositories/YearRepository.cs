using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Model;

namespace Data.Repositories
{
    public class YearRepository : BaseRepository<Year>, IYearRepository
    {
        public YearRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public bool YearNameUnique(string yearName, int yearId)
        {
            var item = DbContext.Years.Any(a => a.YearName == yearName && a.YearId != yearId);
            return item;
        }

        public bool ShortNameUnique(int shortName, int yearId)
        {
            return DbContext.Years.Any(a => a.ShortName == shortName && a.YearId != yearId);
        }

        public void ActiveOnlyOneYear(int shortname)
        {
            List<Year> listYear = GetAll().ToList();
            int yearId = DbContext.Years.FirstOrDefault(a => a.ShortName == shortname).YearId;
            foreach (var item in listYear)
            {
                //  item.IsCurrent = yearId == item.YearId;
                if (yearId != item.YearId)
                {
                    item.IsCurrent = false;
                }
                else
                {
                    item.IsCurrent = true;
                }
 
            }
        }

        public Year GetCurrentYear()
        {
            Year y= DbContext.Years.FirstOrDefault(a => a.IsCurrent == true);
            return y;
        }
    }
}
