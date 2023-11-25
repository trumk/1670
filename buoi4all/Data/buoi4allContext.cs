using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using buoi4all.Models;

namespace buoi4all.Data
{
    public class buoi4allContext : DbContext
    {
        public buoi4allContext (DbContextOptions<buoi4allContext> options)
            : base(options)
        {
        }

        public DbSet<buoi4all.Models.Product> Product { get; set; } = default!;
    }
}
