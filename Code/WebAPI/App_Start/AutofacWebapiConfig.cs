using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using Data;
using Data.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Model;
using Service;
using Service.Contracts;
using Services;
using Data.Repositories;

namespace WebAPI
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // EF HomeCinemaContext
            builder.RegisterType<DBEntities>()
                   .As<DbContext>()
                   .InstancePerRequest();

            builder.RegisterType<DbFactory>()
                .As<IDbFactory>()
                .InstancePerRequest();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerRequest();

            builder.RegisterGeneric(typeof(BaseRepository<>))
                   .InstancePerRequest();

            // Repository Factory
            builder.RegisterType<Data.Repositories.GradeRepository>()
                .As<Data.Repositories.IGradeRepository>().InstancePerRequest();
            builder.RegisterType<Data.Repositories.UserRepository>()
                .As<Data.Repositories.IUserRepository>()
                .InstancePerRequest();

            builder.RegisterType<Data.Repositories.RoleRightRepository>()
                .As<Data.Repositories.IRoleRightRepository>().InstancePerRequest();

            builder.RegisterType<Data.Repositories.RoleRepository>()
                .As<Data.Repositories.IRoleRepository>().InstancePerRequest();

            builder.RegisterType<Data.Repositories.UserRepository>()
    .As<Data.Repositories.IUserRepository>().InstancePerRequest();

            builder.RegisterType<Data.Repositories.SchoolRepository>()
                .As<Data.Repositories.ISchoolRepository>()
                .InstancePerRequest();

            builder.RegisterType<Data.Repositories.FeesTypeRepository>()
                .As<Data.Repositories.IFeesTypeRepository>()
                .InstancePerRequest();

            builder.RegisterType<Data.Repositories.GenderRepository>()
                .As<Data.Repositories.IGenderRepository>()
                .InstancePerRequest();


            builder.RegisterType<Data.Repositories.InterviewStatusRepository>()
                .As<Data.Repositories.IInterviewStatusRepository>()
                .InstancePerRequest();

            builder.RegisterType<Data.Repositories.ParentStatusRepository>()
                .As<Data.Repositories.IParentStatusRepository>()
                .InstancePerRequest();

            builder.RegisterType<Data.Repositories.ReligionRepository>()
                .As<Data.Repositories.IReligionRepository>()
                .InstancePerRequest();

            builder.RegisterType<Data.Repositories.StudentStatusRepository>()
                .As<Data.Repositories.IStudentStatusRepository>()
                .InstancePerRequest();

            builder.RegisterType<Data.Repositories.ParentRepository>()
                .As<Data.Repositories.IParentRepository>()
                .InstancePerRequest();

            builder.RegisterType<Data.Repositories.EmergencyRepository>()
                .As<Data.Repositories.IEmergencyRepository>()
                .InstancePerRequest();

            builder.RegisterType<Data.Repositories.StudentRepository>()
                .As<Data.Repositories.IStudentRepository>()
                .InstancePerRequest();

            builder.RegisterType<Data.Repositories.SiblingRepository>()
                .As<Data.Repositories.ISiblingRepository>()
                .InstancePerRequest();

            builder.RegisterType<Data.Repositories.CustodyRepository>()
                .As<Data.Repositories.ICustodyRepository>()
                .InstancePerRequest();

            builder.RegisterType<Data.Repositories.NationalityRepository>()
                .As<Data.Repositories.INationalityRepository>()
                .InstancePerRequest();
            builder.RegisterType<Data.Repositories.YearRepository>()
                .As<Data.Repositories.IYearRepository>()
                .InstancePerRequest();
            builder.RegisterType<Data.Repositories.VoucherTypeRepository>()
                .As<Data.Repositories.IVoucherTypeRepository>()
                .InstancePerRequest();
            builder.RegisterType<Data.Repositories.StudentVoucherRepository>()
                .As<Data.Repositories.IStudentVoucherRepository>()
                .InstancePerRequest();
            builder.RegisterType<Data.Repositories.StudentVoucherDetailsRepository>()
                .As<Data.Repositories.IStudentVoucherDetailsRepository>()
                .InstancePerRequest();
            builder.RegisterType<Data.Repositories.StudentFinancialRepository>()
                .As<Data.Repositories.IStudentFinancialRepository>()
                .InstancePerRequest();

            builder.RegisterType<Data.Repositories.StudentVoucherRefundRepository>()
                .As<Data.Repositories.IStudentVoucherRefundRepository>()
                .InstancePerRequest();

            builder.RegisterType<Data.Repositories.SettingRepository>()
            .As<Data.Repositories.ISettingRepository>()
            .InstancePerRequest();

            builder.RegisterType<Data.Repositories.DiscountsTypeRepository>()
            .As<Data.Repositories.IDiscountsTypeRepository>()
            .InstancePerRequest();

            //services
            builder.RegisterType<Services.UserService>()
                .As<Services.IUserService>()
                .InstancePerRequest();

            builder.RegisterType<Services.RoleRightService>()
                .As<Services.IRoleRightService>()
                .InstancePerRequest();

            builder.RegisterType<Services.RoleService>()
                .As<Services.IRoleService>()
                .InstancePerRequest();

            builder.RegisterType<Services.SchoolService>()
                .As<Services.ISchoolService>()
                .InstancePerRequest();
            builder.RegisterType<GradeService>()
                .As<IGradeService>()
                .InstancePerRequest();

            builder.RegisterType<Services.FeesTypeService>()
                .As<Services.IFeesTypeService>()
                .InstancePerRequest();

            builder.RegisterType<Services.GenderService>()
                .As<Services.IGenderService>()
                .InstancePerRequest();

            builder.RegisterType<Services.InterviewStatuService>()
                .As<Services.IInterviewStatusService>()
                .InstancePerRequest();

            builder.RegisterType<Services.ParentStatusService>()
                .As<Services.IParentStatusService>()
                .InstancePerRequest();

            builder.RegisterType<Services.ReligionService>()
                .As<Services.IReligionService>()
                .InstancePerRequest();

            builder.RegisterType<Services.StudentStatusService>()
                .As<Services.IStudentStatusService>()
                .InstancePerRequest();

            builder.RegisterType<Services.ParentService>()
                .As<Services.IParentService>()
                .InstancePerRequest();

            builder.RegisterType<Services.EmergencyService>()
                .As<Services.IEmergencyService>()
                .InstancePerRequest();

            builder.RegisterType<Services.StudentService>()
                .As<Services.IStudentService>()
                .InstancePerRequest();

            builder.RegisterType<Services.SiblingService>()
                .As<Services.ISiblingService>()
                .InstancePerRequest();

            builder.RegisterType<Services.CustodyService>()
                .As<Services.ICustodyService>()
                .InstancePerRequest();

            builder.RegisterType<Services.NationalityService>()
                .As<Services.INationalityService>()
                .InstancePerRequest();
            builder.RegisterType<YearService>()
                .As<IYearService>()
                .InstancePerRequest();

            builder.RegisterType<VoucherTypeService>()
                .As<IVoucherTypeService>()
                .InstancePerRequest();

            builder.RegisterType<StudentVoucherService>()
                .As<IStudentvoucherService>()
                .InstancePerRequest();

            builder.RegisterType<StudentVoucherDetailsService>()
                .As<IStudentVoucherDetailsService>()
                .InstancePerRequest();

            builder.RegisterType<StudentFinancialService>()
                .As<IStudentFinancialService>()
                .InstancePerRequest();

            builder.RegisterType<StudentVoucherRefundService>()
                .As<IStudentVoucherRefundService>()
                .InstancePerRequest();

            builder.RegisterType<SettingService>()
                .As<ISettingService>()
                .InstancePerRequest();

            builder.RegisterType<DiscountsTypeService>()
                .As<IDiscountsTypeService>()
                .InstancePerRequest();

            builder.RegisterType<LanguageService>()
           .As<ILanguageService>()
           .InstancePerRequest();

            builder.RegisterType<LanguageRepository>()
             .As<ILanguageRepository>()
             .InstancePerRequest();
 

            builder.RegisterType<StudentLanguageRepository>()
             .As<IStudentLanguageRepository>()
             .InstancePerRequest();

            Container = builder.Build();

            return Container;
        }
    }
}