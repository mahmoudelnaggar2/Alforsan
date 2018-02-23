using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Model;
using Model.DTO;

namespace Data.Repositories
{
    public class GradeRepository:BaseRepository<Grade>,IGradeRepository
    {
     
        
        public GradeRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {      
        }

        public override Grade GetById(int id)
        {
            return DbContext.Grades.Include(a => a.School).FirstOrDefault(a => a.GradeId == id);
        }

        public override IEnumerable<Grade> GetAll()
        {
            return DbContext.Grades.Include(a => a.School);
        }

        public PagedResult<Grade> GetAll(FilterModel<Grade> FilterObject)
        {
            PagedResult<Grade> GradeList = new PagedResult<Grade>();
            Expression<Func<Grade, bool>> SearchCriteria = a => (

                (a.GradeId == FilterObject.SearchObject.GradeId || FilterObject.SearchObject.GradeId == 0)
                && (a.School.SchoolName.Contains(FilterObject.SearchObject.GradeName) || string.IsNullOrEmpty(FilterObject.SearchObject.GradeName))
                && (a.SchoolId == FilterObject.SearchObject.SchoolId || FilterObject.SearchObject.SchoolId == 0)
            );
            GradeList = this.GetAll(FilterObject.PageNumber, FilterObject.PageSize, FilterObject.Includes, SearchCriteria, FilterObject.SortBy, FilterObject.SortDirection);      
            var list = GradeList;
            return GradeList;
        }

        public bool GradeNameUnique(string gradeName, int schoolId, int gradeId)
        {
            bool check = DbContext.Grades.Any(a => a.GradeName == gradeName && a.SchoolId == schoolId &&
                                                   a.GradeId != gradeId);
            return check;
        }
        public bool OrderUnique(int order, int schoolId, int gradeId)
        {
            bool check = DbContext.Grades.Any(a => a.Order == order && a.SchoolId == schoolId &&
                                                   a.GradeId != gradeId);
            return check;
        }

        public List<Grade> GradeBySchoolId(int SchoolId)
        {
            return DbContext.Grades.Include(a => a.School)
                .Where(a => a.SchoolId == SchoolId)
                .ToList();      
        }

        public Grade GetOrder(Grade grade)
        {
            return DbContext.Grades.FirstOrDefault(g => g.SchoolId == grade.SchoolId && g.Order > grade.Order);
        }
    }
}
