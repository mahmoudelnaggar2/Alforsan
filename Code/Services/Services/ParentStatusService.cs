using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using Model;

namespace Services
{
    public class ParentStatusService : IParentStatusService
    {
        private readonly IParentStatusRepository parentStatusRepository;

        public ParentStatusService(IParentStatusRepository parentStatusRepository)
        {
            this.parentStatusRepository = parentStatusRepository;
        }

        public List<ParentStatus> GetAll()
        {
            return parentStatusRepository.GetAll().ToList();
        }
    }
}
