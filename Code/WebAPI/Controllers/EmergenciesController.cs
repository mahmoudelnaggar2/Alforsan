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
    public class EmergenciesController : SecureController
    {
        private readonly Services.IEmergencyService _emergencyService;

        public EmergenciesController(Services.IEmergencyService emergencyService)
        {
            this._emergencyService = emergencyService;
        }

        // GET api/Emergencies
        public List<Emergency> GetEmergencies()
        {
            return _emergencyService.GetAll();
        }


        // GET api/Emergencies/5
        [ResponseType(typeof(Emergency))]
        public IHttpActionResult GetEmergency(int id)
        {
            Emergency emergency = _emergencyService.GetEmergency(id);
            if (emergency == null)
            {
                return NotFound();
            }

            return Ok(emergency);
        }

        // POST api/Emergencies
        [ResponseType(typeof(Emergency))]
        public IHttpActionResult PostEmergency(Emergency emergency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _emergencyService.CreateEmergency(emergency);
            _emergencyService.SaveEmergency();      
            return CreatedAtRoute("DefaultApi", new { id = emergency.EmergencyId }, emergency);
        }

        // DELETE api/Emergencies/5
        [ResponseType(typeof(Emergency))]
        public IHttpActionResult DeleteEmergency(int id)
        {
            Emergency emergency = _emergencyService.GetEmergency(id);
            if (emergency == null)
            {
                return NotFound();
            }
            try
            {
                _emergencyService.DeleteEmergency(emergency);
                _emergencyService.SaveEmergency();  
            }
            catch (DbUpdateException ex)
            {
                return Conflict();
            }
            return Ok(emergency);
        }
        // PUT api/Emergencies/5
        public IHttpActionResult PutEmergency(int id, Emergency emergency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != emergency.EmergencyId)
            {
                return BadRequest();
            }

            try
            {
                _emergencyService.UpdateEmergency(emergency);
                _emergencyService.SaveEmergency();

            }
            catch (Exception ex)
            {
                if (!EmergencyExists(id))
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

        private bool EmergencyExists(int id)
        {
            Emergency emergency = _emergencyService.GetEmergency(id);
            return emergency != null;
        }
    }
}
