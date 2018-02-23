using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Services
{
    public interface IDiscountsTypeService 
    {
        List<DiscountsType> GetAll();
        DiscountsType GetDiscountsType(int id);
        void CreateDiscountsType(DiscountsType DiscountsType);
        void UpdateDiscountsType(DiscountsType DiscountsType);
        void DeleteDiscountsType(DiscountsType DiscountsType);
        void SaveDiscountsType();
        bool DiscountsNameExists(string name, int id);
    }
}
