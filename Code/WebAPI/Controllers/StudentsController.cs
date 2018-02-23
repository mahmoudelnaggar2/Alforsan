using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Model;
using Model.DTO;

namespace WebAPI.Controllers
{
    public class StudentsController : ApiController
    {
        private readonly Services.IStudentService _studentService;

        public StudentsController(Services.IStudentService studentService)
        {
            this._studentService = studentService;
        }

        // GET api/Students
        public List<Student> GetStudents()
        {
            return _studentService.GetAll();
        }


        // GET api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult GetStudent(int id)
        {
            Student student = _studentService.GetStudent(id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // DELETE api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult DeleteStudent(int id)
        {
            Student student = _studentService.GetStudent(id);
            if (student == null)
            {
                return NotFound();
            }
            try
            {
                _studentService.DeleteStudent(student);
                _studentService.SaveStudent();
            }
            catch (DbUpdateException ex)
            {
                return Conflict();
            }
            return Ok(student);
        }

        // PUT api/Students/5
        [HttpPut]
        public IHttpActionResult PutStudent(int id, Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.StudentId)
            {
                return BadRequest();
            }

            try
            {
                _studentService.UpdateStudent(student);
                _studentService.SaveStudent();

            }
            catch (Exception ex)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw ex;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
        [HttpGet]
        [Route("api/Students/ApplicationNoExists")]
        public bool ApplicationNoExists(string code, int id)
        {
            return _studentService.ApplicationNoExists(code, id);
        }

        private bool StudentExists(int id)
        {
            Student student = _studentService.GetStudent(id);
            return student != null;
        }

        [HttpPost]
        [Route("api/Students/FilteredList")]
        [ResponseType(typeof(Model.DTO.PagedResult<Student>))]
        public Model.DTO.PagedResult<Student> FilteredList(FilterModel<Student> FilterObject)
        {
            //if no search is applied
            if (FilterObject.SearchObject == null)
            {

                FilterObject.SearchObject = new Model.Student();
            }

            var list = _studentService.GetAll(FilterObject);
            return list;
        }

        [HttpGet]
        [Route("api/Students/GetStudentBySchoolIdAndGradeId")]
        public List<Student> GetStudentBySchoolIdAndGradeId(int schoolId, int gradeId)
        {
            return _studentService.GetStudentBySchoolIdAndGradeId(schoolId, gradeId);
        }

        [HttpGet]
        [Route("api/Students/StudentBySchoolIdAndGradeId")]
        public List<Student> StudentBySchoolIdAndGradeId(int studentId, int schoolId, int gradeId)
        {
            return _studentService.StudentBySchoolIdAndGradeId(studentId, schoolId, gradeId);
        }

        [HttpGet]
        [Route("api/Students/StudentByCode")]
        public Student StudentByCode(string code)
        {
            return _studentService.GetStudentByCode(code);
        }


        [HttpPost]
        [Route("api/Students/SearchForStudent")]
        [ResponseType(typeof(Model.DTO.PagedResult<Student>))]
        public Model.DTO.PagedResult<Student> SearchToGetStudent(FilterModel<Student> FilterObject)
        {
            //if no search is applied
            if (FilterObject.SearchObject == null)
            {
                FilterObject.SearchObject = new Model.Student();
            }
            var list = _studentService.SearchToGetStudent(FilterObject);
            return list;
        }

        [HttpPost]
        [Route("api/Students/TransferToStudent")]
        public IHttpActionResult TransferToStudent(List<Student> students)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            _studentService.TransferToStudent(students);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("api/Students/UpgradeStudent")]
        public IHttpActionResult UpgradeStudent(List<Student> students)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            _studentService.UpgradeStudent(students);
            _studentService.SaveStudent();
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [Route("api/Students/ValidationStudentStatus/{studentId}/{toStatus}")]
        public bool ValidationStudentStatus(int studentId, int toStatus)
        {
            return _studentService.validationStudentStatus(studentId, toStatus);
        }
        [HttpGet]
        [Route("api/Students/ActivateStudent/{studentId}/{isActive}")]
        public IHttpActionResult ActivateStudent(int studentId, bool isActive)
        {
            if (!StudentExists(studentId))
            {
                return NotFound();
            }
            _studentService.ActivateStudent(studentId, isActive);
            return StatusCode(HttpStatusCode.NoContent);
        }
        [HttpPost]
        [ResponseType(typeof(Student))]
        public IHttpActionResult PostStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _studentService.CreateStudent(student, student.FreeAdmission);
            //  _studentService.SaveStudent();

            return CreatedAtRoute("DefaultApi", new { controller = "Students", id = student.StudentId }, student);
        }

        [HttpGet]
        [Route("api/Students/StudentsFromPreviousSchools/{SchoolID}")]
        public IHttpActionResult GetStudentsFromPreviousSchools(int SchoolID)
        {
            int NumberOfStudents = _studentService.GetStudentsFromPreviousSchools(SchoolID);
            return Ok(NumberOfStudents);
        }



        [HttpPost]
        [Route("api/Students/GetAllStudentsInterView")]
        [ResponseType(typeof(Model.DTO.PagedResult<Student>))]
        public Model.DTO.PagedResult<Student> GetAllStudentsInterView(FilterModel<Student> FilterObject)
        {
            //if no search is applied
            if (FilterObject.SearchObject == null)
            {

                FilterObject.SearchObject = new Model.Student();
            }

            var list = _studentService.GetAllStudentsInterView(FilterObject);
            return list;
        }


        [HttpPost]
        [Route("api/Students/UpdateStudentInterViewStatus")]
        public bool UpdateStudentInterViewStatus(Student student)
        {
            bool IsUpdated = false;
            _studentService.UpdateStudentInterViewStatus(student);
            _studentService.SaveStudent();
            IsUpdated = true;
            return IsUpdated;
        }
    }
}
