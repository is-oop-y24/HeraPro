namespace Banks.BankService.Accounts.DebitAccount
{
    public class DebitAccount : Account, IDebitAccount
    {
        public DebitAccount(double balance, double balancePayment)
            : base(balance)
        {
            BalancePayment = balancePayment;
            Cashback = 0;
        }

        public double BalancePayment { get; }
        public double Cashback { get; private set; }

        internal override bool Withdraw(double money)
        {
            if (money < 0) return false;

            double diff = Balance - money;
            if (diff < 0) return false;

            Balance = diff;
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
            double result = CheckCommission(1);
            if (result == 0) return false;
            Cashback += result;
            return true;
        }

        internal override double CheckCommission(int days)
        {
            if (days < 0) return 0;
            return Balance * (BalancePayment / days);
        }

        internal override void DoPayment()
        {
            Balance += Cashback;
            Cashback = 0;
        }
    }
}