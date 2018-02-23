using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;
using Service;
using System.Data.Entity.Infrastructure;

namespace WebAPI.Controllers
{
    public class YearController : ApiController
    {
        private readonly IYearService _yearService;

        public YearController(IYearService yearService)
        {
            this._yearService = yearService;
        }
        // GET: api/Year
        public List<Year> Get()
        {
            return _yearService.GetAll();
        }

        // GET: api/Year/5
        public IHttpActionResult Get(int id)
        {
            var yearItem = _yearService.GetYear(id);
            if (yearItem != null)
            {
                return Ok(yearItem);
            }
            return NotFound();
        }

        // POST: api/Year
        public IHttpActionResult Post(Year year)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _yearService.CreateYear(year);
            if(year.IsCurrent)
                ActiveOnlyOneYear(year.ShortName);
            return CreatedAtRoute("DefaultApi", new { id = year.YearId }, year);
        }

        // PUT: api/Year/5
        public IHttpActionResult Put(int id, Year year)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _yearService.UpdateYear(id,year);
            if (year.IsCurrent)
                ActiveOnlyOneYear(year.ShortName);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Year/5
        public IHttpActionResult Delete(int id)
        {
            Year yearItem = _yearService.GetYear(id);
            if (yearItem == null)
            {
                return NotFound();
            }
            try
            {
                _yearService.DeleteYear(yearItem);
            }
            catch (DbUpdateException ex)
            {
                return Conflict();
            }
           
            return Ok();       
        }
        [HttpGet]
        [Route("api/Year/YearNameUnique")]
        public bool YearNameUnique(string yearName, int yearId)
        {
            return _yearService.YearNameUnique(yearName, yearId);
        }

        [HttpGet]
        [Route("api/Year/shortNameUnique")]
        public bool ShortNameUnique(int shortName, int yearId)
        {
            return _yearService.ShortNameUnique(shortName, yearId);
        }

        public void ActiveOnlyOneYear(int shortName)
        {
            _yearService.ActiveOnlyOneYear(shortName);
        }


        [HttpGet]
        [Route("api/Year/CurrentYear")]
        public IHttpActionResult GetCurrentYear()
        {
            Year curerntYear=_yearService.GetCurrentYear();
            if (curerntYear == null)
            {
                return NotFound();
            }
            return Ok(curerntYear);
        }
    }
}
