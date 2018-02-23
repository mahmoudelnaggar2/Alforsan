using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Model;
using Service;

namespace WebAPI.Controllers
{
    public class StudentFinancialController : ApiController
    {
        private readonly IStudentFinancialService _studentFinancial;

        public StudentFinancialController(IStudentFinancialService studentFinancial)
        {
            this._studentFinancial = studentFinancial;
        }

        // GET api/StudentFinancial
        public List<StudentFinancial> GetStudentFinancial()
        {
            return _studentFinancial.GetAll();
        }

        // GET api/StudentFinancial/5
        [ResponseType(typeof(StudentFinancial))]
        public IHttpActionResult GetStudentFinancial(int id)
        {
            StudentFinancial studentFinancial = _studentFinancial.GetStudentFinancial(id);
            if (studentFinancial == null)
            {
                return NotFound();
            }

            return Ok(studentFinancial);
        }


        // POST api/StudentFinancial
        [ResponseType(typeof(StudentFinancial))]
        public IHttpActionResult PostStudentFinancial(StudentFinancial studentFinancial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _studentFinancial.CreateStudentFinancial(studentFinancial);
            _studentFinancial.SaveStudentFinancial();
            return CreatedAtRoute("DefaultApi", new { id = studentFinancial.StudentFinancialId }, studentFinancial);
        }

        // DELETE api/StudentFinancial/5
        [ResponseType(typeof(StudentFinancial))]
        public IHttpActionResult DeleteStudentFinancial(int id)
        {
            StudentFinancial studentFinancial = _studentFinancial.GetStudentFinancial(id);
            if (studentFinancial == null)
            {
                return NotFound();
            }
            try
            {
                _studentFinancial.DeleteStudentFinancial(studentFinancial);
                _studentFinancial.SaveStudentFinancial();
            }
            catch (DbUpdateException ex)
            {
                return Conflict();
            }
            return Ok(studentFinancial);
        }

        // PUT api/StudentFinancial/5
        public IHttpActionResult PutStudentFinancial(int id, StudentFinancial studentFinancial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentFinancial.StudentFinancialId)
            {
                return BadRequest();
            }

            try
            {
                _studentFinancial.UpdateStudentFinancial(studentFinancial);
                _studentFinancial.SaveStudentFinancial();
            }
            catch (Exception ex)
            {
                if (!StudentFinancialExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        private bool StudentFinancialExists(int id)
        {
            StudentFinancial studentFinancial = _studentFinancial.GetStudentFinancial(id);
            return studentFinancial != null;
        }

        [HttpGet]
        [Route("api/StudentFinancial/GetBalance")]
        public decimal GetBalance(int studentId)
        {
            return _studentFinancial.calculateBalance(studentId);
        }
    }
}

