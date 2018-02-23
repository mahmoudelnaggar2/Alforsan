using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;
using Model.Enums;

namespace WebAPI.Controllers
{
    public class StudentStatusController : SecureController
    {
        private readonly Services.IStudentStatusService _studentStatusService;

        public StudentStatusController(Services.IStudentStatusService studentStatusService)
        {
            this._studentStatusService = studentStatusService;
        }

        // GET api/StudentStatus
        public List<StudentStatus> GetStudentStatus()
        {
            return _studentStatusService.GetAll().Where(s=> s.StudentStatusId != StudentStatusEnum.WaitingInterview && s.StudentStatusId != StudentStatusEnum.Withdraw&&s.StudentStatusId!=StudentStatusEnum.Rejected).ToList();
        }
    }
}
