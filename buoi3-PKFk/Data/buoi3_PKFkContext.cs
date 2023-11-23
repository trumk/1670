using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using buoi3_PKFk.Models;

namespace buoi3_PKFk.Data
{
    public class buoi3_PKFkContext : DbContext
    {
        public buoi3_PKFkContext (DbContextOptions<buoi3_PKFkContext> options)
            : base(options)
        {
        }

        public DbSet<buoi3_PKFk.Models.Movie> Movie { get; set; } = default!;
    }
}
