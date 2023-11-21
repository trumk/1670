using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using buoi22.Models;

namespace buoi22.Data
{
    public class buoi22Context : DbContext
    {
        public buoi22Context (DbContextOptions<buoi22Context> options)
            : base(options)
        {
        }

        public DbSet<buoi22.Models.Employee> Employee { get; set; } = default!;
    }
}
