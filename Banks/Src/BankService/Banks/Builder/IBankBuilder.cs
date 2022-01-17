using System.Collections.Generic;
using Banks.BankService.Accounts.DepositAccount;

namespace Banks.BankService.Banks.Builder
{
    public interface IBankBuilder
    {
        void BuildName(string name);
        void BuildCommissionForCreditAccount(double commissionForCreditAccount);
        void BuildLimitForCreditAccount(int limitForCreditAccount);
        void BuildBalancePaymentForDebitAccount(double balancePaymentForDebitAccount);
        void BuildPercentByBalanceForDepositAccount(List<DepositCommission> percentByBalanceForDepositAccount);
        IBank GetBank();
    }
}