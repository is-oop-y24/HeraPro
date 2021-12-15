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

        public Bank CreateBank(double money, double commissionForCreditAccount, int limitForCreditAccount, double balancePaymentForDebitAccount, double balancePaymentForDepositAccount)
        {
            BankAccount bankAccount = _transaction.AddBankAccount(money, commissionForCreditAccount, limitForCreditAccount, balancePaymentForDebitAccount, balancePaymentForDepositAccount);
            var bank = new Bank(bankAccount);
            _repositoryOfBanks.Add(bank);

            return bank;
        }

        public bool TransferFromTo(Bank bank1, Bank bank2, double money) => _transaction.TransferFromTo(bank1.BankAccount, bank2.BankAccount, money);

        public void AlertBanks()
        {
            foreach (Bank i in _repositoryOfBanks.GetItemList())
            {
                i.CheckPayment();
            }
        }
    }
}