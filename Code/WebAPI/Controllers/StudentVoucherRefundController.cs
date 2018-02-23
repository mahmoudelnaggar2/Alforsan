using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Model;
using Service.Contracts;
using Model.DTO;

namespace WebAPI.Controllers
{
    public class StudentVoucherRefundController : ApiController
    {
        private readonly IStudentVoucherRefundService _studentVoucherRefundService;

        public StudentVoucherRefundController(IStudentVoucherRefundService _studentVoucherRefundService)
        {
            this._studentVoucherRefundService = _studentVoucherRefundService;
        }

        // GET api/StudentVoucherRefund
        public List<StudentVoucherRefund> GetStudentVoucherRefund()
        {
            return _studentVoucherRefundService.GetAll();
        }

        // GET api/StudentVoucherRefund/5
        [ResponseType(typeof(StudentVoucherRefund))]
        public IHttpActionResult GetStudentVoucherRefund(int id)
        {
            StudentVoucherRefund studentVoucherRefund = _studentVoucherRefundService.GetStudentVoucherRefund(id);
            if (studentVoucherRefund == null)
            {
                return NotFound();
            }

            return Ok(studentVoucherRefund);
        }


        // POST api/StudentVoucherRefund
        [ResponseType(typeof(StudentVoucherRefund))]
        public IHttpActionResult PostStudentVoucherRefund(StudentVoucherRefund studentVoucherRefund)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _studentVoucherRefundService.CreateStudentVoucherRefund(studentVoucherRefund);
            _studentVoucherRefundService.SaveStudentVoucherRefund();
            return CreatedAtRoute("DefaultApi", new { id = studentVoucherRefund.StudentVoucherRefundId }, studentVoucherRefund);
        }

        // DELETE api/StudentVoucherRefund/5
        [ResponseType(typeof(StudentVoucherRefund))]
        public IHttpActionResult DeleteStudentVoucherRefund(int id)
        {
            StudentVoucherRefund studentVoucherRefund = _studentVoucherRefundService.GetStudentVoucherRefund(id);
            if (studentVoucherRefund == null)
            {
                return NotFound();
            }
            try
            {
                _studentVoucherRefundService.DeleteStudentVoucherRefund(studentVoucherRefund);
                _studentVoucherRefundService.SaveStudentVoucherRefund();
            }
            catch (DbUpdateException ex)
            {
                return Conflict();
            }
            return Ok(studentVoucherRefund);
        }

        // PUT api/StudentVoucherRefund/5
        public IHttpActionResult PutStudentVoucherRefund(int id,StudentVoucherRefund studentVoucherRefund)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentVoucherRefund.StudentVoucherRefundId)
            {
                return BadRequest();
            }

            try
            {
                _studentVoucherRefundService.UpdateStudentVoucherRefund(id,studentVoucherRefund);
            }
            catch (Exception ex)
            {
                if (!StudentVoucherRefundExists(id))
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

        private bool StudentVoucherRefundExists(int id)
        {
            StudentVoucherRefund studentVoucherRefund = _studentVoucherRefundService.GetStudentVoucherRefund(id);
            return studentVoucherRefund != null;
        }

        [HttpPost]
        [Route("api/StudentVoucherRefund/FilteredList")]
        [ResponseType(typeof(Model.DTO.PagedResult<StudentVoucherRefund>))]
        public Model.DTO.PagedResult<StudentVoucherRefund> LoadFilteredRefund(FilterModel<StudentVoucherRefund> FilterObject)
        {
            if (FilterObject.SearchObject == null)
            {
                FilterObject.SearchObject = new Model.StudentVoucherRefund();
            }
            return _studentVoucherRefundService.GetAll(FilterObject);
        }
    }
}
