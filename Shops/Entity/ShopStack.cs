using System.Collections.Generic;
using Shops.ValueObj;

namespace Shops.Entity
{
    internal class ShopStack : IShopStack
    {
        private readonly List<Shop> _listOfShops;

        internal ShopStack()
        {
            _listOfShops = new List<Shop>();
        }

        public Shop CreateShop(string name, string address, CashAccount cashAccount)
        {
            if (_listOfShops.FindIndex(x => x.Address.Equals(address)) != -1)
                return null;

            _listOfShops.Add(new Shop(name, _listOfShops.Count, address, cashAccount));
            return _listOfShops[^1];
        }

        public Shop FindShopByName(string name)
        {
            return _listOfShops.Find(x => x.Name == name);
        }

        public Shop FindShopByAddress(string address)
        {
            return _listOfShops.Find(x => x.Address == address);
        }

        public Shop FindShopById(int id)
        {
            return _listOfShops.Find(x => x.Id.Equals(id));
        }

        public IEnumerable<Shop> GetAllShops()
        {
            return _listOfShops.AsReadOnly();
        }
    }
}