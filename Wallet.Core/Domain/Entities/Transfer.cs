namespace Wallet.Core.Domain.Entities
{
    public class Transfer
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Guid SenderWalletId { get; set; }
        public Wallet SenderWallet { get; set; }

        public Guid ReceiverWalletId { get; set; }
        public Wallet ReceiverWallet { get; set; }

    }
}
