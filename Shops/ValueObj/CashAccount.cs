namespace Shops.ValueObj
{
    public class CashAccount
    {
        internal CashAccount(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}