using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Data.Configuration
{
   public class SiblingConfiguration:EntityTypeConfiguration<Sibling>
    {
        public SiblingConfiguration()
        {
            ToTable("Siblings");
            HasRequired(s => s.SiblingStudent).WithMany(p => p.StudentSiblings).HasForeignKey(s => s.SiblingStudentId);
            HasRequired(s => s.Student).WithMany(s => s.Siblings).HasForeignKey(s => s.StudentId);
        }
    }
}
