
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
    public class VoucherTypeController : ApiController
    {
        private readonly IVoucherTypeService _voucherTypeService;

        public VoucherTypeController(IVoucherTypeService voucherTypeService)
        {
            this._voucherTypeService = voucherTypeService;
        }
        // GET: api/VoucherType
        public List<VoucherType> Get()
        {
            return _voucherTypeService.GetAll();
        }

        // GET: api/VoucherType/5
        public IHttpActionResult Get(int id)
        {
            VoucherType voucherType = _voucherTypeService.GetVoucherType(id);
            if (voucherType == null)
            {
                return NotFound();
            }

            return Ok(voucherType);
        }

        // POST: api/VoucherType
        public IHttpActionResult Post(VoucherType voucherType)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _voucherTypeService.CreateVoucherType(voucherType);
            _voucherTypeService.SaveVoucherType();
            return CreatedAtRoute("DefaultApi", new { id = voucherType.VoucherTypeId }, voucherType);
        }

        // PUT: api/VoucherType/5
        public IHttpActionResult Put(int id, VoucherType voucherType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != voucherType.VoucherTypeId)
            {
                return BadRequest();
            }

            try
            {
                _voucherTypeService.UpdateVoucherType(voucherType);
                _voucherTypeService.SaveVoucherType();

            }
            catch (Exception ex)
            {
                if (!VouchertypeExists(id))
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
        private bool VouchertypeExists(int id)
        {
            VoucherType voucherType = _voucherTypeService.GetVoucherType(id);
            return voucherType != null;
        }
        // DELETE: api/VoucherType/5
        public IHttpActionResult Delete(int id)
        {
            VoucherType voucherType = _voucherTypeService.GetVoucherType(id);
            if (voucherType == null)
            {
                return NotFound();
            }
            try
            {
                _voucherTypeService.DeleteVoucherType(voucherType);
                _voucherTypeService.SaveVoucherType();
            }
            catch (DbUpdateException ex)
            {
                return Conflict();
            }
            return Ok(voucherType);
        }
    }
}

