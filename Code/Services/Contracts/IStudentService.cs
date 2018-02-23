using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTO;

namespace Services
{
   public interface IStudentService
    {
        List<Student> GetAll();
        PagedResult<Student> GetAll(int PageNumber, int PageSize, string SortBy, string SortDirection);
        Student GetStudent(int id);
        void CreateStudent(Student student, bool FreeAdmission);
        void UpdateStudent(Student student);
        void DeleteStudent(Student student);
        void SaveStudent();

        PagedResult<Student> GetAll(FilterModel<Student> FilterObject);
        List<Student> GetStudentBySchoolIdAndGradeId(int schoolId, int gradeId);
        List<Student> StudentBySchoolIdAndGradeId(int studentId,int schoolId, int gradeId);
        PagedResult<Student> SearchToGetStudent(FilterModel<Student> FilterObject);
        string GenerateStudentCode(Student student);
        void TransferToStudent(List<Student> students);
        void UpgradeStudent(List<Student> students);
        void UpgradeApplicant(Student students);
        bool ApplicationNoExists(string code, int id);
        bool validationStudentStatus(int studentId, int toStatus);
        void ActivateStudent(int studentId, bool isActive);
        Student GetStudentByCode(string code);
        int GetStudentsFromPreviousSchools(int SchoolID);
        PagedResult<Student> GetAllStudentsInterView(FilterModel<Student> filterObject);
        void UpdateStudentInterViewStatus(Student student);
    }
}
