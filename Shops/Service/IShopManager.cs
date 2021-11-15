using System.Collections.Generic;
using Shops.ValueObj;

namespace Shops.Service
{
    public interface IShopManager
    {
        public bool AddProduct(Shop shop, string name, int quantity, decimal price);
        public bool AddProduct(Shop shop, Product product);
        public bool AddProduct(Shop shop, IEnumerable<Product> products);

        Product BuyProductFromShop(Product product, int quantity, Shop shop, Person customer);
        IEnumerable<Product> BuyProductFromShop(IEnumerable<Product> products, Shop shop, Person customer);

        Product FindProductByName(string name);
        Product FindProductById(int id);
        Product FindProductByName(Shop shop, string name);
        Product FindProductById(Shop shop, int id);
        Shop FindTheCheapestShopForTheseProducts(IEnumerable<Product> products);

        bool ChangeCostOfProduct(Shop shop, Product product, decimal price);
        IEnumerable<Product> GetAllProducts(Shop shop);
        IEnumerable<KeyValuePair<int, List<Product>>> GetAllProducts();

        Person CreateCustomer(decimal balance);
        Shop CreateShop(string name, string address, int balance);
    }
}