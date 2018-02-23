using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;
using Services;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;

namespace WebAPI.Controllers
{
    public class NationalitiesController : SecureController
    {
        private readonly INationalityService _NationalityService;
        public NationalitiesController(INationalityService nationalityService)
        {
            this._NationalityService = nationalityService;
        }

        // GET api/Nationalities
        public List<Nationality> GetNationalities()
        {
            return _NationalityService.GetAll();
        }


        // GET api/Nationalitys/5
        [ResponseType(typeof(Nationality))]
        public IHttpActionResult GetNationality(int id)
        {
            Nationality Nationality = _NationalityService.GetNationality(id);
            if (Nationality == null)
            {
                return NotFound();
            }

            return Ok(Nationality);
        }

        // PUT api/Nationalitys/5
        public IHttpActionResult PutNationality(int id, Nationality Nationality)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Nationality.NationalityId)
            {
                return BadRequest();
            }

            try
            {
                _NationalityService.UpdateNationality(Nationality);
                _NationalityService.SaveNationality();

            }
            catch (Exception ex)
            {
                if (!NationalityExists(id))
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

        // POST api/Nationalitys
        [ResponseType(typeof(Nationality))]
        public IHttpActionResult PostNationality(Nationality Nationality)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _NationalityService.CreateNationality(Nationality);
            _NationalityService.SaveNationality();
            return CreatedAtRoute("DefaultApi", new { id = Nationality.NationalityId }, Nationality);
        }

        // DELETE api/Nationalitys/5
        [ResponseType(typeof(Nationality))]
        public IHttpActionResult DeleteNationality(int id)
        {
            Nationality Nationality = _NationalityService.GetNationality(id);
            if (Nationality == null)
            {
                return NotFound();
            }
            try
            {
                _NationalityService.DeleteNationality(Nationality);
                _NationalityService.SaveNationality();
            }
            catch (DbUpdateException ex)
            {
                return NotFound();
            }
            return Ok(Nationality);
        }

        private bool NationalityExists(int id)
        {
            Nationality Nationality = _NationalityService.GetNationality(id);
            return Nationality != null;
        }

        [HttpGet]
        [Route("api/Nationalities/NationalityNameExists")]
        public bool NationalityNameExists(string name, int id)
        {
            return _NationalityService.NationalityNameExists(name, id);
        }
 
    }
}
