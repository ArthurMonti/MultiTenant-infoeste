using CursoInfoeste.Models;
using CursoInfoeste.Models.Requests;
using CursoInfoeste.Models.Responses;

namespace CursoInfoeste.Abstractions.Services
{
    public interface ICashRegisterService
    {
        Task<CashRegisterResponse> GetByNumber( int number, CancellationToken cancellationToken);
        Task<CashRegisterResponse> Create( CreateCashRegisterRequest request, CancellationToken cancellationToken);
        Task<bool> Open(int number, CancellationToken cancellationToken);
        Task<bool> Close(int number, CancellationToken cancellationToken);

        Task<List<CashRegister>> GetAll();
    }
}
