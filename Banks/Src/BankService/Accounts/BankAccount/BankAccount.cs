using System.Collections.Generic;
using Banks.BankService.Accounts.DepositAccount;

namespace Banks.BankService.Accounts.BankAccount
{
    public class BankAccount : Account, IBankAccount
    {
        public BankAccount(double balance, double commissionForCreditAccount, int limitForCreditAccount, double balancePaymentForDebitAccount, List<DepositCommission> percentByBalanceForDepositAccount, int limitForUntrustedClients)
            : base(balance)
        {
            CommissionForCreditAccount = commissionForCreditAccount;
            LimitForCreditAccount = limitForCreditAccount;
            BalancePaymentForDebitAccount = balancePaymentForDebitAccount;
            PercentByBalanceForDepositAccount = percentByBalanceForDepositAccount;
            LimitForUntrustedClients = limitForUntrustedClients;
        }

        public double CommissionForCreditAccount { get; }
        public int LimitForCreditAccount { get; }

        public double BalancePaymentForDebitAccount { get; }

        public List<DepositCommission> PercentByBalanceForDepositAccount { get; }

        public int LimitForUntrustedClients { get; }

        internal override bool Withdraw(double money)
        {
            if (money < 0) return false;

            Balance -= money;
            return true;
        }

        internal override bool Deposit(double money)
        {
            if (money < 0) return false;

            Balance += money;
            return true;
        }

        internal override bool DoCommission()
        {
            return true;
        }

        internal override double CheckCommission(int days)
        {
            return 0;
        }
    }
}