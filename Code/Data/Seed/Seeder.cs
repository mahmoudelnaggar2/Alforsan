using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Enums;

namespace Data
{
    public static class Seeder
    {

        public static void SeedAllData(Data.DBEntities context)
        {
            PopulateFeatures(context);
            PopulateRights(context);
            PopulateRoles(context);
            PopulateUserData(context);
            PopulateCustdoy(context);
            PopulateGender(context);
            PopulateParentStatus(context);
            PopulateReligion(context);
            PopulateInterviewStatus(context);
            PopulateNationality(context);
            PopulateStudentStatus(context);
            PopulateVoucherType(context);
            PopulateFeesType(context);

            PopulateLangugae(context);
            PopulateSettings(context);


        }

        private static void PopulateUserData(Data.DBEntities context)
        {
            IList<User> lstUsers = new List<User>();

            User useSeed = new User();
            useSeed.FullName = " Admin forsan";
            useSeed.UserEmail = "sawsan.m.abdallah@gmail.com";
            useSeed.UserName = "admin";
            useSeed.RoleID = 1;
            useSeed.UserPassword = "ꉟ뺾";
            useSeed.IsActive = true;
            useSeed.UserID = 1;
            useSeed.SchoolId = null;
            lstUsers.Add(useSeed);
            foreach (User std in lstUsers)
            {
                var adminUser = context.Users.FirstOrDefault(p => p.UserID == std.UserID);
                if (adminUser == null)
                {
                    context.Users.Add(std);
                }
            }
        }

        private static void PopulateFeatures(Data.DBEntities context)
        {
            List<Feature> features = new List<Feature>()
            {
                new Feature() {FeatureID = 1, FeatureName = "Applicant", FeatureOrder=1, IsVisible = true},
                new Feature() {FeatureID = 2, FeatureName = "Student Affairs",FeatureOrder=2, IsVisible = true},
                new Feature() {FeatureID = 3, FeatureName = "Admin",FeatureOrder=3, IsVisible = true},
                new Feature() {FeatureID = 4, FeatureName = "Report",FeatureOrder=4, IsVisible = true}

            };

            foreach (Feature ft in features)
            {
                context.Features.AddOrUpdate(f => f.FeatureName, ft);

            }

        }

