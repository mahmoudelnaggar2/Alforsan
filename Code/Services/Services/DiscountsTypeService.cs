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
    public class DiscountsTypeService : IDiscountsTypeService
    {
        private readonly IDiscountsTypeRepository DiscountsTypeRepository;
        private readonly IUnitOfWork unitOfWork;

        public DiscountsTypeService(IUnitOfWork unitOfWork, IDiscountsTypeRepository DiscountsTypeRepository)
        {
            this.DiscountsTypeRepository = DiscountsTypeRepository;
            this.unitOfWork = unitOfWork;
        }

        public List<DiscountsType> GetAll()
        {
            List<DiscountsType> DiscountsTypes = DiscountsTypeRepository.GetAll().ToList();
            return DiscountsTypes;
        }

        public DiscountsType GetDiscountsType(int id)
        {
            DiscountsType DiscountsType = DiscountsTypeRepository.GetById(id);
            return DiscountsType;
        }

        public void CreateDiscountsType(DiscountsType DiscountsType)
        {
            DiscountsTypeRepository.Add(DiscountsType);
        }

        public void DeleteDiscountsType(DiscountsType DiscountsType)
        {
            DiscountsTypeRepository.Delete(DiscountsType);
        }

        public void SaveDiscountsType()
        {
            unitOfWork.Commit();
        }

        public void UpdateDiscountsType(DiscountsType DiscountsType)
        {
            DiscountsTypeRepository.Update(DiscountsType.DiscountsTypeID, DiscountsType);
        }

        public bool DiscountsNameExists(string name, int id)
        {
            return DiscountsTypeRepository.DiscountsNameExists(name, id);
        }
    }
}
