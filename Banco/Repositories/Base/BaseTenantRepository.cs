using CursoInfoeste.Abstractions.Repositories;
using CursoInfoeste.Models.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CursoInfoeste.Banco.Repositories.Base
{
    public abstract class BaseTenantRepository<T>(CursoInfoesteContext context, Persistencia persistencia)
        : IBaseTenantRepository<T> where T : BaseTenantEntity
    {
        protected readonly CursoInfoesteContext _context = context;
        protected readonly DbSet<T> _repository = context.Set<T>();
        protected readonly Persistencia _persistencia = persistencia;

        public virtual async Task<List<T>> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            return await _repository.ToListAsync();
        }

        public virtual async Task<T?> Get(int id)
        {
            return await _repository.FindAsync(id);
        }

        public virtual async Task Delete(int id)
        {
            var settedRepo = _repository;
            var foundItem = await settedRepo.FindAsync(id);
            if (foundItem != null)
            {
                settedRepo.Remove(foundItem);
                await _context.SaveChangesAsync();
            }
        }
        public virtual async Task Delete(T entity)
        {
            var settedRepo = _repository;
            settedRepo.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<T> Insert(T entity, CancellationToken cancellationToken)
        {
            entity.TenantId = _persistencia.TenantId;
            await _repository.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity; 
        }

        public virtual async Task<T> Update(T entity, CancellationToken cancellationToken)
        {
            _repository.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

    }
}
