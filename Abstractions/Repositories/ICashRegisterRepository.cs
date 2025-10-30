using CursoInfoeste.Models;

namespace CursoInfoeste.Abstractions.Repositories
{
    public interface ICashRegisterRepository : IBaseTenantRepository<CashRegister>
    {
        Task<CashRegister> GetByNumberAsync(int number, CancellationToken cancellationToken);
    }
}
