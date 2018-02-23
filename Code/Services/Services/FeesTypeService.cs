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
   public class FeesTypeService : IFeesTypeService
    {
        private readonly IFeesTypeRepository feesTypeRepository;
        private readonly IUnitOfWork unitOfWork;

        public FeesTypeService(IUnitOfWork unitOfWork,IFeesTypeRepository feesTypeRepository)
        {
            this.feesTypeRepository = feesTypeRepository;
            this.unitOfWork = unitOfWork;
        }

        public List<FeesType> GetAll()
        {
            List<FeesType> feesTypes = feesTypeRepository.GetAll().ToList();
            return feesTypes;
        }

        public FeesType GetFeesType(int id)
        {
            FeesType feesType = feesTypeRepository.GetById(id);
            return feesType;
        }

        public void CreateFeesType(FeesType feesType)
        {
            feesTypeRepository.Add(feesType);
        }

        public void DeleteFeesType(FeesType feesType)
        {
            feesTypeRepository.Delete(feesType);
        }

        public void SaveFeesType()
        {
           unitOfWork.Commit();
        }

        public void UpdateFeesType(FeesType feesType)
        {
            feesTypeRepository.Update(feesType.FeesTypeId,feesType);
        }

        public bool FeesNameExists(string name,int id)
        {
            return feesTypeRepository.FeesNameExists(name,id);
        }
    }
}
