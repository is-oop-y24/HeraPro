namespace Banks.BankService.Accounts.CreditAccount
{
    public interface ICreditAccount
    {
        int Limit { get; }
        double Commission { get; }
    }
}