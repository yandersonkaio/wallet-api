using Wallet.Core.Application.DTOs.User;

namespace Wallet.Core.Application.Interfaces
{
    public interface ICreateUserUseCase
    {
        Task<AuthenticateUserResponse> Execute(CreateUserRequest request);
    }
}
