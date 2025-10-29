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
        private readonly Persistencia persistencia;

        public CashRegisterService(ICashRegisterRepository repository, Persistencia persistencia)
        {
            _repository = repository;
            this.persistencia = persistencia;
        }

        public async Task<CashRegisterResponse> Create( CreateCashRegisterRequest request, CancellationToken cancellationToken)
        {
            var cashRegister = new CashRegister
            {
                Number = request.Number,
                TenantId = persistencia.TenantId,
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

        public async Task<CashRegisterResponse> GetByNumber( int number, CancellationToken cancellationToken)
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

        public async Task<bool> Open( int number, CancellationToken cancellationToken)
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

        public async Task<bool> Close( int number, CancellationToken cancellationToken)
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

        public async Task<List<CashRegister>> GetAll()
        {
            return await _repository.GetAll();
        }

    }
}
