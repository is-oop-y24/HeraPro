namespace Shops.ValueObj
{
    public class Product
    {
        internal Product(string name, int id, int quantity, decimal price)
        {
            Name = name;
            Id = id;
            Quantity = quantity;
            Price = price;
        }

        public string Name { get; }
        public int Id { get; }
        public int Quantity { get; }
        public decimal Price { get; }
    }
}