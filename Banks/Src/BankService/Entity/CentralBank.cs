using Banks.BankService.ValueObj.Accounts;
using Banks.Database;
using Banks.TransactionService;

namespace Banks.BankService.Entity
{
    public class CentralBank
    {
        private Repository<Bank> _repositoryOfBanks;
        private Transaction _transaction;
        public CentralBank()
        {
            _repositoryOfBanks = new Repository<Bank>();
            _transaction = new Transaction();
        }

        public Bank CreateBank(int money)
        {
            BankAccount bankAccount = _transaction.AddBankAccount(money);
            var bank = new Bank(bankAccount);
            _repositoryOfBanks.Add(bank);

            return bank;
        }

        public bool TransferFromTo(Bank bank1, Bank bank2, int money) => _transaction.TransferFromTo(bank1.BankAccount.Id, bank2.BankAccount.Id, money);
        public void AlertBanks();

        // events?
    }
}