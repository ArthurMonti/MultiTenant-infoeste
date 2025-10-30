using CursoInfoeste.Models;
using CursoInfoeste.Models.Base;
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

            ApplyTenantTypes(modelBuilder);

            modelBuilder.Entity<Tenant>(entity =>
            {

            });

            modelBuilder.Entity<CashRegister>(entity =>
            {
                entity.HasIndex(entity => new{ entity.TenantId, entity.Number });
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(entity => entity.TenantId);
            });
        }


        private void ApplyTenantTypes(ModelBuilder modelBuilder)
        {
            var types = modelBuilder.Model.GetEntityTypes();
            Expression<Func<int>> tenantId = () => persistencia.TenantId;
            var right = tenantId.Body;
            foreach (var item in types)
            {

                if (item.ClrType.BaseType != typeof(BaseTenantEntity))
                    continue;

                //Adiciona QueryFilter
                var tenantIdProperty = item.FindProperty("TenantId");
                var parameter = Expression.Parameter(item.ClrType, "p");
                var left = Expression.Property(parameter, tenantIdProperty!.PropertyInfo!);
                var filter = Expression.Lambda(Expression.Equal(left, right), parameter);
                modelBuilder.Entity(item.ClrType).HasQueryFilter(filter);

            }
        }
    }
}
