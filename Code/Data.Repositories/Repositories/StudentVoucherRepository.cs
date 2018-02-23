using Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.DTO;

namespace Data.Repositories
{
    public class StudentVoucherRepository : BaseRepository<StudentVoucher>, IStudentVoucherRepository
    {
        public StudentVoucherRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public PagedResult<StudentVoucher> GetAll(FilterModel<StudentVoucher> FilterObject)
        {
            Model.DTO.PagedResult<StudentVoucher> studentVoucherList = new Model.DTO.PagedResult<StudentVoucher>();
            Expression<Func<StudentVoucher, bool>> SearchCriteria = a => (

                (a.StudentId == FilterObject.SearchObject.StudentId)
            );
            studentVoucherList = this.GetAll(FilterObject.PageNumber, FilterObject.PageSize, FilterObject.Includes, SearchCriteria, FilterObject.SortBy, FilterObject.SortDirection);
            return studentVoucherList;
        }

        public override StudentVoucher GetById(int id)
        {
            return DbContext.StudentVouchers.Include(a => a.StudentFinancials)
                .Include(a => a.StudentVoucherDetails)
                .Where(a => a.StudentVoucherId == id)
                .FirstOrDefault();
        }
    }
}
