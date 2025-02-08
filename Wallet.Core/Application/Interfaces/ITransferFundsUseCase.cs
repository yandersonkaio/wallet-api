using Wallet.Core.Application.DTOs.Wallet;

namespace Wallet.Core.Application.Interfaces
{
    public interface ITransferFundsUseCase
    {
        Task<TransferFundsResponse> Execute(Guid userId, TransferFundsRequest request);
    }
}
