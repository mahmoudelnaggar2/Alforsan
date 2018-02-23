using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Service
{
    public interface IStudentVoucherDetailsService
    {
        List<StudentVoucherDetails> GetAll();
        StudentVoucherDetails GetStudentVoucherDetails(int id);
        void CreateStudentVoucherDetails(StudentVoucherDetails studentVoucherDetails);
        void UpdateStudentVoucherDetails(StudentVoucherDetails studentVoucherDetails);
        void DeleteStudentVoucherDetails(StudentVoucherDetails studentVoucherDetails);
        void SaveStudentVoucherDetails();
    }
}
