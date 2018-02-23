﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Data.Configuration
{
    public class NationalityContiguration:EntityTypeConfiguration<Nationality>
    {
        public NationalityContiguration()
        {
            ToTable("Nationalities");
        }
    }
}