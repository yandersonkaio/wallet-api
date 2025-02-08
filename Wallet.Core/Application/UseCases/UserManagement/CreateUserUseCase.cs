using Microsoft.Extensions.Options;
using Wallet.Core.Application.DTOs.User;
using Wallet.Core.Application.Interfaces;
using Wallet.Core.Domain.Entities;
using Wallet.Core.Domain.Exceptions;
using Wallet.Core.Infrastructure.Configurations;
using Wallet.Core.Infrastructure.Interfaces;

namespace Wallet.Core.Application.UseCases.UserManagement
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly AuthJWTAppSettings _settings;
        private readonly ITokenService _tokenService;

        public CreateUserUseCase(
            IUserRepository userRepository, 
            IPasswordHasher passwordHasher,
            IOptions<AuthJWTAppSettings> settings,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _settings = settings.Value;
            _tokenService = tokenService;
        }

        public async Task<AuthenticateUserResponse> Execute(CreateUserRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                throw new ValidationException("E-mail e senha são obrigatórios.");

            var existingUser = await _userRepository.GetByEmail(request.Email);

            if (existingUser != null)
                throw new ValidationException("Usuário já cadastrado.");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                PasswordHash= _passwordHasher.Hash(request.Password)
            };

            await _userRepository.Add(user);

            var userDto = new UserDto
            (
                id: user.Id,
                name: request.Name,
                email: user.Email,
                createdAt: user.CreatedAt
            );

            var token = _tokenService.GenerateToken(userDto, _settings);

            return new AuthenticateUserResponse
            {
                User = userDto,
                Token = token
            };
        }
    }
}
