﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class ReportAdmissionDTO
    {
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }
        public int SchoolId { get; set; }
    }
}
