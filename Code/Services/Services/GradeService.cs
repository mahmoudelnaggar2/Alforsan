using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Data.Repositories;
using Model;
using Model.DTO;

namespace Service
{
    public class GradeService:IGradeService
    {
        private readonly IGradeRepository gradeRepository;
        private readonly IUnitOfWork unitOfWork;

        public GradeService(IGradeRepository gradeRepository, IUnitOfWork unitOfWork)
        {
            this.gradeRepository = gradeRepository;
            this.unitOfWork = unitOfWork;
        }
        public List<Grade> GetAll()
        {
           return gradeRepository.GetAll().ToList();
        }

        public Grade GetGrade(int id)
        {
            return gradeRepository.GetById(id);
        }

        public void CreateGrade(Grade grade)
        {
                var item = gradeRepository.Add(grade);
                SaveGrade();
        }

        public void UpdateGrade(int id,Grade grade)
        {
                gradeRepository.Update(id, grade);
                SaveGrade();
            
        }

        public void DeleteGrade(Grade grade)
        {
            gradeRepository.Delete(grade);
            SaveGrade();
        }

      
        public void SaveGrade()
        {
            unitOfWork.Commit();
        }
    
        
        public PagedResult<Grade> LoadFilteredCategories(FilterModel<Grade> FilterObject)
        {           
            FilterObject.Includes = new List<string>();
            FilterObject.Includes.Add("School");
            var listitem = gradeRepository.GetAll(FilterObject);
            return listitem;
        }
        public bool GradeNameUnique(string gradeName, int schoolId, int gradeId)
        {
            return gradeRepository.GradeNameUnique(gradeName, schoolId, gradeId);
        }

        public bool OrderUnique(int order, int schoolId, int gradeId)
        {
            return gradeRepository.OrderUnique(order, schoolId, gradeId);
        }

        public List<Grade> GradeBySchoolId(int SchoolId)
        {
            return gradeRepository.GradeBySchoolId(SchoolId);
        }
    }
}
