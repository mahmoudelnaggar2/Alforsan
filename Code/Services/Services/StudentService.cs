using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Data.Repositories;
using Model;
using Model.DTO;
using Model.Enums;
using Service;

namespace Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository studentRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IParentRepository parentRepository;
        private readonly ISiblingRepository siblingRepository;
        private readonly IEmergencyRepository emergencyRepository;
        private readonly IGradeRepository gradeRepository;
        private readonly IStudentFinancialRepository studentFinancialRepository;
        private readonly IYearRepository yearRepository;        
        private readonly IStudentvoucherService studentvoucherService;

        private readonly IStudentLanguageRepository studentLanguageRepository;

        
        public StudentService(IStudentRepository studentRepository,IUnitOfWork unitOfWork, IParentRepository parentRepository, 
            ISiblingRepository siblingRepository, IEmergencyRepository emergencyRepository,IGradeRepository gradeRepository, 
            IStudentFinancialRepository studentFinancialRepository,IYearRepository yearRepository, IStudentvoucherService studentvoucherService,
             IStudentLanguageRepository studentLanguageRepository)
        {
            this.unitOfWork = unitOfWork;
            this.studentRepository = studentRepository;
            this.parentRepository = parentRepository;
            this.siblingRepository = siblingRepository;
            this.emergencyRepository = emergencyRepository;
            this.gradeRepository = gradeRepository;
            this.studentFinancialRepository = studentFinancialRepository;
            this.yearRepository = yearRepository;            
            this.studentvoucherService = studentvoucherService;
            this.studentLanguageRepository = studentLanguageRepository;
        }

        public bool ApplicationNoExists(string code, int id)
        {
            return studentRepository.ApplicationNoExists(code, id);
        }

        public void CreateStudent(Student student, bool FreeAdmission)
        {
            studentRepository.Add(student);
            SaveStudent();
            if (student.GradeId != null)
            {
                StudentVoucher studentVoucher = PrepareAdmissionFeeVouchre(student);
                if (studentVoucher != null)
                {
                    studentvoucherService.CreateStudentVoucher(studentVoucher, FreeAdmission);
                }
            }
        }

        private StudentVoucher PrepareAdmissionFeeVouchre(Student student)
        {
            StudentVoucher studentVoucher = null;
          //  var CurrentYear = yearRepository.GetCurrentYear().YearId;
          //  var shcoolfee = gradeFeeRepository.GetAdmissionFeeForStudentVoucher(student.StudentId, (int)student.GradeId, CurrentYear, AdmissionFeeEnum.AdmissionFee);
          //  if (shcoolfee != null)
          //  {

          //      studentVoucher = new StudentVoucher()
          //      {
          //          StudentId = student.StudentId,
          //          IsPaid = false,
          //          VoucherDate = DateTime.Now,
          //          YearId = yearRepository.GetCurrentYear().YearId,
          //          StudentVoucherDetails = new List<StudentVoucherDetails>() {  new StudentVoucherDetails
          //              {

          //                  FeesTypeId = 1, //admission Fee ID
          //                  Fee = shcoolfee.Fee

          //              }
          //},
          //          TotalVoucher = shcoolfee.Fee
          //      };
          //  }
            return studentVoucher;
        }

        public void DeleteStudent(Student student)
        {
            studentRepository.Delete(student);
        }

        public string GenerateStudentCode(Student student)
        {
            return studentRepository.GenerateStudentCode(student);
        }

        public List<Student> GetAll()
        {
            return studentRepository.GetAll().ToList();
        }

        public PagedResult<Student> GetAll(FilterModel<Student> FilterObject)
        {

            FilterObject.Includes = new List<string>();
            FilterObject.Includes.Add("School");
            FilterObject.Includes.Add("Grade");
            FilterObject.Includes.Add("Father");
            FilterObject.Includes.Add("Mother");
            FilterObject.Includes.Add("StudentStatus");
            FilterObject.Includes.Add("DiscountsType");
            FilterObject.Includes.Add("JoiningYear");

            
            //choose only checked statuses from status list
            if (FilterObject.SearchObject.StudentStatusDTO != null)
            FilterObject.SearchObject.StudentStatusDTO = FilterObject.SearchObject.StudentStatusDTO.Where(x => x.Checked == true).ToList();
            return studentRepository.GetAll(FilterObject);
        }

        public PagedResult<Student> GetAll(int PageNumber, int PageSize, string SortBy, string SortDirection)
         {
             var list = studentRepository.GetAll(PageNumber, PageSize, new List<string>(), SortBy, SortDirection);
             return list;
         }

        public PagedResult<Student> SearchToGetStudent(FilterModel<Student> FilterObject)
        {
            FilterObject.Includes = new List<string>();
            FilterObject.Includes.Add("School");
            FilterObject.Includes.Add("Grade");
            FilterObject.Includes.Add("StudentStatus");
            return studentRepository.SearchToGetStudent(FilterObject);
        }

        public Student GetStudent(int id)
        {
            return studentRepository.GetById(id);
        }

        public Student GetStudentByCode(string code)
        {
            return studentRepository.GetStudentByCode(code);
        }

        public List<Student> GetStudentBySchoolIdAndGradeId(int schoolId, int gradeId)
        {
            List<Student> studentList= studentRepository.GetStudentBySchoolIdAndGradeId(schoolId, gradeId);
            foreach (var student in studentList)
            {
                student.Balance = studentFinancialRepository.calculateBalance(student.StudentId);
            }
            return studentList;
        }

        public void SaveStudent()
        {
            unitOfWork.Commit();
        }

        public void TransferToStudent(List<Student> students)
        {
            foreach (var student in students)
            {
                Student getStudentById = GetStudent(student.StudentId);
                if (student.Balance==0)
                {
                    getStudentById.StudentStatusId = StudentStatusEnum.Student;
                    getStudentById.StudentCode = GenerateStudentCode(student);
                    SaveStudent();
                }
            }

        }

        public void UpdateStudent(Student student)
        {
            if (student != null)
            {
                //sibling Add/Edit/Delete
                UpdateStudentSiblings(student);

                //Emergency Add/Edit/DElete
                UpdateStudentEmergencies(student);
                
                if (student.FatherId == null && student.Father != null)
                    parentRepository.Add(student.Father);
                else if (student.Father != null)
                    parentRepository.Update(student.Father.ParentId, student.Father);
                if (student.MotherId == null && student.Mother != null)
                    parentRepository.Add(student.Mother);
                else if (student.Mother != null)
                    parentRepository.Update(student.Mother.ParentId, student.Mother);

                //UpdateStudentLanguages
                UpdateStudentLanguages(student);
                 
                student.Grade = null;
                student.School = null;
                student.Siblings = null;
                student.StudentStatus = null;
                student.JoiningYear = null;
                student.StudentLanguages = null;

                studentRepository.Update(student.StudentId, student);
            }
        }

        private void UpdateStudentLanguages(Student student)
        {
            //delete Student Languages
             studentLanguageRepository.Delete(e => e.StudentId == student.StudentId);
            //add new StudentLanguage
            foreach (var studentLang in student.StudentLanguages)
            {
                studentLanguageRepository.Add(studentLang);
            }

        }

        private void UpdateStudentSiblings(Student student)
        {
            List<Sibling> sib = siblingRepository.GetAll().Where(s => s.StudentId == student.StudentId).ToList();
            if (student.Siblings.Count != 0)
            {
                foreach (Sibling sibling in student.Siblings)
                {
                    Sibling x = sib.FirstOrDefault(s => s.SiblingStudentId == sibling.SiblingStudentId);
                    if (x == null)
                    {
                        siblingRepository.Add(sibling);
                    }
                }
            }
            foreach (Sibling siblingRemove in sib)
                {
                    Sibling x = student.Siblings.FirstOrDefault(s => s.SiblingStudentId == siblingRemove.SiblingStudentId&&s.StudentId==siblingRemove.StudentId);
                    if (x == null)
                        siblingRepository.Delete(siblingRemove);
                }
            
        }

        private void UpdateStudentEmergencies(Student student)
        {
            List<Emergency> emg = emergencyRepository.GetAll()
                .Where(e => e.StudentId == student.StudentId)
                .ToList();

            if (student.Emergencies.Count != 0)
            {
                foreach (Emergency emergency in student.Emergencies)
                {
                    Emergency e = emg.FirstOrDefault(x => x.EmergencyId == emergency.EmergencyId);
                    if (e == null)
                    {
                        emergencyRepository.Add(emergency);
                    }
                    else
                    {
                        emergencyRepository.Update(emergency.EmergencyId, emergency);
                    }
                }
            }
            foreach (Emergency emergencyRemove in emg)
                {
                    Emergency e = student.Emergencies.FirstOrDefault(x => x.EmergencyId == emergencyRemove.EmergencyId);
                    if (e == null)
                        emergencyRepository.Delete(emergencyRemove);
                }
            
        }

        public void UpgradeApplicant(Student student)
        {
            student.StudentStatus = null;
            Student getStudentById = GetStudent(student.StudentId);
            getStudentById.StudentStatusId = student.StudentStatusId;
            SaveStudent();
        }

        public List<Student> StudentBySchoolIdAndGradeId(int studentId,int schoolId, int gradeId)
        {
            List<Student> studentList = studentRepository.StudentBySchoolIdAndGradeId(studentId, schoolId, gradeId);
            foreach (var student in studentList)
            {
                student.Balance = studentFinancialRepository.calculateBalance(student.StudentId);
            }
            return studentList;
        }

        public void UpgradeStudent(List<Student> students)
        {
            if (students.Count()!=0)
            {
                Grade grade = gradeRepository.GetOrder(students[0].Grade);
                if (grade != null)
                {
                    foreach (Student student in students)
                    {
                        student.Grade = null;
                        student.School = null;
                        student.GradeId = grade.GradeId;
                        studentRepository.Update(student.StudentId, student);
                    }
                }
            }        
        }

        public bool validationStudentStatus(int studentId, int toStatus)
        {
            Student student= GetStudent(studentId);         
            return studentRepository.validationStudentStatus(student, toStatus);
        }

        public void ActivateStudent(int studentId, bool isActive)
        {
            Student student = GetStudent(studentId);
            student.IsSuspend = isActive;
            SaveStudent();
        }

        public int GetStudentsFromPreviousSchools(int SchoolID)
        {
            int NumberOfStudents = studentRepository.GetStudentsFromPreviousSchools(SchoolID);
            return NumberOfStudents;
        }

        public PagedResult<Student> GetAllStudentsInterView(FilterModel<Student> FilterObject)
        {
            FilterObject.Includes = new List<string>();
            FilterObject.Includes.Add("School");
            FilterObject.Includes.Add("Grade");
            FilterObject.Includes.Add("Father");
            FilterObject.Includes.Add("Mother");
            FilterObject.Includes.Add("StudentStatus");
            FilterObject.Includes.Add("DiscountsType");
            FilterObject.Includes.Add("JoiningYear");
             
         return studentRepository.GetAllStudentsInterView(FilterObject);
        }

        public void UpdateStudentInterViewStatus(Student student)
        {
            Student studentToUpdate = studentRepository.GetById(student.StudentId);
            //update interview status and comment
            studentToUpdate.InterviewStatusId = student.InterviewStatusId;
            studentToUpdate.InterviewComments = student.InterviewComments;
            //update student status to be formCompleted
            studentToUpdate.StudentStatusId = StudentStatusEnum.FormCompleted;

            studentRepository.Update(studentToUpdate.StudentId, studentToUpdate);
          
        }
    }
}
