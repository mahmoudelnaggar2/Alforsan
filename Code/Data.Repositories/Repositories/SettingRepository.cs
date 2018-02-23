using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Data.Infrastructure;

namespace Data.Repositories
{
   public class SettingRepository: BaseRepository<Setting>, ISettingRepository
    {
        public SettingRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
