using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.DTO;

namespace Service
{
    public interface IStudentvoucherService
    {
        List<StudentVoucher> GetAll();
        StudentVoucher GetStudentVoucher(int id);
        void CreateStudentVoucher(StudentVoucher studentVoucher, bool FreeAdmission);
        void UpdateStudentVoucher(int id, StudentVoucher studentVoucher);
        void DeleteStudentVoucher(StudentVoucher studentVoucher);
        void SaveStudentVoucher();

        //cutsom function
        Model.DTO.PagedResult<StudentVoucher> GetAll(FilterModel<StudentVoucher> FilterObject);
        void UpdatePaidVoucher(int id, StudentVoucher studentVoucher);
    }
}
