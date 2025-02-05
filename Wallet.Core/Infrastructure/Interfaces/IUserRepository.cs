using Wallet.Core.Domain.Entities;

namespace Wallet.Core.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmail(string email);
        Task Add(User user);
        Task Update(User user);
        Task Delete(Guid userId);
    }
}
