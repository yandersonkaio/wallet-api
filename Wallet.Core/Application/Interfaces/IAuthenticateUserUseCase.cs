using Wallet.Core.Application.DTOs.User;

namespace Wallet.Core.Application.Interfaces
{
    public interface IAuthenticateUserUseCase
    {
        Task<AuthenticateUserResponse> Execute(AuthenticateUserRequest request);
    }
}
