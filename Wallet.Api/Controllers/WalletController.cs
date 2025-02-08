using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wallet.Core.Application.DTOs.Wallet;
using Wallet.Core.Application.Interfaces;

namespace Wallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly ICreateWalletUseCase _createWalletUseCase;
        private readonly IAddBalanceUseCase _addBalanceUseCase;
        private readonly IGetBalanceUseCase _getBalanceUseCase;
        private readonly ITransferFundsUseCase _transferFundsUseCase;
        private readonly IListTransfersUseCase _listTransfersUseCase;


        public WalletController(
            ICreateWalletUseCase createWalletUseCase,
            IAddBalanceUseCase addBalanceUseCase,
            IGetBalanceUseCase getBalanceUseCase,
            ITransferFundsUseCase transferFundsUseCase,
            IListTransfersUseCase listTransfersUseCase)
        {
            _createWalletUseCase = createWalletUseCase;
            _addBalanceUseCase = addBalanceUseCase;
            _getBalanceUseCase = getBalanceUseCase;
            _transferFundsUseCase = transferFundsUseCase;
            _listTransfersUseCase = listTransfersUseCase;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateWallet()
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "userId")?.Value);
            var response = await _createWalletUseCase.Execute(userId);
            return Ok(response);
        }


        [HttpGet("balance")]
        [Authorize]
        public async Task<IActionResult> GetBalance()
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "userId")?.Value);
            var response = await _getBalanceUseCase.Execute(userId);
            return Ok(response);
        }

        [HttpPost("add-balance")]
        [Authorize]
        public async Task<IActionResult> AddBalance(AddBalanceRequest request)
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "userId")?.Value);

            var response = await _addBalanceUseCase.Execute(userId, request);
            return Ok(response);
        }

        [HttpPost("transfer")]
        [Authorize]
        public async Task<IActionResult> TransferFunds([FromBody] TransferFundsRequest request)
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "userId")?.Value);
            var response = await _transferFundsUseCase.Execute(userId, request);
            return Ok(response);
        }

        [HttpGet("transfers")]
        [Authorize]
        public async Task<IActionResult> ListTransfers([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "userId")?.Value);
            var transfers = await _listTransfersUseCase.Execute(userId, startDate, endDate);
            return Ok(transfers);
        }
    }
}
