using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Data.Migrations;
using Model;
using Model.DTO;

namespace Data.Repositories
{
   public interface IStudentRepository:IRepository<Student>
    {
        PagedResult<Student> GetAll(FilterModel<Student> FilterObject);
        List<Student> GetStudentBySchoolIdAndGradeId(int schoolId, int gradeId);
        List<Student> StudentBySchoolIdAndGradeId(int studentId,int schoolId, int gradeId);
        PagedResult<Student> SearchToGetStudent(FilterModel<Student> FilterObject);
        string GenerateStudentCode(Student student);
        bool ApplicationNoExists(string code, int id);
        bool validationStudentStatus(Student student, int toStatus);
        Student GetStudentByCode(string code);
        int GetStudentsFromPreviousSchools(int SchoolID);
        PagedResult<Student> GetAllStudentsInterView(FilterModel<Student> filterObject);
    }
}
