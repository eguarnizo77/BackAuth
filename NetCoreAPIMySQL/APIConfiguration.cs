﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackAuth.Data
{
    public class APIConfiguration
    {
        public APIConfiguration(string connectionString) => ConnectionString = connectionString;
    
        public string ConnectionString { get; set; }
    }
}
   

