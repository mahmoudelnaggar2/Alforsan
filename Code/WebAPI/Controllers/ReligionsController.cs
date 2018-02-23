using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;

namespace WebAPI.Controllers
{
    public class ReligionsController : SecureController
    {
        private readonly Services.IReligionService _religionService;

        public ReligionsController(Services.IReligionService religionService)
        {
            this._religionService = religionService;
        }

        // GET api/Religions
        public List<Religion> GetReligions()
        {
            return _religionService.GetAll();
        }
    }
}
