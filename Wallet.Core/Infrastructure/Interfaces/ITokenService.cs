using Wallet.Core.Application.DTOs.User;
using Wallet.Core.Infrastructure.Configurations;

namespace Wallet.Core.Infrastructure.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(UserDto user, AuthJWTAppSettings settings);
    }
}
