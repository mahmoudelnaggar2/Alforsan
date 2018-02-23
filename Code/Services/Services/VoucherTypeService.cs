using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Data.Repositories;
using Model;

namespace Service
{
    public class VoucherTypeService : IVoucherTypeService
    {
        private readonly IVoucherTypeRepository voucherTypeRepository;
        private readonly IUnitOfWork unitOfWork;

        public VoucherTypeService(IVoucherTypeRepository voucherTypeRepository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.voucherTypeRepository = voucherTypeRepository;
        }

        public List<VoucherType> GetAll()
        {
            return voucherTypeRepository.GetAll().ToList();
        }

        public VoucherType GetVoucherType(int id)
        {
            return voucherTypeRepository.GetById(id);
        }

        public void CreateVoucherType(VoucherType voucherType)
        {
            voucherTypeRepository.Add(voucherType);
        }

        public void UpdateVoucherType(VoucherType voucherType)
        {
            voucherTypeRepository.Update(voucherType.VoucherTypeId, voucherType);
        }

        public void DeleteVoucherType(VoucherType voucherType)
        {
            voucherTypeRepository.Delete(voucherType);
        }

        public void SaveVoucherType()
        {
            unitOfWork.Commit();
        }
    }
}
