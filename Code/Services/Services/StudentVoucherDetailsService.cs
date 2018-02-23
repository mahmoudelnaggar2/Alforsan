using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using Data.Infrastructure;
using Model;

namespace Service
{
    public class StudentVoucherDetailsService : IStudentVoucherDetailsService
    {
        private readonly IStudentVoucherDetailsRepository studentVoucherDetailsRepository;
        private readonly IUnitOfWork unitOfWork;

        public StudentVoucherDetailsService(IStudentVoucherDetailsRepository studentVoucherDetailsRepository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.studentVoucherDetailsRepository = studentVoucherDetailsRepository;
        }

        public List<StudentVoucherDetails> GetAll()
        {
            return studentVoucherDetailsRepository.GetAll().ToList();
        }

        public StudentVoucherDetails GetStudentVoucherDetails(int id)
        {
            return studentVoucherDetailsRepository.GetById(id);
        }

        public void CreateStudentVoucherDetails(StudentVoucherDetails studentVoucherDetails)
        {
            studentVoucherDetailsRepository.Add(studentVoucherDetails);
            SaveStudentVoucherDetails();
        }

        public void UpdateStudentVoucherDetails(StudentVoucherDetails studentVoucherDetails)
        {
            studentVoucherDetailsRepository.Update(studentVoucherDetails.StudentVoucherDetailsId, studentVoucherDetails);
            SaveStudentVoucherDetails();
        }

        public void DeleteStudentVoucherDetails(StudentVoucherDetails studentVoucherDetails)
        {
            studentVoucherDetailsRepository.Delete(studentVoucherDetails);
            SaveStudentVoucherDetails();
        }

        public void SaveStudentVoucherDetails()
        {
            unitOfWork.Commit();
        }
    }
}
