using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Data.Repositories;
using Model;
using Services;

namespace Services
{
   public class SiblingService : ISiblingService
    {
        private readonly ISiblingRepository siblingRepository;
        private readonly IUnitOfWork unitOfWork;

        public SiblingService(ISiblingRepository siblingRepository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.siblingRepository = siblingRepository;
        }
        public void CreateSibling(Sibling sibling)
        {
            siblingRepository.Add(sibling);
        }

        public void DeleteSibling(Sibling sibling)
        {
            siblingRepository.Delete(sibling);
        }

        public List<Sibling> GetAll()
        {
            return siblingRepository.GetAll().ToList();
        }

        public Sibling GetSibling(int id)
        {
            return siblingRepository.GetById(id);
        }

        public void SaveSibling()
        {
            unitOfWork.Commit();
        }

        public void UpdateSibling(Sibling sibling)
        {
           siblingRepository.Update(sibling.SiblingId,sibling);
        }
    }
}
