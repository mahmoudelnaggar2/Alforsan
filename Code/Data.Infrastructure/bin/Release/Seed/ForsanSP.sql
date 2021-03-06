/****** Object:  StoredProcedure [dbo].[SP_GetEmergencyByStudentId]    Script Date: 29/08/2017 03:25:29 Pm ******/
CREATE proc [dbo].[SP_GetSchoolName] @schoolId int 
as
begin 

select SchoolName from [dbo].[Schools] where SchoolId=@schoolId

end

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_GetEmergencyByStudentId] @StudentID int
as
begin
select em.ContactName,em.Relationship,em.MobileNumber
from [dbo].[Students] as std inner join [dbo].[Emergencies] as em on em.StudentId=std.StudentId 
where std.StudentId= @StudentID
end

GO
/****** Object:  StoredProcedure [dbo].[SP_GetSibilngByStudentId]    Script Date: 29/08/2017 03:25:29 Pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_GetSibilngByStudentId] @StudentID int
as
begin
select std1.StudentId ,std1.FirstName+' '+std1.MiddleName+' '+std1.FamilyName[Full Name],sch.SchoolName,grade.GradeName
from [dbo].[Students] as std inner join [dbo].[Siblings] as sib on sib.StudentId=std.StudentId inner join [dbo].[Students] as std1 on sib.SiblingStudentId=std1.StudentId
inner join [dbo].[Schools] as sch on sch.SchoolId=std1.SchoolId
inner join [dbo].[Grades] as grade on grade.GradeId=std1.GradeId
where std.StudentId= @StudentID
end

GO
/****** Object:  StoredProcedure [dbo].[SP_InterviewByDateRange]    Script Date: 29/08/2017 03:25:29 Pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_InterviewByDateRange] @FromDate DateTime,@ToDate DateTime,@SchoolId int,@GradeId int
as
begin
select std.StudentId,std.FirstName+' '+std.MiddleName+' '+std.FamilyName[Full Name],grade.GradeName,fath.Mobile,convert(varchar(12),std.InterviewDate,103)[Date],convert(varchar(8),std.InterviewDate,108)[time],sch.SchoolName
from [dbo].[Students] as std
inner join [dbo].[Grades] as grade on grade.GradeId=std.GradeId
inner join [dbo].[Parents] as fath on fath.ParentId=std.FatherId 
inner join [dbo].[Schools] as sch on sch.SchoolId=std.SchoolId
where std.SchoolId=@SchoolId and std.GradeId=@GradeId and (convert(varchar(12),std.InterviewDate,103) >= convert(varchar(12),@FromDate,103) and convert(varchar(12),std.InterviewDate,103) <= convert(varchar(12),@ToDate,103))
end
GO
/****** Object:  StoredProcedure [dbo].[SP_StudentPrintOut]    Script Date: 29/08/2017 03:25:29 Pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_StudentPrintOut] @StudentID int
as
begin
select std.FirstName+' '+std.MiddleName+' '+std.FamilyName[Full Name],std.StudentCode,sch.SchoolName,grade.GradeName,std.ApplicationDate,std.ApplicationNo,nat.NationalityName,std.DateOfBirth,std.PlaceOfBirth,rel.ReligionName,gen.GenderName,std.PreviousSchool,
fath.Name[FatherName],fath.Mobile[FatherMobile],fath.HomeNumber[FatherHomeNo],fath.Email[FatherEmail],fath.Occupation[father occupation],fath.Employer[father employee],fath.IdNumber,fath.School,fath.University,fath.HomeAddress,natfath.NationalityName[Father nationality],
moth.Name[Mother Name],moth.Mobile[Mother Mobile],moth.HomeNumber[MotherHome Number],moth.Email[Mother Email],moth.Occupation[Mother occupation],moth.Employer,moth.IdNumber[Mother Id],moth.School[Mother School],moth.University[Mother university],moth.HomeAddress[Mother Address],natmoth.NationalityName[Mother nationality],
(Select langs.[LanguageName] + ',' AS [text()]
                From [dbo].[StudentLanguages] stLang join [dbo].[Languages] langs
				on stLang.[LanguageId] =langs.[LanguageId]
				where stLang.[StudentId]=std.StudentId
				  For XML PATH ('')
				 )[Languagh],
(case when std.GrandparentsLiveWithTheFamily='true' then 'Yes'
when std.GrandparentsLiveWithTheFamily='false' then 'No'
end)[Grand parents Live With Family],
parSt.ParentStatusName,
(case when std.MedicalIssues='true' then 'Yes'
when std.MedicalIssues='false' then 'No'
end)[Medical Issues],
(case when std.RegularMedication='true' then 'Yes'
when std.RegularMedication='false' then 'No'
end)[Regular Medication],
std.CurrentMedication,
(case when std.Transportation='true' then 'Yes'
when std.Transportation='false' then 'No'
end)[Transportation Service],
inter.InterviewStatusName,std.InterviewComments,std.InterviewDate

from [dbo].[Students] as std 
inner join [dbo].[Schools] as sch on sch.SchoolId=std.SchoolId
inner join [dbo].[Grades] as grade on grade.GradeId=std.GradeId
inner join [dbo].[Nationalities] as nat on nat.NationalityId=std.NationalityId
inner join [dbo].[Religions] as rel on rel.ReligionId=std.ReligionId
inner join [dbo].[Genders] as gen on gen.GenderId=std.GenderId
inner join [dbo].[Parents] as fath on fath.ParentId=std.FatherId inner join [dbo].[Nationalities] as natfath on natfath.NationalityId=fath.NationalityId
inner join [dbo].[Parents] as moth on moth.ParentId=std.MotherId inner join [dbo].[Nationalities] as natMoth on natMoth.NationalityId=moth.NationalityId
inner join [dbo].[ParentStatus] as parSt on parSt.ParentStatusId=std.ParentStatusId 
inner join [dbo].[InterviewStatus]as inter on inter.InterviewStatusId=std.InterviewStatusId 
where std.StudentId= @StudentID
end

GO 

/****** Object:  StoredProcedure [dbo].[GetAdmission]    Script Date: 25/09/2017 03:07:40 Pm ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[GetAdmission] @Datefrom dateTime ,@DateTo dateTime,@schoolId int
as
begin
if(@schoolId=0)
select @schoolId=null

select Students.ApplicationDate,Students.ApplicationNo,(Students.FirstName+' '+Students.MiddleName+' '+Students.FamilyName)[Applicant Name],Schools.SchoolName
from [dbo].[Students]
inner join  [dbo].[Schools] on Schools.SchoolId=Students.SchoolId
where Students.SchoolId=ISNULL(@schoolId,Students.SchoolId) and (convert(varchar(12),Students.ApplicationDate,103) >= convert(varchar(12), @Datefrom,103)) and (convert(varchar(12),Students.ApplicationDate,103) <= convert(varchar(12), @DateTo,103))

end
GO
/****** Object:  StoredProcedure [dbo].[SP_SchoolPaidFees]    Script Date: 12/17/2017 5:46:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure SP_SchoolPaidFees
 @DateTo datetime  as
select sh.SchoolName, f.FeeName , ISNULL(n.TotalPayment, 0) as TotalAmount 
from Schools sh
cross join FeesTypes f

left join(
select f.FeeName ,s.SchoolId,Sum(v.TotalVoucher) as TotalPayment 
from FeesTypes f
left join StudentVoucherDetails d on f.FeesTypeId = d.FeesTypeId
left join StudentVouchers v on d.StudentVoucherId = v.StudentVoucherId
left join StudentFinancials sf on sf.StudentVoucherId = d.StudentVoucherId
left  join Students st on st.StudentId = sf.StudentId
left join Schools s on s.SchoolId = st.SchoolId

where v.IsPaid =1 and sf.Amount >0and cast(v.PaymentDate as date) <= cast( @DateTo as date)
group by f.FeeName, s.SchoolId 
) as n on n.FeeName = f.FeeName and n.SchoolId = sh.SchoolId 

UNION

select sh.SchoolName ,'Discount',CAST(((SUM((v.TotalVoucher*(ISNULL(d.DiscountPercentage,0)/100)))))as numeric(36,2)) as TotalAmount 
from StudentVouchers v
left join Students st on st.StudentId = v.StudentId
left join DiscountsTypes d on d.DiscountsTypeID = st.DiscountsTypeID
left join Schools sh on sh.SchoolID = st.SchoolID
group by sh.SchoolName


GO
/****** Object:  StoredProcedure [dbo].[SP_UnPaidFeesStudentCount]    Script Date: 12/17/2017 5:09:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--if object_id('dbo.SP_UnPaidFeesStudentCount', 'p') is null
--    exec ('create procedure SP_UnPaidFeesStudentCount as select 1')
--go
--alter procedure dbo.SP_UnPaidFeesStudentCount as
--SET NOCOUNT ON
create procedure [dbo].[SP_UnPaidFeesStudentCount] as
begin
select sh.SchoolName, f.FeeName ,SUM( ISNULL(n.Students, 0)) as NumberOfStudents
from Schools sh
cross join FeesTypes f
left join(
select f.FeeName ,s.SchoolId, COUNT(DISTINCT st.StudentId) as Students
from FeesTypes f
left join StudentVoucherDetails d on f.FeesTypeId = d.FeesTypeId
left join StudentVouchers v on d.StudentVoucherId = v.StudentVoucherId
left join StudentFinancials sf on sf.StudentVoucherId = d.StudentVoucherId
left  join Students st on st.StudentId = sf.StudentId
left join Schools s on s.SchoolId = st.SchoolId
where v.IsPaid =0 
group by f.FeeName, s.SchoolId,st.StudentId 
)as n on n.FeeName = f.FeeName and n.SchoolId = sh.SchoolId
group by  f.FeeName , sh.SchoolName 
end

GO
/****** Object:  StoredProcedure [dbo].[SP_GetVoucherReportByID]    Script Date: 9/24/2017 10:19:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_GetVoucherReportByID] @StudentVoucherID int
as
begin
select sv.StudentVoucherId,svd.StudentVoucherDetailsId ,s.FirstName,s.MiddleName, s.FamilyName,sch.SchoolName, G.GradeName ,PaymentDate , VoucherDate ,sv.TotalVoucher,
 svd.Fee ,FT.FeeName ,sv.IsPaid ,sv.TotalVoucherRefund , y.YearName
from StudentVouchers as sv 
 join StudentVoucherDetails as svd on sv.StudentVoucherId = svd.StudentVoucherId 
 join FeesTypes as FT on svd.FeesTypeId = FT.FeesTypeId
 join Students as s on sv.StudentId = s.StudentId
 join Years as y on sv.YearId = y.YearId
 join Schools as sch on s.SchoolId = sch.SchoolId
 join Grades as G on s.GradeId = G.GradeId
 where sv.StudentVoucherId = @StudentVoucherID
end
/****** Object:  StoredProcedure [dbo].[SP_GetStudentsPreviousSchoolsBySchoolID]    Script Date: 12/17/2017 5:12:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--if object_id('dbo.SP_GetStudentsPreviousSchoolsBySchoolID ', 'p') is null
--    exec ('create procedure SP_GetStudentsPreviousSchoolsBySchoolID  as select 1')
--go
--alter procedure dbo.SP_GetStudentsPreviousSchoolsBySchoolID 
----SET NOCOUNT ON 
Create procedure dbo.SP_GetStudentsPreviousSchoolsBySchoolID 
   @SchoolID int as
   begin
select st.StudentId,st.FirstName+' '+st.MiddleName+' '+st.FamilyName as FullName, st.ApplicationNo, YEAR(st.ApplicationDate) as AcademicYear, st.PreviousSchool, s.SchoolName
from Students st
left join Schools s on st.SchoolId = s.SchoolId
where PreviousSchool IS NOT NULL and s.SchoolId = @SchoolID
Order by st.StudentId
end
