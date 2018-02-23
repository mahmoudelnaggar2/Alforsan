using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Data.Infrastructure;
using Model;
using Services;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;

namespace WebAPI.Controllers
{
    public class SettingController : ApiController
    {
        private readonly ISettingService _settingService;
        public SettingController(ISettingService _settingService)
        {
            this._settingService = _settingService;
        }
        // GET: api/Setting
        public List<Setting> Get()
        {
            return _settingService.GetAll();
        }

        // GET: api/Setting/5
        [ResponseType(typeof(Setting))]
        public IHttpActionResult Get(int id)
        {
            Setting setting = _settingService.GetSetting(id);
            if (setting == null)
            {
                return NotFound();
            }

            return Ok(setting);
        }

        // POST: api/Setting
        public IHttpActionResult PostSetting(Setting setting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _settingService.CreateSetting(setting);
            return CreatedAtRoute("DefaultApi", new { id = setting.SettingId }, setting);
        }

        // PUT: api/Setting/5
        public IHttpActionResult PutSetting(int id, Setting setting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != setting.SettingId)
            {
                return BadRequest();
            }

            try
            {
                _settingService.UpdateSetting(setting);

            }
            catch (Exception ex)
            {
                if (!SettingExists(id))
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

        // DELETE: api/Setting/5
        public IHttpActionResult DeleteSetting(int id)
        {
            Setting setting = _settingService.GetSetting(id);
            if (setting == null)
            {
                return NotFound();
            }
            try
            {
                _settingService.DeleteSetting(setting);
            }
            catch (DbUpdateException ex)
            {
                return Conflict();
            }
            return Ok(setting);
        }
        private bool SettingExists(int id)
        {
            Setting setting = _settingService.GetSetting(id);
            return setting != null;
        }
    }
}
