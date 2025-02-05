namespace Wallet.Core.Application.DTOs.User
{
    public class AuthenticateUserResponse
    {
        public required UserDto User { get; set; }
        public required string Token { get; set; }
    }
}
