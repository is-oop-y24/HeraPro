namespace Banks.BankService.Accounts
{
    public abstract class Account
    {
        internal Account(double balance)
        {
            Balance = balance;
        }

        public double Balance { get; internal set; }

        internal abstract bool Withdraw(double money);
        internal abstract bool Deposit(double money);
        internal abstract bool DoCommission();
        internal abstract double CheckCommission(int days);
    }
}