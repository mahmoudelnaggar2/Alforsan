using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Model;

namespace Data.Repositories
{
    public class InterviewStatusRepository:BaseRepository<InterviewStatus>,IInterviewStatusRepository
    {
        public InterviewStatusRepository(IDbFactory dbFactory) :base(dbFactory)
        {

        }
    }
}
