using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Model;
using Model.DTO;

namespace Data.Repositories
{
    public class StudentVoucherRefundRepository : BaseRepository<StudentVoucherRefund>, IStudentVoucherRefundRepository
    {
        public StudentVoucherRefundRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public PagedResult<StudentVoucherRefund> GetAll(FilterModel<StudentVoucherRefund> FilterObject)
        {
            Model.DTO.PagedResult<StudentVoucherRefund> studentVoucherRefundList = new Model.DTO.PagedResult<StudentVoucherRefund>();
            Expression<Func<StudentVoucherRefund, bool>> SearchCriteria = a => (

                (a.StudentId == FilterObject.SearchObject.StudentId)
            );
            studentVoucherRefundList = this.GetAll(FilterObject.PageNumber, FilterObject.PageSize, FilterObject.Includes, SearchCriteria, FilterObject.SortBy, FilterObject.SortDirection);
            return studentVoucherRefundList;
        }
    }
}
