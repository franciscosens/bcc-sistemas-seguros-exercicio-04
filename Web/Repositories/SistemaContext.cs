using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Repositories
{
    public class SistemaContext : DbContext
    {
        public SistemaContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ArquivoCorreto> ArquivosCorretos { get; set; }
    }
}
