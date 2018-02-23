using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Model;

namespace WebAPI.Controllers
{
    public class SchoolsController : SecureController
    {
        private readonly Services.ISchoolService _schoolService;

        public SchoolsController(Services.ISchoolService schoolService)
        {
            this._schoolService = schoolService;
        }

        // GET api/Schools
        public List<School> GetSchools()
        {
            return _schoolService.GetAll();
        }

        // GET api/Schools/5
        [ResponseType(typeof(School))]
        public IHttpActionResult GetSchool(int id)
        {
            School school = _schoolService.GetSchool(id);
            if (school == null)
            {
                return NotFound();
            }

            return Ok(school);
        }

        // PUT api/Schools/5
        public IHttpActionResult PutSchool(int id, School school)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != school.SchoolId)
            {
                return BadRequest();
            }

            try
            {
                    _schoolService.UpdateSchool(school);
                    _schoolService.SaveSchool();

            }
            catch (Exception ex)
            {
                if (!SchoolExists(id))
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

        // POST api/Schools
        [ResponseType(typeof(School))]
        public IHttpActionResult PostSchool(School school)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                _schoolService.CreateSchool(school);
                _schoolService.SaveSchool();
                return CreatedAtRoute("DefaultApi", new { id = school.SchoolId }, school);
        }

        // DELETE api/Schools/5
        [ResponseType(typeof(School))]
        public IHttpActionResult DeleteSchool(int id)
        {
            School school = _schoolService.GetSchool(id);
            if (school == null)
            {
                return NotFound();
            }
            try
            {
                _schoolService.DeleteSchool(school);
                _schoolService.SaveSchool();
            }
            catch (DbUpdateException ex)
            {
                return NotFound();
            }
            return Ok(school);
        }

        private bool SchoolExists(int id)
        {
            School school = _schoolService.GetSchool(id);
            return school != null;
        }

        [HttpGet]
        [Route("api/Schools/SchoolNameExists")]
        public bool SchoolNameExists(string name,int id)
        {
            return _schoolService.SchoolNameExists(name,id);
        }

        [HttpGet]
        [Route("api/Schools/PrefixExists")]
        public bool PrefixExists(int prefix, int id)
        {
            return _schoolService.SchoolPrefixExists(prefix, id);
        }

    }
}
