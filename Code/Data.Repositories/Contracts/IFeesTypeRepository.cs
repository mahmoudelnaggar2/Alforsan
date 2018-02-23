using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Model;

namespace Data.Repositories
{
    public interface IFeesTypeRepository:IRepository<FeesType>
    {
        bool FeesNameExists(string name,int id);
    }
}
