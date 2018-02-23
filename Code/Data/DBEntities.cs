
using Data.Configuration;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DBEntities : DbContext
    {
        public DBEntities() : base("DBEntities") {
            this.Configuration.LazyLoadingEnabled = false;

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DBEntities, Migrations.Configuration>("DBEntities"));

        }

        public DbSet<Feature> Features { get; set; }
        public DbSet<Right> Rights { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleRight> RoleRights { get; set; }
     
        public DbSet<User> Users { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<FeesType> FeesTypes { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Sibling> Siblings { get; set; }
        public DbSet<Emergency> Emergencies { get; set; }
        public DbSet<InterviewStatus> InterviewStatuses { get; set; }
        public DbSet<StudentStatus> StudentStatuses { get; set; }
        public DbSet<ParentStatus> ParentStatuses { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<Religion> Religions { get; set; }
        public DbSet<Custody> Custodys { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<GradeFees> GradeFees { get; set; }
        public DbSet<VoucherType> VoucherTypes { get; set; }
        public DbSet<StudentVoucher> StudentVouchers { get; set; }
        public DbSet<StudentVoucherDetails> StudentVoucherDetails { get; set; }
        public DbSet<StudentFinancial> StudentFinancials { get; set; }
        public DbSet<StudentVoucherRefund> StudentVoucherRefunds { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<DiscountsType> DiscountsTypes { get; set; }

        public DbSet<Language> Languages { get; set; }

        
        public void Commit()
        {
            base.SaveChanges();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new SchoolConfiguration());
            modelBuilder.Configurations.Add(new FeesTypeConfiguration());
            modelBuilder.Configurations.Add(new GradeConfiguration());
            modelBuilder.Configurations.Add(new YearConfiguration());
            modelBuilder.Configurations.Add(new StudentConfiguration());
            modelBuilder.Configurations.Add(new ParentConfiguration());
            modelBuilder.Configurations.Add(new GenderConfiguration());
            modelBuilder.Configurations.Add(new ReligionConfiguration());
            modelBuilder.Configurations.Add(new NationalityContiguration());
            modelBuilder.Configurations.Add(new InterviewStatusConfiguration());
            modelBuilder.Configurations.Add(new StudentStatusConfiguration());
            modelBuilder.Configurations.Add(new SiblingConfiguration());
            modelBuilder.Configurations.Add(new EmergencyConfiguration());
            modelBuilder.Configurations.Add(new ParentStatusConfiguration());
            modelBuilder.Configurations.Add(new CustodyConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());            
            modelBuilder.Configurations.Add(new VoucherTypeConfiguration());
            modelBuilder.Configurations.Add(new StudentVoucherConfiguration());
            modelBuilder.Configurations.Add(new StudetFinancialConfiguration());
            modelBuilder.Configurations.Add(new StudentVoucherDetailsConfiguration());
            modelBuilder.Configurations.Add(new StudentVoucherRefundConfiguration());
            modelBuilder.Configurations.Add(new SettingConfiguration());
            modelBuilder.Configurations.Add(new DiscountsTypeConfiguration());

            modelBuilder.Configurations.Add(new StudentLanguageConfiguration());
            modelBuilder.Configurations.Add(new LanguageConfiguration());

             
        }
    }
}
