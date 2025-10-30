using CursoInfoeste.Models.Base;

namespace CursoInfoeste.Abstractions.Repositories
{
    public interface IBaseTenantRepository<T> where T : BaseTenantEntity
    {
        Task<List<T>> GetAll(int pageNumber = 1, int pageSize = 10);
        Task<T?> Get(int id);
        Task Delete(int id);
        Task Delete(T entity);
        Task<T> Insert(T entity, CancellationToken cancellationToken);
        Task<T> Update(T entity, CancellationToken cancellationToken);

    }
}