        private static void PopulateRights(Data.DBEntities context)
        {
            List<Right> rights = new List<Right>()
            {
                new Right()
                {
                    RightID = 1,
                    RightCode = "roles",
                    RightName = "Roles",
                    FeatureID = 3,
                    IsVisible = true,
                    MenuIcon = "fa fa-users",
                    RightURL = "#/pages/roles"
                },
                new Right()
                {
                    RightID = 2,
                    RightCode = "users",
                    RightName = "Users",
                    FeatureID = 3,
                    IsVisible = true,
                    MenuIcon = "fa fa-users",
                    RightURL = "#/pages/users"
                },
                new Right()
                {
                    RightID = 3,
                    RightCode = "Schools",
                    RightName = "Schools",
                    FeatureID = 3,
                    MenuIcon = "icon-graduation",
                    IsVisible = true,
                    RightURL = "#/pages/schools"
                },
                new Right()
                {
                    RightID = 4,
                    RightCode = "Grades",
                    RightName = "Grades",
                    FeatureID = 3,
                    MenuIcon = "icon-book-open",
                    IsVisible = true,
                    RightURL = "#/pages/grades"
                },
                new Right()
                {
                    RightID = 5,
                    RightCode = "Fees Types",
                    RightName = "Fees Types",
                    FeatureID =3,
                    MenuIcon = "icon-credit-card",
                    IsVisible = true,
                    RightURL = "#/pages/fees_types"
                },
                new Right()
                {
                    RightID = 6,
                    RightCode = "Years",
                    RightName = "Years",
                    FeatureID =3,
                    MenuIcon = "icon-calendar",
                    IsVisible = true,
                    RightURL = "#/pages/years"
                },
                new Right()
                {
                    RightID = 7,
                    RightCode = "Grade Fees",
                    RightName = "Grade Fees",
                    FeatureID = 3,
                    MenuIcon = "fa fa-money",
                    IsVisible = true,
                    RightURL = "#/pages/grade_fees"
                },
                new Right()
                {
                    RightID = 8,
                    RightCode = "Dashboard",
                    RightName = "Students",
                    FeatureID = 2,
                    MenuIcon = "icon-calendar",
                    IsVisible = true,
                    RightURL = "#/pages/students/:"+StudentStatusEnum.Student
                },
                new Right()
                {
                    RightID = 9,
                    RightCode = "New Applicant",
                    RightName = "New Applicant",
                    FeatureID = 1,
                    MenuIcon = "icon-docs",
                    IsVisible = true,
                    RightURL = "#/pages/new_applicant"
                },
                new Right()
                {
                    RightID = 10,
                    RightCode = "Upgrade Students",
                    RightName = "Upgrade Students",
                    FeatureID =2,
                    MenuIcon = "fa fa-level-up",
                    IsVisible = true,
                    RightURL = "#/pages/upgradeStudents"
                },
                new Right()
                {
                    RightID = 11,
                    RightCode = "Admission Application",
                    RightName = "Admission Application",
                    FeatureID = 1,
                    MenuIcon = "fa fa-pencil-square-o",
                    IsVisible = false,
                    RightURL = "#/pages/dashboard"
                },
                new Right()
                {
                    RightID = 12,
                    RightCode = "Student Voucher",
                    RightName = "Student Voucher",
                    FeatureID = 2,
                    MenuIcon = "fa fa-file-text-o",
                    IsVisible = true,
                    RightURL = "#/pages/issueVoucher"
                },
                new Right()
                {
                    RightID = 13,
                    RightCode = "TransferToStudent",
                    RightName = "Tranfer To Student",
                    FeatureID = 1,
                    MenuIcon = "fa fa-graduation-cap",
                    IsVisible = true,
                    RightURL = "#/pages/transferToStudent"
                },
                 new Right()
                {
                    RightID = 23,
                    RightCode = "Interviews",
                    RightName = "Interviews",
                    FeatureID = 1,
                    MenuIcon = "fa fa-graduation-cap",
                    IsVisible = true,
                    RightURL = "#/pages/InterViews"
                },
                new Right()
                {
                    RightID = 14,
                    RightCode = "Interview Reports",
                    RightName = "Interview Reports",
                    FeatureID = 4,
                    MenuIcon = "icon-docs",
                    IsVisible = true,
                    RightURL = "#/pages/interviewReports"
                }
                ,
                 new Right()
                {
                    RightID = 15,
                    RightCode = "Admission Reports",
                    RightName = "Admission Reports",
                    FeatureID = 4,
                    MenuIcon = "icon-docs",
                    IsVisible = true,
                    RightURL = "#/pages/admissionReports"
                },
              new Right()
                {
                    RightID = 15,
                    RightCode = "Discounts Types",
                    RightName = "Discounts Types",
                    FeatureID =3,
                    MenuIcon = "fa fa-tags",
                    IsVisible = true,
                    RightURL = "#/pages/discounts_types"
                },
                  new Right()
                {
                    RightID = 16,
                    RightCode = "Previous Schools Reports",
                    RightName = "Previous Schools Reports",
                    FeatureID = 4,
                    MenuIcon = "icon-docs",
                    IsVisible = true,
                    RightURL = "#/pages/PreviousSchoolsReports"
                },
                 new Right()
                {
                    RightID = 17,
                    RightCode = "UnPaid Fees Report",
                    RightName = "UnPaid Fees Report",
                    FeatureID = 4,
                    MenuIcon = "icon-docs",
                    IsVisible = true,
                    RightURL = "#/pages/UnPaidFeesReport"
                },

                 new Right()
                {
                    RightID = 18,
                    RightCode = "Paid Fees Report",
                    RightName = "Paid Fees Report",
                    FeatureID = 4,
                    MenuIcon = "icon-docs",
                    IsVisible = true,
                    RightURL = "#/pages/PaidFeesReport"
                } ,
                  new Right()
                {
                    RightID = 22,
                    RightCode = "Nationality",
                    RightName = "Nationality",
                    FeatureID = 3,
                    MenuIcon = "icon-docs",
                    IsVisible = true,
                    RightURL = "#/pages/Nationalities"
                }
                  ,
                 new Right()
                {
                    RightID = 60,
                    RightCode = "New Applicants Report",
                    RightName = "New Applicants Report",
                    FeatureID = 4,
                    MenuIcon = "icon-docs",
                    IsVisible = true,
                    RightURL = "#/pages/NewApplicantsReport"
                }
            };
            foreach (Right right in rights)
            {
                
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Rights] OFF");
                 
                context.Rights.AddOrUpdate(r => r.RightCode, right);
                context.SaveChanges();

                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Rights] ON");
            }
        }

        private static void PopulateRoles(Data.DBEntities context)
        {
            List<RoleRight> adminRoleRights = new List<RoleRight>()
            {
                new RoleRight() {RoleID = 1, RightID = 1, RoleRightID = 1},
                new RoleRight() {RoleID = 1, RightID = 2, RoleRightID = 2},
                new RoleRight() {RoleID = 1, RightID = 3, RoleRightID = 3},
                new RoleRight() {RoleID = 1, RightID = 4, RoleRightID = 4},
                new RoleRight() {RoleID = 1, RightID = 5, RoleRightID = 5},
                new RoleRight() {RoleID = 1, RightID = 6, RoleRightID = 6},
                new RoleRight() {RoleID = 1, RightID = 7, RoleRightID = 7},
                new RoleRight() {RoleID = 2, RightID = 8, RoleRightID = 8},
                new RoleRight() {RoleID = 1, RightID = 8, RoleRightID = 8},
                new RoleRight() {RoleID = 2, RightID = 9, RoleRightID = 9},
                new RoleRight() {RoleID = 2, RightID = 10, RoleRightID = 10},
                new RoleRight() {RoleID = 2, RightID = 11, RoleRightID = 11},
                new RoleRight() {RoleID = 2, RightID = 12, RoleRightID = 12},
                new RoleRight() {RoleID = 3, RightID = 13, RoleRightID = 13},
                new RoleRight() {RoleID = 1, RightID = 14, RoleRightID = 14},
                new RoleRight() {RoleID = 1, RightID = 15, RoleRightID = 15},
                //new RoleRight() {RoleID = 1, RightID = 16},
                //new RoleRight() {RoleID = 1, RightID = 17}
                //new RoleRight() {RoleID = 1, RightID = 15, RoleRightID = 15},
                //new RoleRight() {RoleID = 1, RightID = 16, RoleRightID = 16}
            };
            List<Role> roles = new List<Role>()
            {
                new Role() {RoleID = 1, RoleName = "Admin", IsSystemRole = true},
                new Role() {RoleID = 2, RoleName = "StudentAffairs", IsSystemRole = true},
                new Role() {RoleID = 3, RoleName = "Accountant", IsSystemRole = true}
            };
          
            foreach (Role role in roles)
            {
                context.Roles.AddOrUpdate(r => r.RoleName, role);
            }

            foreach (RoleRight roleRight in adminRoleRights)
            {

                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[RoleRights] OFF");

                context.RoleRights.AddOrUpdate(r => r.RoleRightID, roleRight);
                context.SaveChanges();

                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[RoleRights] ON");
            }

        }

        private static void PopulateGender(Data.DBEntities context)
        {
            List<Gender> genders = new List<Gender>()
            {
                new Gender() {GenderId = 1, GenderName = "Male"},
                new Gender() {GenderId = 2, GenderName = "Female"}

            };

            foreach (Gender gender in genders)
            {
                context.Genders.AddOrUpdate(x => x.GenderId, gender);

            }

        }

        private static void PopulateParentStatus(Data.DBEntities context)
        {
            List<ParentStatus> parentStatuses = new List<ParentStatus>()
            {
                new ParentStatus() {ParentStatusId = 1, ParentStatusName = "Married"},
                new ParentStatus() {ParentStatusId = 2, ParentStatusName = "Divorced"},
                new ParentStatus() {ParentStatusId = 3, ParentStatusName = "Separated"},
                new ParentStatus() {ParentStatusId = 4, ParentStatusName = "Other"}
            };

            foreach (ParentStatus parentStatus in parentStatuses)
            {
                context.ParentStatuses.AddOrUpdate(x => x.ParentStatusId, parentStatus);

            }

        }

        private static void PopulateCustdoy(Data.DBEntities context)
        {
            List<Custody> custodys = new List<Custody>()
            {
                new Custody() {CustodyId = 1, CustodyName = "Mother"},
                new Custody() {CustodyId = 2, CustodyName = "Father"},
                new Custody() {CustodyId = 3, CustodyName = "Both"},
                new Custody() {CustodyId = 4, CustodyName = "Other"},
            };

            foreach (Custody custody in custodys)
            {
                context.Custodys.AddOrUpdate(x => x.CustodyId, custody);

            }
        }

        private static void PopulateReligion(Data.DBEntities context)
        {
            List<Religion> religions = new List<Religion>()
            {
                new Religion() {ReligionId = 1, ReligionName = "Muslim"},
                new Religion() {ReligionId = 2, ReligionName = "Christian"},
            };

            foreach (Religion religion in religions)
            {
                context.Religions.AddOrUpdate(x => x.ReligionId, religion);

            }
        }

        private static void PopulateInterviewStatus(Data.DBEntities context)
        {
            List<InterviewStatus> interviewStatuses = new List<InterviewStatus>()
            {
                new InterviewStatus() {InterviewStatusId = 1, InterviewStatusName = "Accepted"},
                new InterviewStatus() {InterviewStatusId = 2, InterviewStatusName = "Pending/Retest"},
                new InterviewStatus() {InterviewStatusId = 3, InterviewStatusName = "Rejected"},
                new InterviewStatus() {InterviewStatusId = 4, InterviewStatusName = "Pending/No vacancy"},
                new InterviewStatus() {InterviewStatusId = 5, InterviewStatusName = "Pending/Others"},


            };

            foreach (InterviewStatus interviewStatus in interviewStatuses)
            {
                context.InterviewStatuses.AddOrUpdate(x => x.InterviewStatusName, interviewStatus);

            }
        }

        private static void PopulateNationality(Data.DBEntities context)
        {
            List<Nationality> nationalities = new List<Nationality>()
            {
                new Nationality() {NationalityId = 1, NationalityName = "Egyptian"},
                new Nationality() {NationalityId = 2, NationalityName = "British"},
                new Nationality() {NationalityId = 3, NationalityName = "American"},

            };

            foreach (Nationality nationality in nationalities)
            {
                context.Nationalities.AddOrUpdate(x => x.NationalityId, nationality);

            }
        }

        private static void PopulateVoucherType(Data.DBEntities context)
        {
            List<VoucherType> voucherTypes = new List<VoucherType>()
            {
                new VoucherType() {VoucherTypeId = 1,VoucherTypeName = "Issue"},
                new VoucherType() {VoucherTypeId = 2,VoucherTypeName = "Payment"},
                new VoucherType() {VoucherTypeId = 3,VoucherTypeName = "RefundRequest"},
                new VoucherType() {VoucherTypeId = 4,VoucherTypeName = "RefundDelivery"}


            };

            foreach (VoucherType v in voucherTypes)
            {
                context.VoucherTypes.AddOrUpdate(f => f.VoucherTypeId, v);

            }
        }

        private static void PopulateStudentStatus(Data.DBEntities context)
        {
            List<StudentStatus> studentStatuses = new List<StudentStatus>()
            {
                new StudentStatus() {StudentStatusId = 1, StudentStatusName = "ApplicationFees"},
                new StudentStatus() {StudentStatusId = 2, StudentStatusName = "ApplicationWithParent"},
                new StudentStatus() {StudentStatusId = 3, StudentStatusName = "ApplicationNotEntered"},
                new StudentStatus() {StudentStatusId = 4, StudentStatusName = "WaitingInterview"},
                new StudentStatus() {StudentStatusId = 5, StudentStatusName = "FormCompleted"},
                new StudentStatus() {StudentStatusId = 6, StudentStatusName = "Student"},
                new StudentStatus() {StudentStatusId = 7, StudentStatusName = "Rejected"},
                new StudentStatus() {StudentStatusId = 8, StudentStatusName = "Withdraw"},
            };
            foreach (StudentStatus studentStatus in studentStatuses)
            {
                context.StudentStatuses.AddOrUpdate(x => x.StudentStatusId, studentStatus);
            }

        }


        private static void PopulateFeesType(Data.DBEntities context)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {

                FeesType fee = new FeesType()
                {
                    FeeName = "Admission Fees",
                    FeesTypeId = 1,
                    IsMandatory = true,
                    IsBank = false,
                    IsPayOnce = true
                };
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[FeesTypes] OFF");

                context.FeesTypes.AddOrUpdate(f=>f.FeeName,fee);
                context.SaveChanges();

                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[FeesTypes] ON");

                transaction.Commit();
            }
        }

        private static void PopulateLangugae(Data.DBEntities context)
        {
            List<Language> Languages = new List<Language>()
            {
                new Language() {LanguageId = 1, LanguageName = "Arabic"},
                new Language() {LanguageId = 2, LanguageName = "English"},
                new Language() {LanguageId = 3, LanguageName = "Other"} 
            };
            foreach (Language Language in Languages)
            {
                context.Languages.AddOrUpdate(x => x.LanguageId, Language);
            }
        }

        private static void PopulateSettings(Data.DBEntities context)
        {
            List<Setting> Settings = new List<Setting>()
            {
                new Setting() {SettingKey = SettingsEnum.SchoolNameEN, SettingValue="Forsan International School"},
                new Setting() {SettingKey = SettingsEnum.SchoolNameAR, SettingValue= "مدرسة الفرسان الدولية" },
                new Setting() {SettingKey = SettingsEnum.LogoPath, SettingValue= "assets/layouts/layout/img/logo-forsan-report.png" }
            };
            foreach (Setting setting in Settings)
            {
                context.Settings.AddOrUpdate(x => x.SettingKey, setting);
            }
        }
    }
}
