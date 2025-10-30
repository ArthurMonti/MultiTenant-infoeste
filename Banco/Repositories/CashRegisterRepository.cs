using CursoInfoeste.Abstractions.Repositories;
using CursoInfoeste.Banco.Repositories.Base;
using CursoInfoeste.Models;
using Microsoft.EntityFrameworkCore;

namespace CursoInfoeste.Banco.Repositories
{
    public class CashRegisterRepository : BaseRepository<CashRegister>, ICashRegisterRepository
    {
        public CashRegisterRepository(CursoInfoesteContext context, Persistencia persistencia) : base(context, persistencia)
        {
        }

        public async Task<CashRegister> GetByNumberAsync(int number, CancellationToken cancellationToken)
        {

            var query = _repository.IgnoreQueryFilters().Where(x => x.Number == number);

            var stringQuery = query.ToQueryString();

            return await query.FirstOrDefaultAsync();
        }
    }
}
