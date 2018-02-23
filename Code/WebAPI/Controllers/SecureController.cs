using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    [Authorize]
    public class SecureController : ApiController
    {
        public SecureController()
        {
            
        }
    }
}
