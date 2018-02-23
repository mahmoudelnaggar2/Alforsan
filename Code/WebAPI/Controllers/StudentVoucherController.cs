using System;
using System.Collections.Generic;
using System.Linq;
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
using Model.DTO;

namespace WebAPI.Controllers
{
    public class StudentVoucherController : ApiController
    {
        private readonly IStudentvoucherService _studentvoucherService;

        public StudentVoucherController(IStudentvoucherService studentvoucherService)
        {
            this._studentvoucherService = studentvoucherService;
        }

        // GET api/StudentVoucher
        public List<StudentVoucher> GetStudentVoucher()
        {
            return _studentvoucherService.GetAll();
        }


        // GET api/StudentVoucher/5
        [ResponseType(typeof(StudentVoucher))]
        public IHttpActionResult GetStudentVoucher(int id)
        {
            StudentVoucher studentVoucher = _studentvoucherService.GetStudentVoucher(id);
            if (studentVoucher == null)
            {
                return NotFound();
            }

            return Ok(studentVoucher);
        }

        // POST api/StudentVoucher
        [ResponseType(typeof(StudentVoucher))]
        public IHttpActionResult PostStudentVoucher(StudentVoucher studentVoucher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _studentvoucherService.CreateStudentVoucher(studentVoucher,false);
            return CreatedAtRoute("DefaultApi", new { id = studentVoucher.StudentVoucherId }, studentVoucher);
        }

        // DELETE api/StudentVoucher/5
        [ResponseType(typeof(StudentVoucher))]
        public IHttpActionResult DeleteStudentVoucher(int id)
        {
            StudentVoucher studentVoucher = _studentvoucherService.GetStudentVoucher(id);
            if (studentVoucher == null)
            {
                return NotFound();
            }
            try
            {
                _studentvoucherService.DeleteStudentVoucher(studentVoucher);
                _studentvoucherService.SaveStudentVoucher();
            }
            catch (DbUpdateException ex)
            {
                return Conflict();
            }
            return Ok(studentVoucher);
        }
        // PUT api/StudentVoucher/5
        public IHttpActionResult PutStudentVoucher(int id, StudentVoucher studentVoucher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentVoucher.StudentVoucherId)
            {
                return BadRequest();
            }

            try
            {
                _studentvoucherService.UpdatePaidVoucher(studentVoucher.StudentVoucherId, studentVoucher);
                _studentvoucherService.SaveStudentVoucher();

            }
            catch (Exception ex)
            {
                if (!StudentVoucherExists(id))
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

        private bool StudentVoucherExists(int id)
        {
            StudentVoucher studentVoucher = _studentvoucherService.GetStudentVoucher(id);
            return studentVoucher != null;
        }

        [HttpPost]
        [Route("api/StudentVoucher/FilteredList")]
        [ResponseType(typeof(Model.DTO.PagedResult<StudentVoucher>))]
        public Model.DTO.PagedResult<StudentVoucher> LoadFilteredVoucher(FilterModel<StudentVoucher> FilterObject)
        {
            //if no search is applied
            if (FilterObject.SearchObject == null)
            {
                FilterObject.SearchObject = new Model.StudentVoucher();
            }
            return _studentvoucherService.GetAll(FilterObject);
        }

    }
}

