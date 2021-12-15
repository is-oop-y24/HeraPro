namespace Banks.BankService.ValueObj.Accounts
{
    public interface ICreditAccount
    {
        int Limit { get; }
        double Commission { get; }
    }
}