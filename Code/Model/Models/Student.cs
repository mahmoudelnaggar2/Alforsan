using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Student:BaseModel
    {
        public int StudentId { get; set; }
        public string ApplicationNo { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int SchoolId { get; set; }
        public int? GradeId { get; set; }
        public string FamilyName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public int? NationalityId { get; set; }
        public int? ReligionId { get; set; }
        public int? GenderId { get; set; }
        public string PreviousSchool { get; set; }
        public int? FatherId { get; set; }
        public int? MotherId { get; set; }
        public bool? LanguageSpokenAtHome { get; set; }
        public bool? GrandparentsLiveWithTheFamily { get; set; }
        public int? ParentStatusId { get; set; }
        public int? CustodyId { get; set; }
        public int? StudentStatusId { get; set; }
        public bool? MedicalIssues { get; set; }
        public bool? RegularMedication { get; set; }
        public string CurrentMedication { get; set; }
        public bool? Transportation { get; set; }
        public DateTime? InterviewDate { get; set; }
        public int? InterviewStatusId { get; set; }
        public string InterviewComments { get; set; }
        public DateTime? RegistrationFeesDate { get; set; }
        public DateTime? SchoolFeesDate { get; set; }
        public string Signature { get; set; }
        public Parent Father { get; set; }
        public Parent Mother { get; set; } 
        public Custody Custody { get; set; }
        public ParentStatus ParentStatus { get; set; }
        public InterviewStatus InterviewStatus { get; set; }
        public StudentStatus StudentStatus { get; set; }
        public Gender Gender { get; set; }
        public Religion Religion { get; set; }
        public Nationality Nationality { get; set; }
        public Grade Grade { get; set; }
        public School School { get; set; }
        public List<Sibling> Siblings { get; set; }
        public List<Sibling> StudentSiblings { get; set; }
        public List<Emergency> Emergencies { get; set; }

        public List<StudentLanguage> StudentLanguages { get; set; }

        public string StudentCode { get; set; }
        public bool IsSuspend { get; set; }

        public int? JoiningYearId { get; set; } 
        public Year JoiningYear { get; set; }

        public bool FreeAdmission { get; set; }
        public int? DiscountsTypeID { get; set; }
        public DiscountsType DiscountsType { get; set; }
        public Student()
        {
            Siblings=new List<Sibling>();
            Emergencies=new List<Emergency>();
            CheckedInterviewStatus = new List<int>();
            StudentLanguages = new List<Model.StudentLanguage>();
        }
    }
}
