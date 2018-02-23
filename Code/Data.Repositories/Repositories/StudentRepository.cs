using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Model;
using Model.DTO;
using Model.Enums;

namespace Data.Repositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        private StudentFinancialRepository student_financial;

        public Student GetStudentByCode(string code)
        {
            return DbContext.Students.Include(d => d.JoiningYear).Include(x=>x.Grade).Include(x=>x.School).Include(x=>x.StudentStatus).FirstOrDefault(s => s.StudentCode == code);
        }
        public string GenerateStudentCode(Student student)
        {
            int studentOrder =DbContext.Students.Where(a => a.SchoolId == student.SchoolId 
            && a.StudentStatusId == StudentStatusEnum.Student
            && a.ApplicationDate.Year == student.ApplicationDate.Year
            ).ToList().Count();
            int currentYear = DbContext.Years.FirstOrDefault(a => a.IsCurrent == true).ShortName;
            int schoolPrefix = student.School.Prefix;
            string studentCode = currentYear.ToString() + schoolPrefix.ToString() +(studentOrder+1).ToString("D3");
            return studentCode;
        }

        public bool ApplicationNoExists(string code, int id)
        {
            return DbContext.Students.Any(s => s.ApplicationNo == code && s.StudentId != id);
        }

        public PagedResult<Student> GetAll(FilterModel<Student> FilterObject)
        {
            Model.DTO.PagedResult<Student> studentList = new Model.DTO.PagedResult<Student>();
            Expression<Func<Student, bool>> SearchCriteria = a => (
                (a.ApplicationNo == FilterObject.SearchObject.ApplicationNo ||
                 string.IsNullOrEmpty(FilterObject.SearchObject.ApplicationNo))
                && (a.SchoolId == FilterObject.SearchObject.SchoolId || FilterObject.SearchObject.SchoolId == 0)
                && (a.GradeId == FilterObject.SearchObject.GradeId || FilterObject.SearchObject.GradeId == 0 || FilterObject.SearchObject.GradeId==null)
           
                && (a.FirstName.Contains(FilterObject.SearchObject.FirstName)||string.IsNullOrEmpty(FilterObject.SearchObject.FirstName)|| a.MiddleName.Contains(FilterObject.SearchObject.FirstName)|| a.FamilyName.Contains(FilterObject.SearchObject.FirstName))             
                && (FilterObject.SearchObject.DateTimeFrom == null || DbFunctions.TruncateTime(a.ApplicationDate) >=
                    DbFunctions.TruncateTime(FilterObject.SearchObject.DateTimeFrom)) &&
                (FilterObject.SearchObject.DateTimeTo == null || DbFunctions.TruncateTime(a.ApplicationDate) <=
                 DbFunctions.TruncateTime(FilterObject.SearchObject.DateTimeTo))

                 &&
                 (( FilterObject.SearchObject.StudentStatusId==null &&a.StudentStatusId!=StudentStatusEnum.Student) || FilterObject.SearchObject.StudentStatusId == a.StudentStatusId)
                 &&
                 (!FilterObject.SearchObject.CheckedInterviewStatus.Any() || (a.InterviewStatusId!=null&& FilterObject.SearchObject.CheckedInterviewStatus.Any(s=>s==a.InterviewStatusId.Value)) )
            );

            studentList = this.GetAll(FilterObject.PageNumber, FilterObject.PageSize, FilterObject.Includes, SearchCriteria, FilterObject.SortBy, FilterObject.SortDirection);

            //filter list by Status
            if (FilterObject.SearchObject.StudentStatusDTO != null)
            {
                if(FilterObject.SearchObject.StudentStatusDTO.Count != 0)
                {
                    var FinalResult = studentList.Results.Where(s => FilterObject.SearchObject.StudentStatusDTO.Any(r => r.StudentStatusId.Equals(s.StudentStatus.StudentStatusId))).ToList();

                    studentList.Results = FinalResult;
                }
             
            }
            //if(FilterObject.SearchObject.StudentStatusId != null)
            //{
            //    var FinalResult = studentList.Results.Where(x => x.StudentStatusId == StudentStatusEnum.Student).ToList();
            //    studentList.Results = FinalResult;
            //}

            return studentList;
        }

        public List<Student> GetStudentBySchoolIdAndGradeId(int schoolId, int gradeId)
        {
           var items= DbContext.Students.Include(a=>a.School).Include(a=>a.Grade).Include(d => d.JoiningYear)
                .Where(a => a.SchoolId == schoolId && a.GradeId == gradeId && a.StudentStatusId==StudentStatusEnum.FormCompleted &&  a.InterviewStatusId==InterviewStatusEnum.Accepted)
                            .ToList();
            return items;
        }

        public override Student GetById(int id)
        {
            return DbContext.Students.Include(s => s.StudentLanguages).Include(s=>s.Gender).Include(s=>s.Grade).Include(d=>d.JoiningYear).Include(s => s.Siblings).Include(a => a.DiscountsType).Include(s=>s.Father).Include(s=>s.Mother).Include(s=>s.Emergencies).Include(s=>s.Grade).Include(s=>s.School).FirstOrDefault(s => s.StudentId == id);
        }

        public List<Student> StudentBySchoolIdAndGradeId(int studentId,int schoolId, int gradeId)
        {
            return DbContext.Students.Include(s=>s.Grade).Include(s=>s.School).Include(d => d.JoiningYear)
                .Where(s =>s.StudentId!=studentId && s.SchoolId == schoolId && s.GradeId == gradeId && s.StudentStatusId == StudentStatusEnum.Student)
                 .ToList();
        }

        public PagedResult<Student> SearchToGetStudent(FilterModel<Student> FilterObject)
        {


            Model.DTO.PagedResult<Student> studentList = new Model.DTO.PagedResult<Student>();
            Expression<Func<Student, bool>> SearchCriteria;

            if (FilterObject.SearchObject.FirstName == null)
            {
                var searchByApplication = DbContext.Students.Where(x => x.ApplicationNo == FilterObject.SearchObject.ApplicationNo);
                var searchByCode = DbContext.Students.Where(x => x.StudentCode == FilterObject.SearchObject.StudentCode);
                if (searchByApplication.Count() == 0)
                    FilterObject.SearchObject.ApplicationNo = null;
                else if (searchByCode.Count() == 0)
                    FilterObject.SearchObject.StudentCode = null;

            }

            if (FilterObject.SearchObject.StudentStatusId == StudentStatusEnum.Student)
            {
                SearchCriteria = a => (
                   (a.SchoolId == FilterObject.SearchObject.SchoolId || FilterObject.SearchObject.SchoolId == 0)
                   && (a.GradeId == FilterObject.SearchObject.GradeId || FilterObject.SearchObject.GradeId == 0 || FilterObject.SearchObject.GradeId == null)
                   && (a.StudentStatusId == StudentStatusEnum.Student)
                   && (a.FirstName.Contains(FilterObject.SearchObject.FirstName) || a.MiddleName.Contains(FilterObject.SearchObject.FirstName) || a.FirstName.Contains(FilterObject.SearchObject.FirstName) || string.IsNullOrEmpty(FilterObject.SearchObject.FirstName))
                   && (a.StudentCode == FilterObject.SearchObject.StudentCode || string.IsNullOrEmpty(FilterObject.SearchObject.StudentCode))
                   && (a.ApplicationNo == FilterObject.SearchObject.ApplicationNo || string.IsNullOrEmpty(FilterObject.SearchObject.ApplicationNo))
               );
            }
            else
            {
                SearchCriteria = a => (
                       (a.SchoolId == FilterObject.SearchObject.SchoolId || FilterObject.SearchObject.SchoolId == 0)
                       && (a.GradeId == FilterObject.SearchObject.GradeId || FilterObject.SearchObject.GradeId == 0 || FilterObject.SearchObject.GradeId == null)
                       && (a.StudentStatusId != StudentStatusEnum.Student)
                       && (a.FirstName.Contains(FilterObject.SearchObject.FirstName) || a.MiddleName.Contains(FilterObject.SearchObject.FirstName) || a.FirstName.Contains(FilterObject.SearchObject.FirstName) || string.IsNullOrEmpty(FilterObject.SearchObject.FirstName))
                       && (a.StudentCode == FilterObject.SearchObject.StudentCode || string.IsNullOrEmpty(FilterObject.SearchObject.StudentCode))
                       && (a.ApplicationNo == FilterObject.SearchObject.ApplicationNo || string.IsNullOrEmpty(FilterObject.SearchObject.ApplicationNo))
                   );
            }

            studentList = this.GetAll(FilterObject.PageNumber, FilterObject.PageSize, FilterObject.Includes, SearchCriteria, FilterObject.SortBy, FilterObject.SortDirection);
            return studentList;
        }

        public bool validationStudentStatus(Student student, int toStatus)
        {
            if (student.StudentStatusId > toStatus)
            {
                return false;
            }
            return true;
        }
        public int GetStudentsFromPreviousSchools(int SchoolID)
        {
            return DbContext.Students.Where(s =>s.SchoolId == SchoolID && s.PreviousSchool != null).Count();
        }

        public PagedResult<Student> GetAllStudentsInterView(FilterModel<Student> FilterObject)
        {
            Model.DTO.PagedResult<Student> studentList = new Model.DTO.PagedResult<Student>();
            Expression<Func<Student, bool>> SearchCriteria = a => (
                (a.ApplicationNo == FilterObject.SearchObject.ApplicationNo ||
                 string.IsNullOrEmpty(FilterObject.SearchObject.ApplicationNo))
                && (a.SchoolId == FilterObject.SearchObject.SchoolId || FilterObject.SearchObject.SchoolId == 0)
                && (a.GradeId == FilterObject.SearchObject.GradeId || FilterObject.SearchObject.GradeId == 0 || FilterObject.SearchObject.GradeId == null)

                && (a.FirstName.Contains(FilterObject.SearchObject.FirstName) || string.IsNullOrEmpty(FilterObject.SearchObject.FirstName) || a.MiddleName.Contains(FilterObject.SearchObject.FirstName) || a.FamilyName.Contains(FilterObject.SearchObject.FirstName))
                &&
                (FilterObject.SearchObject.DateTimeFrom == null || DbFunctions.TruncateTime(a.InterviewDate) >=
                    DbFunctions.TruncateTime(FilterObject.SearchObject.DateTimeFrom)) 
                &&
                (FilterObject.SearchObject.DateTimeTo == null || DbFunctions.TruncateTime(a.InterviewDate) <=
                 DbFunctions.TruncateTime(FilterObject.SearchObject.DateTimeTo))
                 &&
                 (a.StudentStatusId != StudentStatusEnum.Student)
                 &&
                 (a.InterviewDate!=null &&  (a.InterviewStatusId!=InterviewStatusEnum.Accepted&& a.InterviewStatusId != InterviewStatusEnum.Rejected))
                 );

            studentList = this.GetAll(FilterObject.PageNumber, FilterObject.PageSize, FilterObject.Includes, SearchCriteria, FilterObject.SortBy, FilterObject.SortDirection);
  
            return studentList;
             
        }
    }
}
