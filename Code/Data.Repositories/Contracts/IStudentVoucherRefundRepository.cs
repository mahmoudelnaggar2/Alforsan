using Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.DTO;

namespace Data.Repositories
{
   public interface IStudentVoucherRefundRepository : IRepository<StudentVoucherRefund>
    {
        PagedResult<StudentVoucherRefund> GetAll(FilterModel<StudentVoucherRefund> FilterObject);
    }
}
