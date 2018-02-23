using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using Model;
using Model.Enums;
using Data.Infrastructure;
using Model.DTO;

namespace Service
{
    public class StudentVoucherService : IStudentvoucherService
    {
        private readonly IStudentVoucherRepository studentVoucherRepository;
        private readonly IStudentVoucherDetailsRepository studentVoucherDetails;
        private readonly IStudentFinancialRepository studentFinancialRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IStudentVoucherDetailsRepository studentVoucherDetailRepository;
        private readonly IYearRepository yearRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly Helpers.SecurityHelper _security;
        public StudentVoucherService(IStudentVoucherRepository studentVoucherRepository, IStudentVoucherDetailsRepository studentVoucherDetails, IStudentFinancialRepository studentFinancialRepository, IStudentRepository studentRepository,IStudentVoucherDetailsRepository studentVoucherDetailRepository, IUnitOfWork unitOfWork, IYearRepository yearRepository)
        {
            this.unitOfWork = unitOfWork;
            this.studentVoucherRepository = studentVoucherRepository;
            this.studentVoucherDetails = studentVoucherDetails;
            this.studentFinancialRepository = studentFinancialRepository;
            this.studentRepository = studentRepository;
            this.studentVoucherDetailRepository = studentVoucherDetailRepository;
            this.yearRepository = yearRepository;
            _security = new Helpers.SecurityHelper();
        }


        public void CreateStudentVoucher(StudentVoucher studentVoucher, bool FreeAdmission)
        {
            try
            {
                unitOfWork.BeginTransaction();
                studentVoucher.VoucherDate = DateTime.Now;
                studentVoucher.PaymentDate =null;
                studentVoucher.YearId = studentVoucher.YearId==0? yearRepository.GetCurrentYear().YearId: studentVoucher.YearId;
                studentVoucher.IsPaid = false;
                if (studentVoucher.TotalVoucher > 0)
                {

                    studentVoucherRepository.Add(studentVoucher);
                    SaveStudentVoucher();

                    StudentFinancial studentFinancial = new StudentFinancial()
                    {
                        StudentId = studentVoucher.StudentId,
                        StudentVoucherId = studentVoucher.StudentVoucherId,
                        Amount = -1 * studentVoucher.TotalVoucher,
                        VoucherTypeId = VoucherTypeEnum.Issue,
                        PaymentDate = DateTime.Now
                    };
                    studentFinancialRepository.Add(studentFinancial);
                    SaveStudentVoucher();
                    if (FreeAdmission == true)
                    {
                        UpdatePaidVoucher(studentVoucher.StudentVoucherId, studentVoucher);
                      
                    }
                }
                unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                unitOfWork.RollBackTransaction();
            }
        }

        public StudentFinancial AddStudentFinancial(StudentVoucher studentVoucher)
        {
            StudentFinancial studentFinancial = new StudentFinancial()
            {
                StudentId = studentVoucher.StudentId,
                StudentVoucherId = studentVoucher.StudentVoucherId,
                Amount = -1 * studentVoucher.TotalVoucher,
                VoucherTypeId = VoucherTypeEnum.Issue,
                PaymentDate = DateTime.Now
            };
            studentFinancialRepository.Add(studentFinancial);
            return studentFinancial;
        }
        public StudentVoucherDetails AddStudentVoucherDetails( StudentVoucher studentVoucher)
        {
                StudentVoucherDetails StudentVoucherDetails = new StudentVoucherDetails
                {
                    StudentVoucherId = studentVoucher.StudentVoucherId,
                    FeesTypeId = Model.Enums.AdmissionFeeEnum.AdmissionFee,
                    Fee = studentVoucher.TotalVoucher

                };
                studentVoucherDetailRepository.Add(StudentVoucherDetails);
            return StudentVoucherDetails;
        }

        public void DeleteStudentVoucher(StudentVoucher studentVoucher)
        {
            try
            {
                unitOfWork.BeginTransaction();
                if (!studentVoucher.IsPaid)
                {

                    StudentVoucher DeleteStudentVoucher =
                        studentVoucherRepository.GetById(studentVoucher.StudentVoucherId);
                    foreach (var finacialItem in DeleteStudentVoucher.StudentFinancials.ToList())
                    {
                        if (finacialItem.VoucherTypeId == Model.Enums.VoucherTypeEnum.Issue)
                        {
                            studentFinancialRepository.Delete(finacialItem);
                            SaveStudentVoucher();
                        }
                    }
                    foreach (var detailsItem in DeleteStudentVoucher.StudentVoucherDetails.ToList())
                    {
                        studentVoucherDetails.Delete(detailsItem);
                        SaveStudentVoucher();
                    }
                    studentVoucherRepository.Delete(studentVoucher);
                    SaveStudentVoucher();
                }
                unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                unitOfWork.RollBackTransaction();
            }
 

        }

        public List<StudentVoucher> GetAll()
        {
            return studentVoucherRepository.GetAll().ToList();
        }

        public StudentVoucher GetStudentVoucher(int id)
        {
            return studentVoucherRepository.GetById(id);
        }

        public void SaveStudentVoucher()
        {
            unitOfWork.Commit();
        }

        public void UpdateStudentVoucher(int id, StudentVoucher studentVoucher)
        {
            studentVoucherRepository.Update(id, studentVoucher);
        }

        public void UpdatePaidVoucher(int id, StudentVoucher studentVoucher)
        {
           StudentVoucher toUpdateVoucher= GetStudentVoucher(id);
            if (!toUpdateVoucher.IsPaid)
            {
                toUpdateVoucher.Notes = studentVoucher.Notes;
                toUpdateVoucher.PaymentDate = studentVoucher.PaymentDate;
                toUpdateVoucher.IsPaid = true;
                SaveStudentVoucher();
                StudentFinancial paymentStudentFinancail = new StudentFinancial();
                paymentStudentFinancail.StudentId = toUpdateVoucher.StudentId;
                paymentStudentFinancail.StudentVoucherId = toUpdateVoucher.StudentVoucherId;
                paymentStudentFinancail.Amount = toUpdateVoucher.TotalVoucher;
                paymentStudentFinancail.PaymentDate = DateTime.Now;
                paymentStudentFinancail.Notes = toUpdateVoucher.Notes;
                paymentStudentFinancail.VoucherTypeId = VoucherTypeEnum.Payment;
                studentFinancialRepository.Add(paymentStudentFinancail);
                SaveStudentVoucher();
            }    
        }

        public PagedResult<StudentVoucher> GetAll(FilterModel<StudentVoucher> FilterObject)
        {
            FilterObject.Includes = new List<string>();
            FilterObject.Includes.Add("Year");
            return studentVoucherRepository.GetAll(FilterObject);
        }
    }
}
