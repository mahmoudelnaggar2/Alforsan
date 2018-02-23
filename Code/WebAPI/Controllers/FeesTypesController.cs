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
    public class FeesTypesController : SecureController
    {
        private readonly Services.IFeesTypeService _feesTypeService;

        public FeesTypesController(Services.IFeesTypeService feesTypeService)
        {
            this._feesTypeService = feesTypeService;
        }

        // GET api/FeesTypes
        public List<FeesType> GetFeesTypes()
        {
            return _feesTypeService.GetAll();
        }

        // GET api/FeesTypes/5
        [ResponseType(typeof(FeesType))]
        public IHttpActionResult GetFeesType(int id)
        {
            FeesType feesType = _feesTypeService.GetFeesType(id);
            if (feesType == null)
            {
                return NotFound();
            }

            return Ok(feesType);
        }

        // PUT api/FeesTypes/5
        public IHttpActionResult PutFeesType(int id, FeesType feesType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != feesType.FeesTypeId)
            {
                return BadRequest();
            }

            try
            {
                _feesTypeService.UpdateFeesType(feesType);
                _feesTypeService.SaveFeesType();

            }
            catch (Exception ex)
            {
                if (!FeesTypesExists(id))
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

        // POST api/FeesTypes
        [ResponseType(typeof(FeesType))]
        public IHttpActionResult PostFeesType(FeesType feesType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _feesTypeService.CreateFeesType(feesType);
            _feesTypeService.SaveFeesType();
            return CreatedAtRoute("DefaultApi", new { id = feesType.FeesTypeId }, feesType);
        }

        // DELETE api/FeesTypes/5
        [ResponseType(typeof(FeesType))]
        public IHttpActionResult DeleteFeesTypes(int id)
        {
            FeesType feesType = _feesTypeService.GetFeesType(id);
            if (feesType == null)
            {
                return NotFound();
            }
            try
            {
                _feesTypeService.DeleteFeesType(feesType);
                _feesTypeService.SaveFeesType();
            }
            catch (DbUpdateException ex)
            {
                return Conflict();
            }
            return Ok(feesType);
        }

        [HttpGet]
        [Route("api/FeesTypes/FeesNameExists")]
        public bool FeesNameExists(string name, int id)
        {
            return _feesTypeService.FeesNameExists(name, id);
        }

        private bool FeesTypesExists(int id)
        {
            FeesType feesType = _feesTypeService.GetFeesType(id);
            return feesType != null;
        }
    }
}
