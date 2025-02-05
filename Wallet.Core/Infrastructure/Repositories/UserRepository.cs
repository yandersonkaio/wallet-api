using Microsoft.EntityFrameworkCore;
using Wallet.Core.Domain.Entities;
using Wallet.Core.Infrastructure.Database;
using Wallet.Core.Infrastructure.Interfaces;

namespace Wallet.Core.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WalletDbContext _context;

        public UserRepository(WalletDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public Task Update(User user)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
