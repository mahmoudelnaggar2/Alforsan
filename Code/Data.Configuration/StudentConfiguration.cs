using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Data.Configuration
{
    public class StudentConfiguration: EntityTypeConfiguration<Student>
    {
        public StudentConfiguration()
        {
            ToTable("Students");
            Property(s => s.ApplicationDate).IsRequired();
            //Property(s => s.ApplicantName).IsRequired();
            
            HasOptional(s => s.Father).WithMany(p => p.StudentsFather).HasForeignKey(s => s.FatherId);
            HasOptional(s => s.Mother).WithMany(p => p.StudentsMother).HasForeignKey(s => s.MotherId);

            HasOptional(s => s.JoiningYear).WithMany(p => p.Students).HasForeignKey(s => s.JoiningYearId);

            Ignore(s => s.DateTimeFrom);
            Ignore(s => s.DateTimeTo);
            Ignore(s => s.Balance);
            Ignore(s => s.ApplicantName);
            Ignore(s => s.CheckedInterviewStatus);
            Ignore(s => s.StudentStatusDTO);
              
        }
    }
}
