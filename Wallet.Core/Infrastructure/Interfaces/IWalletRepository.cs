using Wallet.Core.Domain.Entities;
using WalletEntity = Wallet.Core.Domain.Entities.Wallet;

namespace Wallet.Core.Infrastructure.Interfaces
{
    public interface IWalletRepository
    {
        Task<WalletEntity> GetById(Guid walletId);
        Task<WalletEntity> GetByUserId(Guid userId);
        Task Add(WalletEntity wallet);
        Task Update(WalletEntity wallet);
        Task Delete(Guid walletId);
        Task AddTransaction(Transfer transaction);
        Task<List<Transfer>> GetTransactionsByUserId(
            Guid userId,
            DateTime? startDate = null,
            DateTime? endDate = null);

    }
}
