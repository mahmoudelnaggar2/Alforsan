using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using Model;
using Data.Infrastructure;

namespace Services
{
    public class LanguageService:ILanguageService
    {
        private readonly ILanguageRepository LanguageRepository;
        private readonly IUnitOfWork unitOfWork;
        
        public LanguageService(IUnitOfWork unitOfWork, ILanguageRepository LanguageRepository)
        {
        this.LanguageRepository = LanguageRepository;
        this.unitOfWork = unitOfWork;
        }

        public Language GetLanguage(int id)
        {
            Language Language = LanguageRepository.GetById(id);
            return Language;
        }

        public void CreateLanguage(Language Language)
        {
            LanguageRepository.Add(Language);
        }

        public void DeleteLanguage(Language Language)
        {
            LanguageRepository.Delete(Language);
        }

        public void UpdateLanguage(Language Language)
        {
            LanguageRepository.Update(Language.LanguageId, Language);
        }

        public void SaveLanguage()
        {
            unitOfWork.Commit();
        }
        public List<Language> GetAll()
        {
            return LanguageRepository.GetAll().ToList();
        }
    }
}
