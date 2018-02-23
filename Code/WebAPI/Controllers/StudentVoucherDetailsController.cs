
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
    public class StudentVoucherDetailsController : ApiController
    {
        private readonly IStudentVoucherDetailsService _studentVoucherDetailsService;

        public StudentVoucherDetailsController(IStudentVoucherDetailsService _studentVoucherDetailsService)
        {
            this._studentVoucherDetailsService = _studentVoucherDetailsService;
        }

        // GET api/StudentVoucherDetails
        public List<StudentVoucherDetails> GetStudentVoucherDetails()
        {
            return _studentVoucherDetailsService.GetAll();
        }

        // GET api/StudentVoucherDetails/5
        [ResponseType(typeof(StudentVoucherDetails))]
        public IHttpActionResult GetStudentVoucherDetails(int id)
        {
            StudentVoucherDetails studentVoucherDetails = _studentVoucherDetailsService.GetStudentVoucherDetails(id);
            if (studentVoucherDetails == null)
            {
                return NotFound();
            }

            return Ok(studentVoucherDetails);
        }


        // POST api/StudentVoucherDetails
        [ResponseType(typeof(StudentVoucherDetails))]
        public IHttpActionResult PostStudentVoucherDetails(StudentVoucherDetails studentVoucherDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _studentVoucherDetailsService.CreateStudentVoucherDetails(studentVoucherDetails);
            _studentVoucherDetailsService.SaveStudentVoucherDetails();
            return CreatedAtRoute("DefaultApi", new { id = studentVoucherDetails.StudentVoucherDetailsId }, studentVoucherDetails);
        }

        // DELETE api/StudentVoucherDetails/5
        [ResponseType(typeof(StudentVoucherDetails))]
        public IHttpActionResult DeleteStudentVoucherDetails(int id)
        {
            StudentVoucherDetails studentVoucherDetails = _studentVoucherDetailsService.GetStudentVoucherDetails(id);
            if (studentVoucherDetails == null)
            {
                return NotFound();
            }
            try
            {
                _studentVoucherDetailsService.DeleteStudentVoucherDetails(studentVoucherDetails);
                _studentVoucherDetailsService.SaveStudentVoucherDetails();
            }
            catch (DbUpdateException ex)
            {
                return Conflict();
            }
            return Ok(studentVoucherDetails);
        }

        // PUT api/StudentVoucherDetails/5
        public IHttpActionResult PutStudentVoucherDetails(int id, StudentVoucherDetails studentVoucherDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentVoucherDetails.StudentVoucherDetailsId)
            {
                return BadRequest();
            }

            try
            {
                _studentVoucherDetailsService.UpdateStudentVoucherDetails(studentVoucherDetails);
                _studentVoucherDetailsService.SaveStudentVoucherDetails();
            }
            catch (Exception ex)
            {
                if (!StudentVoucherDetailsExists(id))
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

        private bool StudentVoucherDetailsExists(int id)
        {
            StudentVoucherDetails studentVoucherDetails = _studentVoucherDetailsService.GetStudentVoucherDetails(id);
            return studentVoucherDetails != null;
        }
    }
}

