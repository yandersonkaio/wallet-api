using Wallet.Core.Application.DTOs.Wallet;
using Wallet.Core.Application.Interfaces;
using Wallet.Core.Domain.Entities;
using Wallet.Core.Domain.Exceptions;
using Wallet.Core.Infrastructure.Interfaces;

namespace Wallet.Core.Application.UseCases.WalletManagement
{
    public class TransferFundsUseCase : ITransferFundsUseCase
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IUserRepository _userRepository;

        public TransferFundsUseCase(IWalletRepository walletRepository,
            IUserRepository userRepository)
        {
            _walletRepository = walletRepository;
            _userRepository = userRepository;
        }

        public async Task<TransferFundsResponse> Execute(Guid userId, TransferFundsRequest request)
        {
            var fromWallet = await _walletRepository.GetByUserId(userId);
            var toUser = await _userRepository.GetByEmail(request.ToUserEmail);

            if (toUser == null)
            {
                throw new NotFoundException("Usuário de destino não encontrado.");
            }

            var toWallet = await _walletRepository.GetByUserId(toUser.Id);

            if (fromWallet == null || toWallet == null)
            {
                throw new NotFoundException("Carteira de origem ou destino não encontrada.");
            }

            if (fromWallet.Balance < request.Amount)
            {
                throw new ValidationException("Saldo insuficiente na carteira de origem.");
            }

            fromWallet.Balance -= request.Amount;
            toWallet.Balance += request.Amount;

            await _walletRepository.Update(fromWallet);
            await _walletRepository.Update(toWallet);

            var transaction = new Transfer
            {
                Id = Guid.NewGuid(),
                ReceiverWalletId = toWallet.Id,
                SenderWalletId = fromWallet.Id,
                Amount = request.Amount
            };
            await _walletRepository.AddTransaction(transaction);

            return new TransferFundsResponse
            {
                TransactionId = transaction.Id,
                FromWalletId = transaction.SenderWalletId,
                ToWalletId = transaction.ReceiverWalletId,
                Amount = transaction.Amount,
                CreatedAt = transaction.CreatedAt
            };
        }
    }
}
