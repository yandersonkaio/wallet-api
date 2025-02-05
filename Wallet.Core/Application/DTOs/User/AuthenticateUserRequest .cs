namespace Wallet.Core.Application.DTOs.User
{
    public class AuthenticateUserRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
