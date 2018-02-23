using Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using Model;

namespace Service
{
    public class StudentFinancialService : IStudentFinancialService
    {
        private readonly IStudentFinancialRepository studentFinancialRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StudentFinancialService(IStudentFinancialRepository studentFinancialRepository, IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this.studentFinancialRepository = studentFinancialRepository;
        }

        public decimal calculateBalance(int studentId)
        {
            return studentFinancialRepository.calculateBalance(studentId);
        }

        public void CreateStudentFinancial(StudentFinancial studentFinancial)
        {
            studentFinancial.PaymentDate=DateTime.Now;
            if (studentFinancial.StudentVoucherId != 0)
            {
                studentFinancialRepository.Add(studentFinancial);
            }      
        }

        public void DeleteStudentFinancial(StudentFinancial studentFinancial)
        {
            studentFinancialRepository.Delete(studentFinancial);
        }

        public List<StudentFinancial> GetAll()
        {
            return studentFinancialRepository.GetAll().ToList();
        }

        public StudentFinancial GetStudentFinancial(int id)
        {
            return studentFinancialRepository.GetById(id);
        }

        public void SaveStudentFinancial()
        {
            _unitOfWork.Commit();
        }

        public void UpdateStudentFinancial(StudentFinancial studentFinancial)
        {
            studentFinancialRepository.Update(studentFinancial.StudentVoucherId, studentFinancial);
        }
    }
}
