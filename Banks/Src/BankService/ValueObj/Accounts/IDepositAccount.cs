using System;

namespace Banks.BankService.ValueObj.Accounts
{
    public interface IDepositAccount
    {
        DateTime StartPeriod { get; }
        DateTime EndPeriod { get; }
        double BalancePayment { get; }
    }
}