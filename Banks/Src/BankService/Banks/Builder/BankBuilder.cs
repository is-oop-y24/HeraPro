using System.Collections.Generic;
using Banks.BankService.Accounts.BankAccount;
using Banks.BankService.Accounts.DepositAccount;

namespace Banks.BankService.Banks.Builder
{
    public class BankBuilder : IBankBuilder
    {
        private string _name;
        private double _balance;
        private double _commissionForCreditAccount;
        private int _limitForCreditAccount;
        private double _balancePaymentForDebitAccount;
        private List<DepositCommission> _percentByBalanceForDepositAccount;
        private int _limitForUntrustedCLients;

        public BankBuilder()
        {
            Reset();
        }

        public void BuildName(string name)
        {
            _name = name;
        }

        public void BuildBalance(double balance)
        {
            _balance = balance;
        }

        public void BuildCommissionForCreditAccount(double commissionForCreditAccount)
        {
            _commissionForCreditAccount = commissionForCreditAccount;
        }

        public void BuildLimitForCreditAccount(int limitForCreditAccount)
        {
            _limitForCreditAccount = limitForCreditAccount;
        }

        public void BuildBalancePaymentForDebitAccount(double balancePaymentForDebitAccount)
        {
            _balancePaymentForDebitAccount = balancePaymentForDebitAccount;
        }

        public void BuildPercentByBalanceForDepositAccount(List<DepositCommission> percentByBalanceForDepositAccount)
        {
            _percentByBalanceForDepositAccount = percentByBalanceForDepositAccount;
        }

        public void BuildLimitForUntrustedClient(int money)
        {
            _limitForUntrustedCLients = money;
        }

        public IBank GetBank()
        {
            var result = new Bank(
                _name,
                new BankAccount(
                    _balance,
                    _commissionForCreditAccount,
                    _limitForCreditAccount,
                    _balancePaymentForDebitAccount,
                    _percentByBalanceForDepositAccount,
                    _limitForUntrustedCLients));

            Reset();
            return result;
        }

        private void Reset()
        {
            _name = "Test";
            _balance = 100000;
            _commissionForCreditAccount = .1;
            _limitForCreditAccount = 20000;
            _balancePaymentForDebitAccount = .01;
            _percentByBalanceForDepositAccount = new List<DepositCommission>()
                { new (0.3, 50000), new (0.35, 100000), new (0.4, 500000) };
            _limitForUntrustedCLients = 10000;
        }
    }
}