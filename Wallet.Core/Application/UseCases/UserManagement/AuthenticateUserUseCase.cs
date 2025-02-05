using Microsoft.Extensions.Options;
using Wallet.Core.Application.DTOs.User;
using Wallet.Core.Application.Interfaces;
using Wallet.Core.Domain.Exceptions;
using Wallet.Core.Infrastructure.Configurations;
using Wallet.Core.Infrastructure.Interfaces;

namespace Wallet.Core.Application.UseCases.UserManagement
{
    public class AuthenticateUserUseCase : IAuthenticateUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly AuthJWTAppSettings _settings;


        public AuthenticateUserUseCase(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            ITokenService tokenService,
            IOptions<AuthJWTAppSettings> settings)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _settings = settings.Value;
        }

        public async Task<AuthenticateUserResponse> Execute(AuthenticateUserRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                throw new UnauthorizedException("E-mail e senha são obrigatórios.");
            }

            var user = await _userRepository.GetByEmail(request.Email);
            if (user == null)
            {
                throw new UnauthorizedException("Usuário ou senha inválidos.");
            }

            if (!_passwordHasher.Verify(user.PasswordHash, request.Password))
            {
                throw new UnauthorizedException("Usuário ou senha inválidos.");
            }

            var userDto = new UserDto
            (
                id: user.Id,
                name: user.Name,
                email: user.Email,
                createdAt: user.CreatedAt
            );

            var token = _tokenService.GenerateToken(userDto, _settings);

            return new AuthenticateUserResponse
            {
                User = new UserDto(id: user.Id, name: user.Name, email: user.Email, createdAt: user.CreatedAt),
                Token = token
            };
        }
    }
}
