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
    public class ParentService : IParentService
    {
        private readonly IParentRepository parentRepository;
        private readonly IUnitOfWork unitOfWork;

        public ParentService(IParentRepository parentRepository,IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.parentRepository = parentRepository;
        }
        public void CreateParent(Parent parent)
        {
            parentRepository.Add(parent);
        }

        public void DeleteParent(Parent parent)
        {
            parentRepository.Delete(parent);
        }

        public List<Parent> GetAll()
        {
           return parentRepository.GetAll().ToList();
        }

        public Parent GetParent(int id)
        {
            return parentRepository.GetById(id);
        }

        public void SaveParent()
        {
            unitOfWork.Commit();
        }

        public void UpdateParent(Parent parent)
        {
            parentRepository.Update(parent.ParentId,parent);
        }
    }
}
