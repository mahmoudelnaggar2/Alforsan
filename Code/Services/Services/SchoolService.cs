using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Data.Repositories;
using Model;

namespace Services
{
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository schoolRepository;
        private readonly IUnitOfWork unitOfWork;

        public SchoolService(IUnitOfWork unitOfWork, ISchoolRepository schoolRepository)
        {
            this.schoolRepository = schoolRepository;
            this.unitOfWork = unitOfWork;
        }

        public List<School> GetAll()
        {
            List<School> schools = schoolRepository.GetAll().ToList();
            return schools;
        }

        public School GetSchool(int id)
        {
            School school = schoolRepository.GetById(id);
            return school;
        }
        
        public void CreateSchool(School school)
        {
            schoolRepository.Add(school);
        }

        public void DeleteSchool(School school)
        {
            schoolRepository.Delete(school);
        }

        public void UpdateSchool(School school)
        {
            schoolRepository.Update(school.SchoolId,school);
        }

        public void SaveSchool()
        {
            unitOfWork.Commit();
        }

        public bool SchoolNameExists(string name,int id)
        {
            return schoolRepository.SchoolNameExists(name,id);
        }

        public bool SchoolPrefixExists(int prefix,int id)
        {
            return schoolRepository.SchoolPrefixExists(prefix,id);
        }
    }
}
