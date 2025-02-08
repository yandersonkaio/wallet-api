using Microsoft.EntityFrameworkCore;
using Wallet.Core.Domain.Entities;
using Wallet.Core.Infrastructure.Database;
using Wallet.Core.Infrastructure.Interfaces;
using WalletEntity = Wallet.Core.Domain.Entities.Wallet;

namespace Wallet.Core.Infrastructure.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly WalletDbContext _context;

        public WalletRepository(WalletDbContext context)
        {
            _context = context;
        }

        public async Task Add(WalletEntity wallet)
        {
            await _context.Wallets.AddAsync(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task<WalletEntity> GetById(Guid walletId)
        {
            return await _context.Wallets
                .FirstOrDefaultAsync(w => w.Id == walletId);
        }

        public async Task<WalletEntity> GetByUserId(Guid userId)
        {
            return await _context.Wallets
                .FirstOrDefaultAsync(w => w.UserId == userId);
        }

        public async Task Delete(Guid walletId)
        {
            var wallet = await _context.Wallets
                            .FirstOrDefaultAsync(w => w.Id == walletId);

            if (wallet != null)
            {
                _context.Wallets.Remove(wallet);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(WalletEntity wallet)
        {
            _context.Wallets.Update(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task AddTransaction(Transfer transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Transfer>> GetTransactionsByUserId(
            Guid userId,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            var query = _context.Transactions.Include(t => t.SenderWallet).Include(t => t.ReceiverWallet)
                .Where(t => t.SenderWallet.UserId == userId || t.ReceiverWallet.UserId == userId);

            if (startDate.HasValue)
            {
                query = query.Where(t => t.CreatedAt >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(t => t.CreatedAt <= endDate.Value);
            }

            return await query
                .Include(t => t.SenderWallet)
                .Include(t => t.ReceiverWallet)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }
    }
}
