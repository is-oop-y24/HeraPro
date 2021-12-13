using System.Collections.Generic;
using Banks.BankService.ValueObj;
using Banks.BankService.ValueObj.Accounts;

namespace Banks.TransactionService
{
    public interface ITransaction
    {
        DepositAccount AddDepositAccount(int money);
        DebitAccount AddDebitAccount(int money);
        CreditAccount AddCreditAccount(int money);
        public BankAccount AddBankAccount(int money);
        bool TransferFromTo(int id1, int id2, int money);
        bool Withdraw(int id, int money);
        void Deposit(int id, int money);
        IEnumerable<Log> GetTransactionsFromIdByOperation(int id, int operation);
        bool RollBackTransaction(Log log);
    }
}