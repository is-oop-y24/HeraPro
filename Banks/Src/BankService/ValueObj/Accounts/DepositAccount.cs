using System;

namespace Banks.BankService.ValueObj.Accounts
{
    public class DepositAccount : Account, IDepositAccount
    {
        public DepositAccount(int id, DateTime startPeriod, DateTime endPeriod, double balancePayment)
        : base(id)
        {
            StartPeriod = startPeriod;
            EndPeriod = endPeriod;
            BalancePayment = balancePayment;
        }

        public DateTime StartPeriod { get; }
        public DateTime EndPeriod { get; }
        public double BalancePayment { get; }
    }
}