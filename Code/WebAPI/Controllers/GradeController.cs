using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting;
using System.Web.Http;
using Model;
using Service;
using Model.DTO;
using System.Data.Entity.Infrastructure;

namespace WebAPI.Controllers
{
    public class GradeController : ApiController
    {
        private readonly IGradeService _gradeService;

        public GradeController(IGradeService gradeService)
        {
            this._gradeService = gradeService;
        }

        // GET: api/Grade
        public List<Grade> Get()
        {
            return _gradeService.GetAll();
        }
        // GET: api/Grade/5
        public Grade Get(int id)
        {

            return _gradeService.GetGrade(id);
        }

        // POST: api/Grade
        public IHttpActionResult Post(Grade grade)
        {
               if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
            grade.School = null;
            _gradeService.CreateGrade(grade);
            return CreatedAtRoute("DefaultApi", new { id = grade.GradeId}, grade);
           // return Ok(grade);               
        }

        // PUT: api/Grade/5
        public IHttpActionResult Put(int id,Grade grade)
        {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
            grade.School = null;
                _gradeService.UpdateGrade(id, grade);
            return StatusCode(HttpStatusCode.NoContent);
            //return Ok(grade);        
        }

        // DELETE: api/Grade/5
        public IHttpActionResult Delete(int id)
        {
          
            Grade gradeItem = _gradeService.GetGrade(id);
            if (gradeItem == null)
            {
                return NotFound();
            }
            try
            {
                _gradeService.DeleteGrade(gradeItem);
            }
            catch (DbUpdateException ex)
            {
                return Conflict();
            }

            return Ok();
        }

        [HttpPost]
        [Route("api/Grade/FilteredList")]
        public PagedResult<Grade> LoadFilteredCategories(FilterModel<Grade> FilterObject)
        {
            //if no search is applied
            if (FilterObject.SearchObject == null)
            {
                FilterObject.SearchObject = new Model.Grade();
            }
            var list = _gradeService.LoadFilteredCategories(FilterObject);
            return list;
        }

        [HttpGet]
        [Route("api/Grade/OrderUnique")]
        public bool OrderUnique(int order, int schoolId,int gradeId)
        {
            return _gradeService.OrderUnique(order, schoolId, gradeId);
        }

        [HttpGet]
        [Route("api/Grade/GradeNameUnique")]
        public bool GradeNameUnique(string gradeName, int schoolId,int gradeId)
        {
            return _gradeService.GradeNameUnique(gradeName, schoolId, gradeId);
        }
        [HttpGet]
        [Route("api/Grade/GradeBySchoolId")]
        public List<Grade> GradeBySchoolId(int SchoolId)
        {
            return _gradeService.GradeBySchoolId(SchoolId);
        }
    }
}
