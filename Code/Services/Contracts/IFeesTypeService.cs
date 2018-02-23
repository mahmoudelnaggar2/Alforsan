using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Services
{
    public interface IFeesTypeService
    {
        List<FeesType> GetAll();
        FeesType GetFeesType(int id);
        void CreateFeesType(FeesType feesType);
        void UpdateFeesType(FeesType feesType);
        void DeleteFeesType(FeesType feesType);
        void SaveFeesType();

        //custom
        bool FeesNameExists(string name,int id);

    }
}
