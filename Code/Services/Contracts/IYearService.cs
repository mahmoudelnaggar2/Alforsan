using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Service
{
    public interface IYearService
    {
        List<Year> GetAll();
        Year GetYear(int id);
        void CreateYear(Year year);
        void UpdateYear(int id, Year year);
        void DeleteYear(Year year);
        bool YearNameUnique(string yearName, int yearId);
        bool ShortNameUnique(int shortName, int yearId);
        void ActiveOnlyOneYear(int shortName);
        Year GetCurrentYear();
        void SaveYear();
    }
}
