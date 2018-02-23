using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Data.Repositories;
using Model;

namespace Services
{
    public class InterviewStatuService : IInterviewStatusService
    {
        private readonly IInterviewStatusRepository interviewStatusRepository;
        private readonly IUnitOfWork unitOfWork;

        public InterviewStatuService(IInterviewStatusRepository interviewStatusRepository,IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.interviewStatusRepository = interviewStatusRepository;
        }
        public List<InterviewStatus> GetAll()
        {
            return interviewStatusRepository.GetAll().OrderBy(s=>s.InterviewStatusName).ToList();
        }

        public void SaveInterviewStatus()
        {
            unitOfWork.Commit();
        }
    }
}
