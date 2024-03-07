using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfraData.Domain;
using Microsoft.EntityFrameworkCore;

namespace ContextDb.ContextDB
{
    public class Context_Db : DbContext
    {
        public Context_Db(DbContextOptions<Context_Db> options) : base(options)
        {

        }

        public DbSet<Usuario> User { get; set; }
        public DbSet<Conta_debito> Conta { get; set; }
        public DbSet<CartaoCredito> cartaoCreditos { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context_Db).Assembly);
        }
        */


    }
}
