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
    public class SiblingsController : SecureController
    {
        private readonly Services.ISiblingService _siblingService;

        public SiblingsController(Services.ISiblingService siblingService)
        {
            this._siblingService = siblingService;
        }

        // GET api/Siblings
        public List<Sibling> GetSiblings()
        {
            return _siblingService.GetAll();
        }

        // GET api/Siblings/5
        [ResponseType(typeof(Sibling))]
        public IHttpActionResult GetSibling(int id)
        {
            Sibling sibling = _siblingService.GetSibling(id);
            if (sibling == null)
            {
                return NotFound();
            }

            return Ok(sibling);
        }


        // POST api/Siblings
        [ResponseType(typeof(Sibling))]
        public IHttpActionResult PostSibling(Sibling sibling)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _siblingService.CreateSibling(sibling);
            _siblingService.SaveSibling();
            return CreatedAtRoute("DefaultApi", new { id = sibling.SiblingId }, sibling);
        }

        // DELETE api/Siblings/5
        [ResponseType(typeof(Sibling))]
        public IHttpActionResult DeleteSibling(int id)
        {
            Sibling sibling = _siblingService.GetSibling(id);
            if (sibling == null)
            {
                return NotFound();
            }
            try
            {
                _siblingService.DeleteSibling(sibling);
                _siblingService.SaveSibling();
            }
            catch (DbUpdateException ex)
            {
                return Conflict();
            }
            return Ok(sibling);
        }

        // PUT api/Siblings/5
        public IHttpActionResult PutSibling(int id, Sibling sibling)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sibling.SiblingId)
            {
                return BadRequest();
            }

            try
            {
                _siblingService.UpdateSibling(sibling);
                _siblingService.SaveSibling();

            }
            catch (Exception ex)
            {
                if (!SiblingExists(id))
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

        private bool SiblingExists(int id)
        {
            Sibling sibling = _siblingService.GetSibling(id);
            return sibling != null;
        }
    }
}
