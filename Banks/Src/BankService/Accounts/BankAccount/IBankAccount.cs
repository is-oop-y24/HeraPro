using System.Collections.Generic;
using Banks.BankService.Accounts.DepositAccount;

namespace Banks.BankService.Accounts.BankAccount
{
    public interface IBankAccount
    {
        double CommissionForCreditAccount { get; }
        int LimitForCreditAccount { get; }

        double BalancePaymentForDebitAccount { get; }

        List<DepositCommission> PercentByBalanceForDepositAccount { get; }

        int LimitForUntrustedClients { get; }
    }
}