namespace Banks.BankService.ValueObj.Accounts
{
    public class CreditAccount : Account, ICreditAccount
    {
        public CreditAccount(int id, int limit, double commission)
            : base(id)
        {
            Limit = limit;
            Commission = commission;
        }

        public int Limit { get; }
        public double Commission { get; }
    }
}