using Wallet.Core.Application.DTOs.Wallet;

namespace Wallet.Core.Application.Interfaces
{
    public interface ICreateWalletUseCase
    {
        Task<WalletDto> Execute(Guid userId);
    }
}
