using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;

namespace WebAPI.Controllers
{
    public class ParentStatusController : SecureController
    {
        private readonly Services.IParentStatusService _parentStatusService;

        public ParentStatusController(Services.IParentStatusService parentStatusService)
        {
            this._parentStatusService = parentStatusService;
        }

        // GET api/ParentStatus
        public List<ParentStatus> GetParentStatus()
        {
            return _parentStatusService.GetAll();
        }
    }
}
