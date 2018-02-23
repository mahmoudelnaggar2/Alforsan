using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.DTO;

namespace Service
{
    public interface IGradeService
    {
        List<Grade> GetAll();
        Grade GetGrade(int id);
        void CreateGrade(Grade grade);
        void UpdateGrade(int id,Grade grade);
        void DeleteGrade(Grade grade);
        void SaveGrade();
        bool OrderUnique(int order, int schoolId, int gradeId);
        bool GradeNameUnique(string gradeName, int schoolId, int gradeId);
        PagedResult<Grade> LoadFilteredCategories(FilterModel<Grade> FilterObject);
        List<Grade> GradeBySchoolId(int SchoolId);
    }
}
