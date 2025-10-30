using CursoInfoeste.Abstractions.Repositories;
using CursoInfoeste.Abstractions.Services;
using CursoInfoeste.Banco.Repositories.Base;
using CursoInfoeste.Models;
using CursoInfoeste.Models.Requests;
using CursoInfoeste.Models.Responses;

namespace CursoInfoeste.Services
{
    public class TenantService : ITenantService
    {
        private readonly ITenantRepository _repository;
        public TenantService(ITenantRepository repository)
        {
            _repository = repository;
        }

        public async Task<TenantResponse> Create(CreateTenantRequest request, CancellationToken cancellationToken)
        {
            var tenant = new Tenant { Name = request.Name };
            var newTenant = await _repository.Insert(tenant, cancellationToken);
            return new TenantResponse { Id = newTenant.Id, Name = newTenant.Name };
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _repository.Delete(id);
        }

        public async Task<TenantResponse> Get(int id, CancellationToken cancellationToken)
        {
            var tenant = await _repository.Get(id);
            return tenant == null ? null : new TenantResponse { Id = tenant.Id, Name = tenant.Name };
        }

        public async Task<IEnumerable<TenantResponse>> List(CancellationToken cancellationToken)
        {
            var tenants = await _repository.GetAll();
            return tenants.Select(t => new TenantResponse { Id = t.Id, Name = t.Name });
        }

        public async Task<TenantResponse> Update(int id, UpdateTenantRequest request, CancellationToken cancellationToken)
        {
            var tenant = await _repository.Get(id);
            if (tenant == null)
            {
                return null;
            }
            tenant.Name = request.Name;
            var updatedTenant = await _repository.Update(tenant, cancellationToken);
            return new TenantResponse { Id = updatedTenant.Id, Name = updatedTenant.Name };
        }
    }
}
