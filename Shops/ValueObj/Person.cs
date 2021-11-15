namespace Shops.ValueObj
{
    public class Person
    {
        internal Person(CashAccount cashAccount)
        {
            CashAccount = cashAccount;
        }

        public CashAccount CashAccount { get; }
    }
}