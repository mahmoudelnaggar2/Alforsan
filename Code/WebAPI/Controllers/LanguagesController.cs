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
    public class LanguagesController : SecureController
    {
        private readonly ILanguageService _LanguageService;
        public LanguagesController(ILanguageService LanguageService)
        {
            this._LanguageService = LanguageService;
        }

        // GET api/Languages
        public List<Language> GetLanguages()
        {
            return _LanguageService.GetAll();
        }


        // GET api/Languages/5
        [ResponseType(typeof(Language))]
        public IHttpActionResult GetLanguage(int id)
        {
            Language Language = _LanguageService.GetLanguage(id);
            if (Language == null)
            {
                return NotFound();
            }

            return Ok(Language);
        }

        // PUT api/Languages/5
        public IHttpActionResult PutLanguage(int id, Language Language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Language.LanguageId)
            {
                return BadRequest();
            }

            try
            {
                _LanguageService.UpdateLanguage(Language);
                _LanguageService.SaveLanguage();

            }
            catch (Exception ex)
            {
                if (!LanguageExists(id))
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

        // POST api/Languages
        [ResponseType(typeof(Language))]
        public IHttpActionResult PostLanguage(Language Language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _LanguageService.CreateLanguage(Language);
            _LanguageService.SaveLanguage();
            return CreatedAtRoute("DefaultApi", new { id = Language.LanguageId }, Language);
        }

        // DELETE api/Languages/5
        [ResponseType(typeof(Language))]
        public IHttpActionResult DeleteLanguage(int id)
        {
            Language Language = _LanguageService.GetLanguage(id);
            if (Language == null)
            {
                return NotFound();
            }
            try
            {
                _LanguageService.DeleteLanguage(Language);
                _LanguageService.SaveLanguage();
            }
            catch (DbUpdateException ex)
            {
                return NotFound();
            }
            return Ok(Language);
        }

        private bool LanguageExists(int id)
        {
            Language Language = _LanguageService.GetLanguage(id);
            return Language != null;
        }

     
 
    }
}
