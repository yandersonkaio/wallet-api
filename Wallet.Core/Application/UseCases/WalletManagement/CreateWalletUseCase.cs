using Wallet.Core.Application.DTOs.Wallet;
using Wallet.Core.Application.Interfaces;
using Wallet.Core.Domain.Exceptions;
using Wallet.Core.Infrastructure.Interfaces;

namespace Wallet.Core.Application.UseCases.WalletManagement
{
    public class CreateWalletUseCase : ICreateWalletUseCase
    {
        private readonly IWalletRepository _walletRepository;

        public CreateWalletUseCase(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<WalletDto> Execute(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ValidationException("ID do usuário é obrigatório.");
            }

            var existingWallet = await _walletRepository.GetByUserId(userId);
            if (existingWallet != null)
            {
                throw new ValidationException("O usuário já possui uma carteira.");
            }

            var wallet = new Domain.Entities.Wallet
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Balance = 0
            };

            await _walletRepository.Add(wallet);

            return new WalletDto
            {
                Id = wallet.Id,
                UserId = wallet.UserId,
                Balance = wallet.Balance
            };
        }
    }
}
