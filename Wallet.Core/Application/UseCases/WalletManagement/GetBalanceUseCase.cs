using Wallet.Core.Application.Interfaces;
using Wallet.Core.Domain.Exceptions;
using Wallet.Core.Infrastructure.Interfaces;

namespace Wallet.Core.Application.UseCases.WalletManagement
{
    public class GetBalanceUseCase : IGetBalanceUseCase
    {
        private readonly IWalletRepository _walletRepository;

        public GetBalanceUseCase(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<decimal> Execute(Guid userId)
        {
            var wallet = await _walletRepository.GetByUserId(userId);
            if (wallet == null)
            {
                throw new NotFoundException("Carteira não encontrada.");
            }

            return wallet.Balance;
        }
    }
}
