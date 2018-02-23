using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ElSherkaSeedData : DropCreateDatabaseIfModelChanges<DBEntities>
    {
        protected override void Seed(DBEntities context)
        {
            context.Commit();
        }


    }
}
