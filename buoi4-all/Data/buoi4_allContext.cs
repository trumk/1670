using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using buoi4_all.Models;

namespace buoi4_all.Data
{
    public class buoi4_allContext : DbContext
    {
        public buoi4_allContext (DbContextOptions<buoi4_allContext> options)
            : base(options)
        {
        }

        public DbSet<buoi4_all.Models.Product> Product { get; set; } = default!;
    }
}
