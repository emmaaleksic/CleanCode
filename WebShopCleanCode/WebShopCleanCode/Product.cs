namespace WebShopCleanCode
{
    public class Product : IProduct
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int NrInStock { get; set; }
        public Product(string name, int price, int nrInStock)
        {
           this.Name = name;
           this.Price = price;
           this. NrInStock = nrInStock;
        }

        public bool InStock()
        {
            return NrInStock > 0;
        }
        public void PrintInfo()
        {
            Console.WriteLine(Name + ": " + Price + "kr, " + NrInStock + " in stock.");
        }

        public string GetName()
        {
            return Name;
        }

        public int GetPrice()
        {
            return Price;
        }

        public int GetNrInStock()
        {
            return NrInStock;
        }
    }
}
