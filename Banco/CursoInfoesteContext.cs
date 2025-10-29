using CursoInfoeste.Models;
using CursoInfoeste.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CursoInfoeste.Banco
{
    public class CursoInfoesteContext(DbContextOptions<CursoInfoesteContext> options, Persistencia persistencia) : DbContext(options)
    {
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<CashRegister> CashRegisters { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tenant>(entity =>
            {

            });

            modelBuilder.Entity<CashRegister>(entity =>
            {
                entity.HasIndex(entity => new{ entity.TenantId, entity.Number });

                entity.HasQueryFilter(x => x.TenantId == persistencia.TenantId);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(entity => entity.TenantId);
            });
        }
    }
}
