using CursoInfoeste.Abstractions.Repositories;
using CursoInfoeste.Banco.Repositories.Base;
using CursoInfoeste.Models;

namespace CursoInfoeste.Banco.Repositories
{
    public class TenantRepository : BaseRepository<Tenant>, ITenantRepository
    {
        public TenantRepository(CursoInfoesteContext context, Persistencia persistencia) : base(context, persistencia)
        {
        }



    }
}
