using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;

namespace WebAPI.Controllers
{
    public class GenderController : SecureController
    {
        private readonly Services.IGenderService _genderService;

        public GenderController(Services.IGenderService genderService)
        {
            this._genderService = genderService;
        }

        // GET api/Gender
        public List<Gender> GetGenders()
        {
            return _genderService.GetAll();
        }
    }
}
