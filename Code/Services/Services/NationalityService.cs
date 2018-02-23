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
    public class NationalityService:INationalityService
    {
        private readonly INationalityRepository nationalityRepository;
        private readonly IUnitOfWork unitOfWork;

        public NationalityService(IUnitOfWork unitOfWork, INationalityRepository nationalityRepository)
        {
        this.nationalityRepository = nationalityRepository;
        this.unitOfWork = unitOfWork;
        }

        public Nationality GetNationality(int id)
        {
            Nationality Nationality = nationalityRepository.GetById(id);
            return Nationality;
        }

        public void CreateNationality(Nationality Nationality)
        {
            nationalityRepository.Add(Nationality);
        }

        public void DeleteNationality(Nationality Nationality)
        {
            nationalityRepository.Delete(Nationality);
        }

        public void UpdateNationality(Nationality Nationality)
        {
            nationalityRepository.Update(Nationality.NationalityId, Nationality);
        }

        public void SaveNationality()
        {
            unitOfWork.Commit();
        }

        public bool NationalityNameExists(string name, int id)
        {
            return nationalityRepository.NationalityNameExists(name, id);
        }
         
        public List<Nationality> GetAll()
        {
            return nationalityRepository.GetAll().ToList();
        }
    }
}
