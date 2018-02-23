namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlfursanMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.custodies",
                c => new
                    {
                        CustodyId = c.Int(nullable: false, identity: true),
                        CustodyName = c.String(),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CustodyId);
            
            CreateTable(
                "dbo.DiscountsTypes",
                c => new
                    {
                        DiscountsTypeID = c.Int(nullable: false, identity: true),
                        DiscountName = c.String(nullable: false),
                        DiscountPercentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DiscountsTypeID);
            
            CreateTable(
                "dbo.Emergencies",
                c => new
                    {
                        EmergencyId = c.Int(nullable: false, identity: true),
                        ContactName = c.String(),
                        Relationship = c.String(),
                        MobileNumber = c.String(),
                        StudentId = c.Int(nullable: false),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmergencyId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        ApplicationNo = c.String(),
                        ApplicationDate = c.DateTime(nullable: false),
                        SchoolId = c.Int(nullable: false),
                        GradeId = c.Int(),
                        FamilyName = c.String(),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        DateOfBirth = c.DateTime(),
                        PlaceOfBirth = c.String(),
                        NationalityId = c.Int(),
                        ReligionId = c.Int(),
                        GenderId = c.Int(),
                        PreviousSchool = c.String(),
                        FatherId = c.Int(),
                        MotherId = c.Int(),
                        LanguageSpokenAtHome = c.Boolean(),
                        GrandparentsLiveWithTheFamily = c.Boolean(),
                        ParentStatusId = c.Int(),
                        CustodyId = c.Int(),
                        StudentStatusId = c.Int(),
                        MedicalIssues = c.Boolean(),
                        RegularMedication = c.Boolean(),
                        CurrentMedication = c.String(),
                        Transportation = c.Boolean(),
                        InterviewDate = c.DateTime(),
                        InterviewStatusId = c.Int(),
                        InterviewComments = c.String(),
                        RegistrationFeesDate = c.DateTime(),
                        SchoolFeesDate = c.DateTime(),
                        Signature = c.String(),
                        StudentCode = c.String(),
                        IsSuspend = c.Boolean(nullable: false),
                        JoiningYearId = c.Int(),
                        FreeAdmission = c.Boolean(nullable: false),
                        DiscountsTypeID = c.Int(),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.custodies", t => t.CustodyId)
                .ForeignKey("dbo.DiscountsTypes", t => t.DiscountsTypeID)
                .ForeignKey("dbo.Parents", t => t.FatherId)
                .ForeignKey("dbo.Genders", t => t.GenderId)
                .ForeignKey("dbo.Grades", t => t.GradeId)
                .ForeignKey("dbo.InterviewStatus", t => t.InterviewStatusId)
                .ForeignKey("dbo.Years", t => t.JoiningYearId)
                .ForeignKey("dbo.Parents", t => t.MotherId)
                .ForeignKey("dbo.Nationalities", t => t.NationalityId)
                .ForeignKey("dbo.ParentStatus", t => t.ParentStatusId)
                .ForeignKey("dbo.Religions", t => t.ReligionId)
                .ForeignKey("dbo.Schools", t => t.SchoolId)
                .ForeignKey("dbo.StudentStatus", t => t.StudentStatusId)
                .Index(t => t.SchoolId)
                .Index(t => t.GradeId)
                .Index(t => t.NationalityId)
                .Index(t => t.ReligionId)
                .Index(t => t.GenderId)
                .Index(t => t.FatherId)
                .Index(t => t.MotherId)
                .Index(t => t.ParentStatusId)
                .Index(t => t.CustodyId)
                .Index(t => t.StudentStatusId)
                .Index(t => t.InterviewStatusId)
                .Index(t => t.JoiningYearId)
                .Index(t => t.DiscountsTypeID);
            
            CreateTable(
                "dbo.Parents",
                c => new
                    {
                        ParentId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Mobile = c.String(),
                        HomeNumber = c.String(),
                        Email = c.String(),
                        IdNumber = c.String(),
                        Occupation = c.String(),
                        Employer = c.String(),
                        School = c.String(),
                        University = c.String(),
                        HomeAddress = c.String(),
                        NationalityId = c.Int(),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ParentId)
                .ForeignKey("dbo.Nationalities", t => t.NationalityId)
                .Index(t => t.NationalityId);
            
            CreateTable(
                "dbo.Nationalities",
                c => new
                    {
                        NationalityId = c.Int(nullable: false, identity: true),
                        NationalityName = c.String(),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NationalityId);
            
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        GenderId = c.Int(nullable: false, identity: true),
                        GenderName = c.String(),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GenderId);
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        GradeId = c.Int(nullable: false, identity: true),
                        GradeName = c.String(nullable: false),
                        Order = c.Int(nullable: false),
                        SchoolId = c.Int(nullable: false),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GradeId)
                .ForeignKey("dbo.Schools", t => t.SchoolId)
                .Index(t => t.SchoolId);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        SchoolId = c.Int(nullable: false, identity: true),
                        SchoolName = c.String(nullable: false, maxLength: 150),
                        Prefix = c.Int(nullable: false),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SchoolId);
            
            CreateTable(
                "dbo.InterviewStatus",
                c => new
                    {
                        InterviewStatusId = c.Int(nullable: false, identity: true),
                        InterviewStatusName = c.String(),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InterviewStatusId);
            
            CreateTable(
                "dbo.Years",
                c => new
                    {
                        YearId = c.Int(nullable: false, identity: true),
                        YearName = c.String(),
                        ShortName = c.Int(nullable: false),
                        IsCurrent = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.YearId);
            
            CreateTable(
                "dbo.GradeFees",
                c => new
                    {
                        GradeFeesId = c.Int(nullable: false, identity: true),
                        YearId = c.Int(nullable: false),
                        JoiningYearId = c.Int(),
                        GradeId = c.Int(nullable: false),
                        FeesTypeId = c.Int(nullable: false),
                        Fee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                        JoiningYear_YearId = c.Int(),
                        Year_YearId = c.Int(),
                        Year_YearId1 = c.Int(),
                        Year_YearId2 = c.Int(),
                    })
                .PrimaryKey(t => t.GradeFeesId)
                .ForeignKey("dbo.FeesTypes", t => t.FeesTypeId)
                .ForeignKey("dbo.Grades", t => t.GradeId)
                .ForeignKey("dbo.Years", t => t.JoiningYear_YearId)
                .ForeignKey("dbo.Years", t => t.Year_YearId)
                .ForeignKey("dbo.Years", t => t.Year_YearId1)
                .ForeignKey("dbo.Years", t => t.Year_YearId2)
                .Index(t => t.GradeId)
                .Index(t => t.FeesTypeId)
                .Index(t => t.JoiningYear_YearId)
                .Index(t => t.Year_YearId)
                .Index(t => t.Year_YearId1)
                .Index(t => t.Year_YearId2);
            
            CreateTable(
                "dbo.FeesTypes",
                c => new
                    {
                        FeesTypeId = c.Int(nullable: false, identity: true),
                        FeeName = c.String(nullable: false),
                        IsMandatory = c.Boolean(nullable: false),
                        IsBank = c.Boolean(nullable: false),
                        IsPayOnce = c.Boolean(nullable: false),
                        AllowDiscounts = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FeesTypeId);
            
            CreateTable(
                "dbo.ParentStatus",
                c => new
                    {
                        ParentStatusId = c.Int(nullable: false, identity: true),
                        ParentStatusName = c.String(),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ParentStatusId);
            
            CreateTable(
                "dbo.Religions",
                c => new
                    {
                        ReligionId = c.Int(nullable: false, identity: true),
                        ReligionName = c.String(),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReligionId);
            
            CreateTable(
                "dbo.Siblings",
                c => new
                    {
                        SiblingId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        SiblingStudentId = c.Int(nullable: false),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SiblingId)
                .ForeignKey("dbo.Students", t => t.SiblingStudentId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.SiblingStudentId);
            
            CreateTable(
                "dbo.StudentLanguages",
                c => new
                    {
                        StudentLanguageId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentLanguageId)
                .ForeignKey("dbo.Languages", t => t.LanguageId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.LanguageId);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        LanguageId = c.Int(nullable: false, identity: true),
                        LanguageName = c.String(),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LanguageId);
            
            CreateTable(
                "dbo.StudentStatus",
                c => new
                    {
                        StudentStatusId = c.Int(nullable: false, identity: true),
                        StudentStatusName = c.String(),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentStatusId);
            
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        FeatureID = c.Int(nullable: false, identity: true),
                        FeatureName = c.String(),
                        FeatureNameAr = c.String(),
                        MenuIcon = c.String(),
                        IsVisible = c.Boolean(nullable: false),
                        FeatureOrder = c.Int(nullable: false),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FeatureID);
            
            CreateTable(
                "dbo.Rights",
                c => new
                    {
                        RightID = c.Int(nullable: false, identity: true),
                        RightCode = c.String(),
                        FeatureID = c.Int(nullable: false),
                        RightOrder = c.Int(nullable: false),
                        RightName = c.String(),
                        RightNameAr = c.String(),
                        MenuIcon = c.String(),
                        RightURL = c.String(),
                        IsVisible = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RightID)
                .ForeignKey("dbo.Features", t => t.FeatureID)
                .Index(t => t.FeatureID);
            
            CreateTable(
                "dbo.RoleRights",
                c => new
                    {
                        RoleRightID = c.Int(nullable: false, identity: true),
                        RoleID = c.Int(nullable: false),
                        RightID = c.Int(nullable: false),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoleRightID)
                .ForeignKey("dbo.Rights", t => t.RightID)
                .ForeignKey("dbo.Roles", t => t.RoleID)
                .Index(t => t.RoleID)
                .Index(t => t.RightID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        RoleNameAr = c.String(),
                        IsSystemRole = c.Boolean(),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        RoleID = c.Int(nullable: false),
                        SchoolId = c.Int(),
                        FullName = c.String(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 150),
                        UserEmail = c.String(maxLength: 255),
                        UserPassword = c.String(nullable: false, maxLength: 200),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Roles", t => t.RoleID)
                .ForeignKey("dbo.Schools", t => t.SchoolId)
                .Index(t => t.RoleID)
                .Index(t => t.SchoolId);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        SettingId = c.Int(nullable: false, identity: true),
                        SettingKey = c.String(),
                        SettingValue = c.String(),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SettingId);
            
            CreateTable(
                "dbo.StudentFinancials",
                c => new
                    {
                        StudentFinancialId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        StudentVoucherId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentDate = c.DateTime(),
                        Notes = c.String(),
                        VoucherTypeId = c.Int(nullable: false),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentFinancialId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .ForeignKey("dbo.StudentVouchers", t => t.StudentVoucherId)
                .ForeignKey("dbo.VoucherTypes", t => t.VoucherTypeId)
                .Index(t => t.StudentId)
                .Index(t => t.StudentVoucherId)
                .Index(t => t.VoucherTypeId);
            
            CreateTable(
                "dbo.StudentVouchers",
                c => new
                    {
                        StudentVoucherId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        YearId = c.Int(nullable: false),
                        VoucherDate = c.DateTime(),
                        PaymentDate = c.DateTime(),
                        IsPaid = c.Boolean(nullable: false),
                        Notes = c.String(),
                        TotalVoucher = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalVoucherRefund = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentVoucherId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .ForeignKey("dbo.Years", t => t.YearId)
                .Index(t => t.StudentId)
                .Index(t => t.YearId);
            
            CreateTable(
                "dbo.StudentVoucherDetails",
                c => new
                    {
                        StudentVoucherDetailsId = c.Int(nullable: false, identity: true),
                        StudentVoucherId = c.Int(nullable: false),
                        FeesTypeId = c.Int(nullable: false),
                        Fee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentVoucherDetailsId)
                .ForeignKey("dbo.FeesTypes", t => t.FeesTypeId)
                .ForeignKey("dbo.StudentVouchers", t => t.StudentVoucherId)
                .Index(t => t.StudentVoucherId)
                .Index(t => t.FeesTypeId);
            
            CreateTable(
                "dbo.StudentVoucherRefunds",
                c => new
                    {
                        StudentVoucherRefundId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        VoucherRefundDate = c.DateTime(),
                        PaymentDate = c.DateTime(),
                        IsPaid = c.Boolean(nullable: false),
                        TotalRefund = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StudentVoucherId = c.Int(nullable: false),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentVoucherRefundId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .ForeignKey("dbo.StudentVouchers", t => t.StudentVoucherId)
                .Index(t => t.StudentId)
                .Index(t => t.StudentVoucherId);
            
            CreateTable(
                "dbo.VoucherTypes",
                c => new
                    {
                        VoucherTypeId = c.Int(nullable: false, identity: true),
                        VoucherTypeName = c.String(),
                        CreationDate = c.DateTime(),
                        ModificationDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VoucherTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentFinancials", "VoucherTypeId", "dbo.VoucherTypes");
            DropForeignKey("dbo.StudentVouchers", "YearId", "dbo.Years");
            DropForeignKey("dbo.StudentVoucherRefunds", "StudentVoucherId", "dbo.StudentVouchers");
            DropForeignKey("dbo.StudentVoucherRefunds", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentVoucherDetails", "StudentVoucherId", "dbo.StudentVouchers");
            DropForeignKey("dbo.StudentVoucherDetails", "FeesTypeId", "dbo.FeesTypes");
            DropForeignKey("dbo.StudentFinancials", "StudentVoucherId", "dbo.StudentVouchers");
            DropForeignKey("dbo.StudentVouchers", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentFinancials", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Users", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.Users", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.RoleRights", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.RoleRights", "RightID", "dbo.Rights");
            DropForeignKey("dbo.Rights", "FeatureID", "dbo.Features");
            DropForeignKey("dbo.Students", "StudentStatusId", "dbo.StudentStatus");
            DropForeignKey("dbo.StudentLanguages", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentLanguages", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.Siblings", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Siblings", "SiblingStudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.Students", "ReligionId", "dbo.Religions");
            DropForeignKey("dbo.Students", "ParentStatusId", "dbo.ParentStatus");
            DropForeignKey("dbo.Students", "NationalityId", "dbo.Nationalities");
            DropForeignKey("dbo.Students", "MotherId", "dbo.Parents");
            DropForeignKey("dbo.Students", "JoiningYearId", "dbo.Years");
            DropForeignKey("dbo.GradeFees", "Year_YearId2", "dbo.Years");
            DropForeignKey("dbo.GradeFees", "Year_YearId1", "dbo.Years");
            DropForeignKey("dbo.GradeFees", "Year_YearId", "dbo.Years");
            DropForeignKey("dbo.GradeFees", "JoiningYear_YearId", "dbo.Years");
            DropForeignKey("dbo.GradeFees", "GradeId", "dbo.Grades");
            DropForeignKey("dbo.GradeFees", "FeesTypeId", "dbo.FeesTypes");
            DropForeignKey("dbo.Students", "InterviewStatusId", "dbo.InterviewStatus");
            DropForeignKey("dbo.Students", "GradeId", "dbo.Grades");
            DropForeignKey("dbo.Grades", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.Students", "GenderId", "dbo.Genders");
            DropForeignKey("dbo.Students", "FatherId", "dbo.Parents");
            DropForeignKey("dbo.Parents", "NationalityId", "dbo.Nationalities");
            DropForeignKey("dbo.Emergencies", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "DiscountsTypeID", "dbo.DiscountsTypes");
            DropForeignKey("dbo.Students", "CustodyId", "dbo.custodies");
            DropIndex("dbo.StudentVoucherRefunds", new[] { "StudentVoucherId" });
            DropIndex("dbo.StudentVoucherRefunds", new[] { "StudentId" });
            DropIndex("dbo.StudentVoucherDetails", new[] { "FeesTypeId" });
            DropIndex("dbo.StudentVoucherDetails", new[] { "StudentVoucherId" });
            DropIndex("dbo.StudentVouchers", new[] { "YearId" });
            DropIndex("dbo.StudentVouchers", new[] { "StudentId" });
            DropIndex("dbo.StudentFinancials", new[] { "VoucherTypeId" });
            DropIndex("dbo.StudentFinancials", new[] { "StudentVoucherId" });
            DropIndex("dbo.StudentFinancials", new[] { "StudentId" });
            DropIndex("dbo.Users", new[] { "SchoolId" });
            DropIndex("dbo.Users", new[] { "RoleID" });
            DropIndex("dbo.RoleRights", new[] { "RightID" });
            DropIndex("dbo.RoleRights", new[] { "RoleID" });
            DropIndex("dbo.Rights", new[] { "FeatureID" });
            DropIndex("dbo.StudentLanguages", new[] { "LanguageId" });
            DropIndex("dbo.StudentLanguages", new[] { "StudentId" });
            DropIndex("dbo.Siblings", new[] { "SiblingStudentId" });
            DropIndex("dbo.Siblings", new[] { "StudentId" });
            DropIndex("dbo.GradeFees", new[] { "Year_YearId2" });
            DropIndex("dbo.GradeFees", new[] { "Year_YearId1" });
            DropIndex("dbo.GradeFees", new[] { "Year_YearId" });
            DropIndex("dbo.GradeFees", new[] { "JoiningYear_YearId" });
            DropIndex("dbo.GradeFees", new[] { "FeesTypeId" });
            DropIndex("dbo.GradeFees", new[] { "GradeId" });
            DropIndex("dbo.Grades", new[] { "SchoolId" });
            DropIndex("dbo.Parents", new[] { "NationalityId" });
            DropIndex("dbo.Students", new[] { "DiscountsTypeID" });
            DropIndex("dbo.Students", new[] { "JoiningYearId" });
            DropIndex("dbo.Students", new[] { "InterviewStatusId" });
            DropIndex("dbo.Students", new[] { "StudentStatusId" });
            DropIndex("dbo.Students", new[] { "CustodyId" });
            DropIndex("dbo.Students", new[] { "ParentStatusId" });
            DropIndex("dbo.Students", new[] { "MotherId" });
            DropIndex("dbo.Students", new[] { "FatherId" });
            DropIndex("dbo.Students", new[] { "GenderId" });
            DropIndex("dbo.Students", new[] { "ReligionId" });
            DropIndex("dbo.Students", new[] { "NationalityId" });
            DropIndex("dbo.Students", new[] { "GradeId" });
            DropIndex("dbo.Students", new[] { "SchoolId" });
            DropIndex("dbo.Emergencies", new[] { "StudentId" });
            DropTable("dbo.VoucherTypes");
            DropTable("dbo.StudentVoucherRefunds");
            DropTable("dbo.StudentVoucherDetails");
            DropTable("dbo.StudentVouchers");
            DropTable("dbo.StudentFinancials");
            DropTable("dbo.Settings");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.RoleRights");
            DropTable("dbo.Rights");
            DropTable("dbo.Features");
            DropTable("dbo.StudentStatus");
            DropTable("dbo.Languages");
            DropTable("dbo.StudentLanguages");
            DropTable("dbo.Siblings");
            DropTable("dbo.Religions");
            DropTable("dbo.ParentStatus");
            DropTable("dbo.FeesTypes");
            DropTable("dbo.GradeFees");
            DropTable("dbo.Years");
            DropTable("dbo.InterviewStatus");
            DropTable("dbo.Schools");
            DropTable("dbo.Grades");
            DropTable("dbo.Genders");
            DropTable("dbo.Nationalities");
            DropTable("dbo.Parents");
            DropTable("dbo.Students");
            DropTable("dbo.Emergencies");
            DropTable("dbo.DiscountsTypes");
            DropTable("dbo.custodies");
        }
    }
}
