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
    public class ParentsController : SecureController
    {
        private readonly Services.IParentService _parentService;

        public ParentsController(Services.IParentService parentService)
        {
            this._parentService = parentService;
        }

        // GET api/Parents
        public List<Parent> GetParents()
        {
            return _parentService.GetAll();
        }


        // GET api/Parents/5
        [ResponseType(typeof(Parent))]
        public IHttpActionResult GetParent(int id)
        {
            Parent parent = _parentService.GetParent(id);
            if (parent == null)
            {
                return NotFound();
            }

            return Ok(parent);
        }

        // PUT api/Parents/5
        public IHttpActionResult PutParent(int id, Parent parent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parent.ParentId)
            {
                return BadRequest();
            }

            try
            {
                _parentService.UpdateParent(parent);
                _parentService.SaveParent();

            }
            catch (Exception ex)
            {
                if (!ParentExists(id))
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

        // POST api/Parents
        [ResponseType(typeof(Parent))]
        public IHttpActionResult PostParent(Parent parent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _parentService.CreateParent(parent);
            _parentService.SaveParent();
            return CreatedAtRoute("DefaultApi", new { id = parent.ParentId }, parent);
        }

        // DELETE api/Parents/5
        [ResponseType(typeof(Parent))]
        public IHttpActionResult DeleteParent(int id)
        {
            Parent parent = _parentService.GetParent(id);
            if (parent == null)
            {
                return NotFound();
            }
            try
            {
                _parentService.DeleteParent(parent);
                _parentService.SaveParent();        
            }
            catch (DbUpdateException ex)
            {
                return Conflict();
            }
            return Ok(parent);
        }


        private bool ParentExists(int id)
        {
            Parent parent = _parentService.GetParent(id);
            return parent != null;
        }
    }
}
