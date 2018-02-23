using Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using  Model.Enums;

namespace Data.Repositories
{
    public class StudentFinancialRepository : BaseRepository<StudentFinancial>, IStudentFinancialRepository
    {
        public StudentFinancialRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public decimal calculateBalance(int studentId)
        {
            var studentBalance = DbContext.StudentFinancials
                .Where(a => a.StudentId == studentId)?
                .Sum(a => (decimal?)a.Amount)??0;

            return studentBalance;
        }
    }
}
