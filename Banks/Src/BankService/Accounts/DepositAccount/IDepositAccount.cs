using System;
using System.Collections.Generic;

namespace Banks.BankService.Accounts.DepositAccount
{
    public interface IDepositAccount
    {
        List<DepositCommission> PercentByBalance { get; }
        DateTime StartPeriod { get; }
        DateTime EndPeriod { get; }
    }
}