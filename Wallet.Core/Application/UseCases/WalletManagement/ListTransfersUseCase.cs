using Wallet.Core.Application.DTOs.Wallet;
using Wallet.Core.Application.Interfaces;
using Wallet.Core.Infrastructure.Interfaces;

namespace Wallet.Core.Application.UseCases.WalletManagement
{
    public class ListTransfersUseCase : IListTransfersUseCase
    {
        private readonly IWalletRepository _walletRepository;

        public ListTransfersUseCase(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<List<TransferDto>> Execute(Guid userId, DateTime? startDate, DateTime? endDate)
        {
            var transactions = await _walletRepository.GetTransactionsByUserId(userId, startDate, endDate);

            var transfers = transactions.Select(t => new TransferDto
            {
                TransactionId = t.Id,
                FromWalletId = t.SenderWalletId,
                ToWalletId = t.ReceiverWalletId,
                Amount = t.Amount,
                CreatedAt = t.CreatedAt
            }).ToList();

            return transfers;
        }
    }
}
