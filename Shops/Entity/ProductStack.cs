using System.Collections.Generic;
using System.Linq;
using Shops.ValueObj;

namespace Shops.Entity
{
    internal class ProductStack : IProductStack
    {
        private readonly Dictionary<int, List<Product>> _listOfProductsByShop;
        private int _id;

        internal ProductStack()
        {
            _listOfProductsByShop = new Dictionary<int, List<Product>>();
            _id = 1;
        }

        public bool AddProduct(Shop shop, Product product)
        {
            if (shop == null)
                return false;
            if (product == null)
                return false;

            if (!_listOfProductsByShop.ContainsKey(shop.Id))
                _listOfProductsByShop.Add(shop.Id, new List<Product>());

            int index = _listOfProductsByShop[shop.Id].FindIndex(x => x.Id.Equals(product.Id));
            if (index == -1)
            {
                _listOfProductsByShop[shop.Id].Add(product);
                return true;
            }

            int tmpQuantity = _listOfProductsByShop[shop.Id][index].Quantity;
            _listOfProductsByShop[shop.Id].RemoveAt(index);
            _listOfProductsByShop[shop.Id]
                .Add(new Product(product.Name, product.Id, product.Quantity + tmpQuantity, product.Price));
            return true;
        }

        public bool AddProduct(Shop shop, string name, int quantity, decimal price)
        {
            return AddProduct(shop, RegisterProduct(name, quantity, price));
        }

        public bool AddProduct(Shop shop, IEnumerable<Product> products)
        {
            return products.All(i => AddProduct(shop, i));
        }

        public bool DeleteProduct(Shop shop, Product product)
        {
            if (!_listOfProductsByShop.ContainsKey(shop.Id)) return false;

            int index = _listOfProductsByShop[shop.Id].FindIndex(x => x.Id.Equals(product.Id));
            if (index == -1) return false;

            _listOfProductsByShop[shop.Id].RemoveAt(index);
            return true;
        }

        public bool DeleteProduct(Shop shop, IEnumerable<Product> products)
        {
            return products.All(i => DeleteProduct(shop, i));
        }

        public Product FindProductByName(Shop shop, string name)
        {
            return !_listOfProductsByShop.ContainsKey(shop.Id)
                ? null
                : _listOfProductsByShop[shop.Id].Find(x => x.Name.Equals(name));
        }

        public Product FindProductById(Shop shop, int id)
        {
            return !_listOfProductsByShop.ContainsKey(shop.Id)
                ? null
                : _listOfProductsByShop[shop.Id].Find(x => x.Id.Equals(id));
        }

        public bool ChangeCostOfProduct(Shop shop, Product product, decimal price)
        {
            if (price < 0) return false;
            if (!_listOfProductsByShop.ContainsKey(shop.Id)) return false;
            if (!_listOfProductsByShop[shop.Id].Remove(product)) return false;

            _listOfProductsByShop[shop.Id].Add(new Product(product.Name, product.Id, product.Quantity, price));
            return true;
        }

        public IEnumerable<KeyValuePair<int, List<Product>>> GetAllProducts()
        {
            return _listOfProductsByShop.AsEnumerable();
        }

        public IEnumerable<Product> GetAllProducts(Shop shop)
        {
            return shop == null ? null : _listOfProductsByShop[shop.Id].AsReadOnly();
        }

        private Product RegisterProduct(string name, int quantity, decimal price)
        {
            if (name == null)
                return null;
            if (quantity < 0)
                return null;
            return price < 0 ? null : new Product(name, _id++, quantity, price);
        }
    }
}