using Wallet.Core.Application.DTOs.Wallet;

namespace Wallet.Core.Application.Interfaces
{
    public interface IAddBalanceUseCase
    {
        Task<WalletDto> Execute(Guid userId, AddBalanceRequest request);
    }
}
