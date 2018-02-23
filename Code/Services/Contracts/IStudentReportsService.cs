using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IStudentReportsService
    {
        byte[] GetStudentReport(int studentId);

        byte[] GetInterviewReport(DateTime datefrom, DateTime dateTo, int schoolId, int gradeId, int InterViewStatusID);
        byte[] GetAdmissionReport(DateTime datefrom, DateTime dateTo, int schoolId);
        string StuentName(int studentId);

        byte[] GetVoucherReport(int StudentVoucherID);
       byte[] GetPreviousSchoolReport(int SchoolID);
       byte[] GetUnPaidFeesReport();
        byte[] GetPaidFeesReport(DateTime dateTo);
        byte[] GetNewApplicantsReport(NewApplicantsDTO NewApplicants, string WebURL);
    }
}
