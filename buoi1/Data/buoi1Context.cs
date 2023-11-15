using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using buoi1.Models;

namespace buoi1.Data
{
    public class buoi1Context : DbContext
    {
        public buoi1Context (DbContextOptions<buoi1Context> options)
            : base(options)
        {
        }

        public DbSet<buoi1.Models.Ticket> Ticket { get; set; } = default!;
    }
}
