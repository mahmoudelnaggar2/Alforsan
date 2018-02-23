using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Services
{
    public interface ILanguageService
    {
        List<Language> GetAll();
        void SaveLanguage();
        Language GetLanguage(int id);
        void UpdateLanguage(Language Language);
        void CreateLanguage(Language Language);
        void DeleteLanguage(Language Language);
        
    }
}
