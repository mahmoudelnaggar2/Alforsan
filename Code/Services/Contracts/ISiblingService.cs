using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Services
{
    public interface ISiblingService
    {
        List<Sibling> GetAll();
        Sibling GetSibling(int id);
        void CreateSibling(Sibling sibling);
        void UpdateSibling(Sibling sibling);
        void DeleteSibling(Sibling sibling);
        void SaveSibling();
    }
}
