namespace Banks.BankService.ValueObj.Accounts
{
    public class DebitAccount : Account, IDebitAccount
    {
        public DebitAccount(int id, double balancePayment)
            : base(id)
        {
            BalancePayment = balancePayment;
        }

        public double BalancePayment { get; }
    }
}