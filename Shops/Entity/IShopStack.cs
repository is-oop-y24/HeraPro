using System.Collections.Generic;
using Shops.ValueObj;

namespace Shops.Entity
{
    public interface IShopStack
    {
        Shop CreateShop(string name, string address, CashAccount cashAccount);
        Shop FindShopByName(string name);
        Shop FindShopByAddress(string address);
        Shop FindShopById(int id);
        IEnumerable<Shop> GetAllShops();
    }
}