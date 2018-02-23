using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Services
{
    public interface INationalityService
    {
        List<Nationality> GetAll();
        void SaveNationality();
        Nationality GetNationality(int id);
        void UpdateNationality(Nationality nationality);
        void CreateNationality(Nationality nationality);
        void DeleteNationality(Nationality nationality);
        bool NationalityNameExists(string name, int id);
    }
}
