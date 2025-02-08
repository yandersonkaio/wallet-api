namespace Wallet.Core.Application.DTOs.Wallet
{
    public class TransferFundsResponse
    {
        public Guid TransactionId { get; set; }
        public Guid FromWalletId { get; set; }
        public Guid ToWalletId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
