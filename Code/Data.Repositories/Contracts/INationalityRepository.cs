﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Model;

namespace Data.Repositories
{
    public interface INationalityRepository : IRepository<Nationality>
    {
        bool NationalityNameExists(string name, int id);
    }
}
