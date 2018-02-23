using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Service
{
    public interface IStudentFinancialService
    {
        List<StudentFinancial> GetAll();
        StudentFinancial GetStudentFinancial(int id);
        void CreateStudentFinancial(StudentFinancial studentFinancial);
        void UpdateStudentFinancial(StudentFinancial studentFinancial);
        void DeleteStudentFinancial(StudentFinancial studentFinancial);
        void SaveStudentFinancial();

        decimal calculateBalance(int studentId);
    }
}
