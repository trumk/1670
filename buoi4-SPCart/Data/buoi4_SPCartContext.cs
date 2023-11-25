using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using buoi4_SPCart.Models;

namespace buoi4_SPCart.Data
{
    public class buoi4_SPCartContext : DbContext
    {
        public buoi4_SPCartContext (DbContextOptions<buoi4_SPCartContext> options)
            : base(options)
        {
        }

        public DbSet<buoi4_SPCart.Models.Product> Product { get; set; } = default!;
    }
}
