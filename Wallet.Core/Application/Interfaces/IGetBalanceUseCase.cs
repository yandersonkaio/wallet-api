namespace Wallet.Core.Application.Interfaces
{
    public interface IGetBalanceUseCase
    {
        Task<decimal> Execute(Guid userId);
    }
}
