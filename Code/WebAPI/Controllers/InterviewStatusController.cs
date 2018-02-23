using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;

namespace WebAPI.Controllers
{
    public class InterviewStatusController : SecureController
    {
        private readonly Services.IInterviewStatusService _interviewStatusService;

        public InterviewStatusController(Services.IInterviewStatusService interviewStatuService)
        {
            this._interviewStatusService = interviewStatuService;
        }

        // GET api/InterviewStatus
        public List<InterviewStatus> GetInterviewStatus()
        {
            return _interviewStatusService.GetAll();
        }
    }
}
