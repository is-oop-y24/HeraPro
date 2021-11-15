using System.Collections.Generic;
using Shops.ValueObj;

namespace Shops.Entity
{
    public interface IProductStack
    {
        bool AddProduct(Shop shop, Product product);
        bool AddProduct(Shop shop, string name, int quantity, decimal price);
        bool AddProduct(Shop shop, IEnumerable<Product> products);

        bool DeleteProduct(Shop shop, Product product);
        bool DeleteProduct(Shop shop, IEnumerable<Product> products);

        Product FindProductByName(Shop shop, string name);
        Product FindProductById(Shop shop, int id);

        bool ChangeCostOfProduct(Shop shop, Product product, decimal price);

        IEnumerable<KeyValuePair<int, List<Product>>> GetAllProducts();
        IEnumerable<Product> GetAllProducts(Shop shop);
    }
}