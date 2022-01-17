using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Banks.BankLogService;
using Banks.BankService.Accounts.DepositAccount;
using Banks.BankService.Banks.Builder;
using Banks.Database;

namespace Banks.BankService.Banks
{
    public class CentralBank
    {
        private readonly IBankBuilder _bankBuilder;
        private readonly Repository<IBank> _repositoryOfBanks;

        public CentralBank()
        {
            _bankBuilder = new BankBuilder();
            _repositoryOfBanks = new Repository<IBank>();
            Operations = new List<Log>();
        }

        public List<Log> Operations { get; }

        public IBank CreateDefaultBank(string name)
        {
            _bankBuilder.BuildName(name);

            IBank bank = _bankBuilder.GetBank();
            _repositoryOfBanks.Add(bank);
            return bank;
        }

        public IBank CreateBankWithSpecialConditions(string name, double commissionForCreditAccount, int limitForCreditAccount, double balancePaymentForDebitAccount, List<DepositCommission> percentByBalanceForDepositAccount)
        {
            _bankBuilder.BuildName(name);
            _bankBuilder.BuildCommissionForCreditAccount(commissionForCreditAccount);
            _bankBuilder.BuildLimitForCreditAccount(limitForCreditAccount);
            _bankBuilder.BuildBalancePaymentForDebitAccount(balancePaymentForDebitAccount);
            _bankBuilder.BuildPercentByBalanceForDepositAccount(percentByBalanceForDepositAccount);

            IBank bank = _bankBuilder.GetBank();
            _repositoryOfBanks.Add(bank);
            return bank;
        }

        public bool TransferFromTo(IBank bankFrom, IBank bankTo, double money)
        {
            if (!_repositoryOfBanks.Contains(bankFrom) || !_repositoryOfBanks.Contains(bankTo)) return false;
            if (money < 0) return false;

            var scope = new TransactionScope();
            if (!bankFrom.Account.Withdraw(money)) return false;
            bankTo.Account.Deposit(money);
            Operations.Add(new Log(bankFrom.Account, bankTo.Account, money, OperationEnum.TransferOp));
            scope.Complete();
            return true;
        }

        public bool RollBackTransaction(Log log)
        {
            if (!Operations.Contains(log)) return false;
            Operations.Remove(log);

            var scope = new TransactionScope();
            log.AccountTo.Balance -= log.Money;
            log.AccountFrom.Balance += log.Money;
            scope.Complete();
            return true;
        }

        public void DoCommission()
        {
            foreach (IBank bank in _repositoryOfBanks.GetItemList())
            {
                bank.DoCommission();
            }
        }

        public void DoPayment()
        {
            foreach (IBank bank in _repositoryOfBanks.GetItemList())
            {
                bank.DoPayment();
            }
        }

        public IBank FindBankByName(string name)
        {
            var banks = _repositoryOfBanks.GetItemList().ToList();
            return banks.Find(x => x.Name.Equals(name));
        }

        public IEnumerable<Log> GetTransactionsByAllBanks()
        {
            var list = Operations.ToList();
            foreach (IBank bank in GetBanksList())
            {
                list.AddRange(bank.Operations);
            }

            return list;
        }

        public IEnumerable<IBank> GetBanksList() => _repositoryOfBanks.GetItemList();
    }
}