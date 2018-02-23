using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Model;
using Model.DTO;

namespace Data.Repositories
{
    public interface IGradeRepository:IRepository<Grade>
    {
        PagedResult<Grade> GetAll(FilterModel<Grade> FilterObject);
        bool GradeNameUnique(string gradeName, int schoolId, int gradeId);
        bool OrderUnique(int order, int schoolId, int gradeId);
        List<Grade> GradeBySchoolId(int SchoolId);
        Grade GetOrder(Grade grade);
    }
}
