using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wallet.Core.Application.DTOs.User;
using Wallet.Core.Application.Interfaces;

namespace Wallet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ICreateUserUseCase _createUserUseCase;
        private readonly IAuthenticateUserUseCase _authenticateUserUseCase;
        private readonly ILogger<UserController> _logger;

        public UserController(
            ICreateUserUseCase createUserUseCase,
            IAuthenticateUserUseCase authenticateUserUseCase,
            ILogger<UserController> logger)
        {
            _createUserUseCase = createUserUseCase;
            _authenticateUserUseCase = authenticateUserUseCase;
            _logger = logger;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
        {
            var response = await _createUserUseCase.Execute(request);
            return Ok(response);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AuthenticateUserRequest request)
        {
            var response = await _authenticateUserUseCase.Execute(request);
            return Ok(response);
        }
    }
}
