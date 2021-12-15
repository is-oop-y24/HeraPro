namespace Banks.BankService.ValueObj.Accounts
{
    public class BankAccount : Account
    {
        public BankAccount(int id, double commissionForCreditAccount, int limitForCreditAccount, double balancePaymentForDebitAccount, double balancePaymentForDepositAccount)
            : base(id)
        {
            CommissionForCreditAccount = commissionForCreditAccount;
            LimitForCreditAccount = limitForCreditAccount;
            BalancePaymentForDebitAccount = balancePaymentForDebitAccount;
            BalancePaymentForDepositAccount = balancePaymentForDepositAccount;
        }

        public double CommissionForCreditAccount { get; }
        public int LimitForCreditAccount { get; }
        public double BalancePaymentForDebitAccount { get; }
        public double BalancePaymentForDepositAccount { get; }
    }
}