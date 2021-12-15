using System;
using System.Collections.Generic;
using Banks.BankService.ValueObj.Accounts;

namespace Banks.TransactionService
{
    public interface ITransaction
    {
        DepositAccount AddDepositAccount(double money, DateTime startPeriod, DateTime endPeriod, double balancePayment);
        DebitAccount AddDebitAccount(double money, double balancePayment);
        CreditAccount AddCreditAccount(double money, int limit, double commission);

        public BankAccount AddBankAccount(double money, double commissionForCreditAccount, int limitForCreditAccount, double balancePaymentForDebitAccount, double balancePaymentForDepositAccount);
        void Deposit(Account account, double money);
        bool Withdraw(Account account, double money);
        bool Withdraw(DepositAccount account, double money);
        bool Withdraw(CreditAccount account, double money);
        bool TransferFromTo(Account account1, Account account2, double money);
        IEnumerable<Log> GetTransactionsFromIdByOperation(Account account, int operation);
        bool RollBackTransaction(Log log);
        void DoPayment(DebitAccount account);
        void DoPayment(DepositAccount account);
        double DoCommission(CreditAccount account, double money);
        double CheckPayment(DebitAccount account, int days);
        double CheckPayment(DepositAccount account, int days);
    }
}