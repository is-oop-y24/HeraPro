using System.Collections.Generic;
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
            Operations.Add(new Log(bankFrom.Account, bankTo.Account, money, OperationEnum.TransferOp));
            bankFrom.Account.Balance -= money;
            bankTo.Account.Balance += money;
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
    }
}