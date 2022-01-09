namespace Banks.BankService.Accounts.CreditAccount
{
    public class CreditAccount : Account, ICreditAccount
    {
        public CreditAccount(double balance, int limit, double commission)
            : base(balance)
        {
            Limit = limit;
            Commission = commission;
        }

        public int Limit { get; }
        public double Commission { get; }

        internal override bool Withdraw(double money)
        {
            if (money < 0) return false;

            double diff = Balance - money;
            if (diff < 0)
                diff = Balance - (money * Commission);

            if (-diff > Limit) return false;

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
            return true;
        }

        internal override double CheckCommission(int days)
        {
            return 0;
        }
    }
}