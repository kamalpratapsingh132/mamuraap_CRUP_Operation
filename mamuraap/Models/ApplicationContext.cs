﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mamuraap.Models
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext
            (DbContextOptions<ApplicationContext> options) :base(options)  { }
       
        public DbSet<Employee> Employees { get; set; }


        public DbSet<Department>Departments { get; set; }
    }
}
