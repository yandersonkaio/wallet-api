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

            modelBuilder.Entity<WalletEntity>()
                .Property(w => w.Balance)
                .HasColumnType("NUMERIC(18,2)")
                .IsRequired();

            modelBuilder.Entity<WalletEntity>()
                .ToTable(t => t.HasCheckConstraint("CK_Wallet_Balance", "\"Balance\" >= 0"));

            modelBuilder.Entity<Transfer>()
                .Property(w => w.Amount)
                .HasColumnType("NUMERIC(18,2)")
                .IsRequired();

            modelBuilder.Entity<Transfer>()
                .ToTable(t => t.HasCheckConstraint("CK_Transactions_Amount", "\"Amount\" >= 0"));

            modelBuilder.Entity<Transfer>()
                .Property(t => t.CreatedAt)
                .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<User>()
                .Property(t => t.CreatedAt)
                .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<WalletEntity>()
                .Property(t => t.CreatedAt)
                .HasColumnType("timestamp without time zone");

            // Configuração da relação 1:1 entre User e Wallet
            modelBuilder.Entity<User>()
                .HasOne(u => u.Wallet)
                .WithOne(w => w.User)
                .HasForeignKey<WalletEntity>(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuração da relação 1:N entre Wallet e Transfer (remetente)
            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.SenderWallet)
                .WithMany(w => w.SentTransfers)
                .HasForeignKey(t => t.SenderWalletId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuração da relação 1:N entre Wallet e Transfer (destinatário)
            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.ReceiverWallet)
                .WithMany(w => w.ReceivedTransfers)
                .HasForeignKey(t => t.ReceiverWalletId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
