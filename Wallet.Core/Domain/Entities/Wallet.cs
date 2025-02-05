namespace Wallet.Core.Domain.Entities
{
    public class Wallet
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }


        public ICollection<Transfer> SentTransfers { get; set; }

        public ICollection<Transfer> ReceivedTransfers { get; set; }
    }
}
