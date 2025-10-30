using CursoInfoeste.Models.Requests;
using CursoInfoeste.Models.Responses;

namespace CursoInfoeste.Abstractions.Services
{
    public interface ICashRegisterService
    {
        Task<CashRegisterResponse> GetByNumber( int number, CancellationToken cancellationToken);
        Task<CashRegisterResponse> Create(int tenantId, CreateCashRegisterRequest request, CancellationToken cancellationToken);
        Task<bool> Open(int tenantId, int number, CancellationToken cancellationToken);
        Task<bool> Close(int tenantId, int number, CancellationToken cancellationToken);
    }
}
