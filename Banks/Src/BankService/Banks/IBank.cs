using Banks.BankService.Accounts.BankAccount;

namespace Banks.BankService.Banks
{
    public interface IBank
    {
        string Name { get; }
        BankAccount Account { get; }

        public void DoCommission();
    }
}