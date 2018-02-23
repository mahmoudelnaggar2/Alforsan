using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using Data.Infrastructure;
using Service.Contracts;
using Model;
using Model.Enums;
using Model.DTO;

namespace Service
{
    public class StudentVoucherRefundService:IStudentVoucherRefundService
    {
        private readonly IStudentVoucherRefundRepository studentVoucherRefundRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IStudentVoucherRepository studentVoucherRepository;
        private readonly IStudentFinancialRepository studentFinancialRepository;
        public StudentVoucherRefundService(IStudentVoucherRefundRepository studentVoucherRefundRepository, IUnitOfWork unitOfWork, IStudentVoucherRepository studentVoucherRepository, IStudentFinancialRepository studentFinancialRepository)
        {
            this.studentVoucherRefundRepository = studentVoucherRefundRepository;
            this.unitOfWork = unitOfWork;
            this.studentVoucherRepository = studentVoucherRepository;
            this.studentFinancialRepository = studentFinancialRepository;
        }

        public void CreateStudentVoucherRefund(StudentVoucherRefund studentVoucherRefund)
        {
            try
            { 
                studentVoucherRefund.IsPaid = false;
                studentVoucherRefundRepository.Add(studentVoucherRefund);
                SaveStudentVoucherRefund();
                UpdateTotalVoucherRefund(studentVoucherRefund.StudentVoucherId, studentVoucherRefund.TotalRefund);
                StudentFinancial studentFinancial = new StudentFinancial()
                {
                    StudentId = studentVoucherRefund.StudentId,
                    StudentVoucherId = studentVoucherRefund.StudentVoucherId,
                    Amount = studentVoucherRefund.TotalRefund,
                    PaymentDate = DateTime.Now,
                    VoucherTypeId = VoucherTypeEnum.RefundRequest
                };
                studentFinancialRepository.Add(studentFinancial);
                SaveStudentVoucherRefund();
            }
            catch (Exception e)
            {
                
            }
        
        }

        public void DeleteStudentVoucherRefund(StudentVoucherRefund studentVoucherRefund)
        {
            studentVoucherRefundRepository.Delete(studentVoucherRefund);
        }

        public List<StudentVoucherRefund> GetAll()
        {
            return studentVoucherRefundRepository.GetAll().ToList();
        }

        public StudentVoucherRefund GetStudentVoucherRefund(int id)
        {
            return studentVoucherRefundRepository.GetById(id);
        }

        public void SaveStudentVoucherRefund()
        {
            unitOfWork.Commit();
        }

        public void UpdateStudentVoucherRefund(int id, StudentVoucherRefund studentVoucherRefund)
        {
            try
            {
                unitOfWork.BeginTransaction();
                StudentVoucherRefund studentRefund = GetStudentVoucherRefund(id);
                if (!studentRefund.IsPaid)
                {
                    studentRefund.IsPaid = true;
                    studentRefund.PaymentDate = studentVoucherRefund.PaymentDate;
                    SaveStudentVoucherRefund();
                    StudentFinancial deliveryRefundFinancial = new StudentFinancial()
                    {
                        StudentId = studentRefund.StudentId,
                        StudentVoucherId = studentRefund.StudentVoucherId,
                        PaymentDate = DateTime.Now,
                        Amount = -1 * studentRefund.TotalRefund,
                        VoucherTypeId = VoucherTypeEnum.RefundDelivery
                    };
                    studentFinancialRepository.Add(deliveryRefundFinancial);
                    SaveStudentVoucherRefund();
                }
                unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        private void UpdateTotalVoucherRefund(int studentVoucherId, decimal totalVoucherRefund)
        {
            StudentVoucher studentVoucher = studentVoucherRepository.GetById(studentVoucherId);
            studentVoucher.TotalVoucherRefund = totalVoucherRefund;
            SaveStudentVoucherRefund();
        }

        public PagedResult<StudentVoucherRefund> GetAll(FilterModel<StudentVoucherRefund> FilterObject)
        {
            FilterObject.Includes=new List<string>();
            return studentVoucherRefundRepository.GetAll(FilterObject);
        }
    }
}
