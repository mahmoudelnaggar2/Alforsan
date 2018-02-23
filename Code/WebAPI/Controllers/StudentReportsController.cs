using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Model.DTO;
using System.IO;

namespace WebAPI.Controllers
{
    public class StudentReportsController : ApiController
    {
        private readonly IStudentReportsService _studentReportService;
        public StudentReportsController(IStudentReportsService StudentReportService)
        {
            _studentReportService = StudentReportService;
        }

        [HttpGet]
        [Route("api/StudentReports/{studentId}")]
        //api/StudentReport
        public HttpResponseMessage GetReport(int studentId)
        {
            string fileName = _studentReportService.StuentName(studentId);
            byte[] fileBytes = _studentReportService.GetStudentReport(studentId);
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(fileBytes)
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.Add("Filename", fileName);
            result.Content.Headers.Add("Access-Control-Expose-Headers", "Filename");
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };
            return result;
        }

        [HttpPost]
        [Route("api/StudentReports/Interview")]
        //api/StudentReport
        public HttpResponseMessage PostInterviewReport(ReportInterviewDTO interviewReport)
        {
            string fileName = interviewReport.dateFrom.ToShortDateString()+"To"+ interviewReport.dateTo.ToShortDateString();
            byte[] fileBytes = _studentReportService.GetInterviewReport(interviewReport.dateFrom, interviewReport.dateTo, interviewReport.SchoolId, interviewReport.GradeId,interviewReport.InterViewStatusID);
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(fileBytes)
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.Add("Filename", fileName);
            result.Content.Headers.Add("Access-Control-Expose-Headers", "Filename");
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };
            return result;
        }

        [HttpPost]
        [Route("api/StudentReports/Admission")]
        //api/StudentReport
        public HttpResponseMessage PostAdmissionwReport(ReportAdmissionDTO admissionReport)
        {
            string fileName ="Admission From"+ admissionReport.dateFrom.ToShortDateString() + "To" + admissionReport.dateTo.ToShortDateString();
            byte[] fileBytes = _studentReportService.GetAdmissionReport(admissionReport.dateFrom, admissionReport.dateTo, admissionReport.SchoolId);
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(fileBytes)
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.Add("Filename", fileName);
            result.Content.Headers.Add("Access-Control-Expose-Headers", "Filename");
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };
            return result;
        }

        [HttpPost]
        [Route("api/StudentReports/PreviousSchool/{SchoolID}")]
        //api/StudentReport
        public HttpResponseMessage PostPreviousSchool(int SchoolID)
        {
            string fileName = "PreviousSchoolReport";
            byte[] fileBytes = _studentReportService.GetPreviousSchoolReport(SchoolID);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(fileBytes)
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.Add("Filename", fileName);
            result.Content.Headers.Add("Access-Control-Expose-Headers", "Filename");
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };
            return result;
        }

        [HttpGet]
        [Route("api/StudentReports/UnPaidFees")]
        //api/StudentReport
        public HttpResponseMessage GetUnPaidFees()
        {
            string fileName = "UnPaidFeesReport";
            byte[] fileBytes = _studentReportService.GetUnPaidFeesReport();
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(fileBytes)
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.Add("Filename", fileName);
            result.Content.Headers.Add("Access-Control-Expose-Headers", "Filename");
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };
            return result;
        }

        [HttpPost]
        [Route("api/StudentReports/PaidFees")]
        //api/StudentReport
        public HttpResponseMessage PostPaidFeesReport(PaidFeesDTO PaidFees)
        {
            string fileName = PaidFees.DateTo.ToShortDateString();
            byte[] fileBytes = _studentReportService.GetPaidFeesReport(PaidFees.DateTo);
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(fileBytes)
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.Add("Filename", fileName);
            result.Content.Headers.Add("Access-Control-Expose-Headers", "Filename");
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };
            return result;
        }


        [HttpGet]
        [Route("api/StudentReports/voucher/{StudentVoucherID}")]
        //api/StudentReport
        public HttpResponseMessage GetVoucherReport(int StudentVoucherID)
        {
            string fileName = "Student_Voucher_Report_"+ StudentVoucherID;
            byte[] filebytes = _studentReportService.GetVoucherReport(StudentVoucherID);
            var Result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(filebytes)
            };
            Result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            Result.Content.Headers.Add("Filename", fileName);
            Result.Content.Headers.Add("Access-Control-Expose-Headers", "Filename");
            Result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };
            return Result;
        }

        [HttpPost]
        [Route("api/StudentReports/NewApplicants")]
        public HttpResponseMessage PostNewApplicants(NewApplicantsDTO NewApplicants)
        {
            string WebURL = System.Configuration.ConfigurationManager.AppSettings["WebURL"];
            string fileName = "NewApplicants_"+DateTime.Now;
            byte[] fileBytes = _studentReportService.GetNewApplicantsReport(NewApplicants,WebURL);
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(fileBytes)
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.Add("Filename", fileName);
            result.Content.Headers.Add("Access-Control-Expose-Headers", "Filename");
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };
            return result;
        }
    }
}
