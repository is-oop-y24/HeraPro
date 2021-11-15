namespace Shops.ValueObj
{
    public class Shop
    {
        internal Shop(string name, int id, string address, CashAccount cashAccount)
        {
            Name = name;
            Id = id;
            Address = address;
            CashAccount = cashAccount;
        }

        public string Name { get; }
        public int Id { get; }
        public string Address { get; }
        public CashAccount CashAccount { get; }
    }
}