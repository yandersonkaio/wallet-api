namespace Wallet.Core.Application.DTOs.Wallet
{
    public class WalletDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Balance { get; set; }
    }
}
