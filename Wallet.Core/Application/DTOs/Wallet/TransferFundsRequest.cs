namespace Wallet.Core.Application.DTOs.Wallet
{
    public class TransferFundsRequest
    {
        public required string ToUserEmail { get; set; }
        public decimal Amount { get; set; }
    }
}
