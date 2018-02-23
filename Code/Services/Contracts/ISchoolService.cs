using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Services
{
    public interface ISchoolService
    {
        List<School> GetAll();
        School GetSchool(int id);
        void CreateSchool(School school);
        void UpdateSchool(School school);
        void DeleteSchool(School school);
        void SaveSchool();
        
        //custom
        bool SchoolNameExists(string name,int id);
        bool SchoolPrefixExists(int prefix,int id);
    }
}
