using Wallet.Core.Application.DTOs.Wallet;
using Wallet.Core.Application.Interfaces;
using Wallet.Core.Domain.Exceptions;
using Wallet.Core.Infrastructure.Interfaces;

namespace Wallet.Core.Application.UseCases.WalletManagement
{
    public class AddBalanceUseCase : IAddBalanceUseCase
    {
        private readonly IWalletRepository _walletRepository;

        public AddBalanceUseCase(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<WalletDto> Execute(Guid userId, AddBalanceRequest request)
        {
            if (request.Amount <= 0)
            {
                throw new ValidationException("O valor a ser adicionado deve ser maior que zero.");
            }

            var wallet = await _walletRepository.GetByUserId(userId);

            if (wallet == null)
            {
                throw new NotFoundException("Carteira não encontrada.");
            }

            wallet.Balance += request.Amount;

            await _walletRepository.Update(wallet);

            return new WalletDto
            {
                Id = wallet.Id,
                UserId = wallet.UserId,
                Balance = wallet.Balance
            };
        }
    }
}
