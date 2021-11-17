using Shops.ValueObj;

namespace Shops.Entity
{
    public interface ICashStack
    {
        bool TransferFromTo(CashAccount account1, CashAccount account2, decimal price);
        CashAccount CreateCashAccount(decimal balance);
        decimal ShowCashInAccount(CashAccount account);
    }
}