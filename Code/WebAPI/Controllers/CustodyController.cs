using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;
using Services;

namespace WebAPI.Controllers
{
    public class CustodyController : SecureController
    {
        private readonly ICustodyService _custodyService;

        #region constructor
        public CustodyController(ICustodyService custodyService)
        {
            this._custodyService = custodyService;
        }
#endregion
        // GET: api/Custody
        public List<Custody> Get()
        {
            return _custodyService.GetAll();
        }
       
    }
}
