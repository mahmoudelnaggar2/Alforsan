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
    public class DiscountsTypesController : SecureController
    {                          
        private readonly Services.IDiscountsTypeService _discountsTypeService;

        public DiscountsTypesController(Services.IDiscountsTypeService discountsTypeService)
        {
            this._discountsTypeService = discountsTypeService;
        }

        // GET api/DiscountsTypes
        [Route("api/DiscountsTypes")]
        public List<DiscountsType> GetDiscountsTypes()
        {
            return _discountsTypeService.GetAll();
        }

        // GET api/DiscountsTypes/5
        [ResponseType(typeof(DiscountsType))]
        public IHttpActionResult GetDiscountsType(int id)
        {
            DiscountsType discountsType = _discountsTypeService.GetDiscountsType(id);
            if (discountsType == null)
            {
                return NotFound();
            }

            return Ok(discountsType);
        }

        // PUT api/DiscountsTypes/5
        public IHttpActionResult PutDiscountsType(int id, DiscountsType discountsType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != discountsType.DiscountsTypeID)
            {
                return BadRequest();
            }

            try
            {
                _discountsTypeService.UpdateDiscountsType(discountsType);
                _discountsTypeService.SaveDiscountsType();

            }
            catch (Exception ex)
            {
                if (!DiscountsTypesExists(id))
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

        // POST api/DiscountsTypes
        [HttpPost]
        [Route("api/DiscountsTypes", Name = "DiscountsTypes")]
        [ResponseType(typeof(DiscountsType))]
        public IHttpActionResult PostDiscountsType(DiscountsType discountsType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _discountsTypeService.CreateDiscountsType(discountsType);
            _discountsTypeService.SaveDiscountsType();
            return CreatedAtRoute("DefaultApi", new { controller = "DiscountsTypes", id = discountsType.DiscountsTypeID }, discountsType);
        }

        // DELETE api/DiscountsTypes/5
        [ResponseType(typeof(DiscountsType))]
        public IHttpActionResult DeleteDiscountsTypes(int id)
        {
            DiscountsType discountsType = _discountsTypeService.GetDiscountsType(id);
            if (discountsType == null)
            {
                return NotFound();
            }
            try
            {
                _discountsTypeService.DeleteDiscountsType(discountsType);
                _discountsTypeService.SaveDiscountsType();
            }
            catch (DbUpdateException ex)
            {
                return Conflict();
            }
            return Ok(discountsType);
        }

        [HttpGet]
        [Route("api/DiscountsTypes/DiscountsNameExists")]
        public bool DiscountsNameExists(string name, int id)
        {
            return _discountsTypeService.DiscountsNameExists(name, id);  
        }

        private bool DiscountsTypesExists(int id)
        {
            DiscountsType discountsType = _discountsTypeService.GetDiscountsType(id);
            return discountsType != null;
        }
    }
}
