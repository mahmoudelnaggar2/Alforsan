using Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Data.Repositories
{
    public class StudentVoucherDetailsRepository : BaseRepository<StudentVoucherDetails>, IStudentVoucherDetailsRepository
    {
        public StudentVoucherDetailsRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
