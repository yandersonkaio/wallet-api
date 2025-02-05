using Microsoft.EntityFrameworkCore;
using Wallet.Core.Domain.Entities;
using WalletEntity = Wallet.Core.Domain.Entities.Wallet;

namespace Wallet.Core.Infrastructure.Database
{
    public class WalletDbContext : DbContext
    {
        public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<WalletEntity> Wallets { get; set; }
        public DbSet<Transfer> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("DefaultConnection", b => b.MigrationsAssembly("Wallet.Api"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da relação 1:1 entre User e Wallet
            modelBuilder.Entity<User>()
                .HasOne(u => u.Wallet)
                .WithOne(w => w.User)
                .HasForeignKey<WalletEntity>(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Se o usuário for excluído, a carteira também será

            // Configuração da relação 1:N entre Wallet e Transfer (remetente)
            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.SenderWallet)
                .WithMany(w => w.SentTransfers)
                .HasForeignKey(t => t.SenderWalletId)
                .OnDelete(DeleteBehavior.Restrict); // Impede a exclusão da carteira se houver transferências

            // Configuração da relação 1:N entre Wallet e Transfer (destinatário)
            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.ReceiverWallet)
                .WithMany(w => w.ReceivedTransfers)
                .HasForeignKey(t => t.ReceiverWalletId)
                .OnDelete(DeleteBehavior.Restrict); // Impede a exclusão da carteira se houver transferências
        }
    }
}
