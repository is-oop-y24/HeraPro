using Banks.BankService.Accounts.BankAccount;

namespace Banks.BankEventService
{
    public interface ISubscriber
    {
        IBankAccount Subject { get; }
        void Update(IBankAccount subject);
    }
}