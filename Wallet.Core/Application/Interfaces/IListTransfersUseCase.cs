using Wallet.Core.Application.DTOs.Wallet;

namespace Wallet.Core.Application.Interfaces
{
    public interface IListTransfersUseCase
    {
        Task<List<TransferDto>> Execute(Guid userId, DateTime? startDate, DateTime? endDate);
    }
}
