using CursoInfoeste.Models;

namespace CursoInfoeste.Abstractions.Repositories
{
    public interface ICashRegisterRepository : IRepository<CashRegister>
    {
        Task<CashRegister> GetByNumberAsync(int number, CancellationToken cancellationToken);
    }
}
