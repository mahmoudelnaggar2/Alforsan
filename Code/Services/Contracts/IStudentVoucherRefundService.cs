using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.DTO;

namespace Service.Contracts
{
    public interface IStudentVoucherRefundService
    {
        List<StudentVoucherRefund> GetAll();
        StudentVoucherRefund GetStudentVoucherRefund(int id);
        void CreateStudentVoucherRefund(StudentVoucherRefund studentVoucherRefund);
        void UpdateStudentVoucherRefund(int id, StudentVoucherRefund studentVoucherRefund);
        void DeleteStudentVoucherRefund(StudentVoucherRefund studentVoucherRefund);
        void SaveStudentVoucherRefund();

        //cutsom function
        Model.DTO.PagedResult<StudentVoucherRefund> GetAll(FilterModel<StudentVoucherRefund> FilterObject);
    }
}
