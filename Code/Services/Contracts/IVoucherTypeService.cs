using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Service
{
    public interface IVoucherTypeService
    {
        List<VoucherType> GetAll();
        VoucherType GetVoucherType(int id);
        void CreateVoucherType(VoucherType voucherType);
        void UpdateVoucherType(VoucherType voucherType);
        void DeleteVoucherType(VoucherType voucherType);
        void SaveVoucherType();
    }
}
