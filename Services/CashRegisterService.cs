using CursoInfoeste.Abstractions.Repositories;
using CursoInfoeste.Abstractions.Services;
using CursoInfoeste.Models;
using CursoInfoeste.Models.Requests;
using CursoInfoeste.Models.Responses;

namespace CursoInfoeste.Services
{
    public class CashRegisterService : ICashRegisterService
    {
        private readonly ICashRegisterRepository _repository;

        public CashRegisterService(ICashRegisterRepository repository)
        {
            _repository = repository;
        }

        public async Task<CashRegisterResponse> Create(int tenantId, CreateCashRegisterRequest request, CancellationToken cancellationToken)
        {
            var cashRegister = new CashRegister
            {
                Number = request.Number,
                TenantId = tenantId,
                IsOpen = false
            };
            var newCashRegister = await _repository.Insert(cashRegister, cancellationToken);
            return new CashRegisterResponse
            {
                Id = newCashRegister.Id,
                Number = newCashRegister.Number,
                TenantId = newCashRegister.TenantId,
                IsOpen = newCashRegister.IsOpen
            };
        }

        public async Task<CashRegisterResponse> GetByNumber(int number, CancellationToken cancellationToken)
        {
            var cashRegister = await _repository.GetByNumberAsync(number, cancellationToken);
            if (cashRegister == null)
            {
                return null;
            }
            return new CashRegisterResponse
            {
                Id = cashRegister.Id,
                Number = cashRegister.Number,
                TenantId = cashRegister.TenantId,
                IsOpen = cashRegister.IsOpen
            };
        }

        public async Task<bool> Open(int tenantId, int number, CancellationToken cancellationToken)
        {
            var cashRegister = await _repository.GetByNumberAsync(number, cancellationToken);
            if (cashRegister == null || cashRegister.IsOpen)
            {
                return false;
            }

            cashRegister.IsOpen = true;
            await _repository.Update(cashRegister, cancellationToken);
            return true;
        }

        public async Task<bool> Close(int tenantId, int number, CancellationToken cancellationToken)
        {
            var cashRegister = await _repository.GetByNumberAsync(number, cancellationToken);
            if (cashRegister == null || !cashRegister.IsOpen)
            {
                return false;
            }

            cashRegister.IsOpen = false;
            await _repository.Update(cashRegister, cancellationToken);
            return true;
        }
    }
}
