namespace WebShopCleanCode
{       
    public class ProxyProduct : IProduct
    { // Implementerat Proxy i products för att belasta vår "databas" varje gång användare ska kolla på produkterna som finns att köpa. 

        string Name;
        int Price;
        int NrInStock;
        Product product;
        Database database;

        public ProxyProduct(string name, int price, int nrInStock, Database database) 
        {
            this.Name = name;
            this.Price = price;
            this.NrInStock = nrInStock;
            this.database = database;
        }

        public void LoadedCheck() 
        {
            if (product == null)
            {
                Console.WriteLine(Name + " Loaded");
                product = database.GetProduct(Name, Price, NrInStock);
            }
        }

        public string GetName()
        {
            LoadedCheck();
            return product.GetName();
        }

        public int GetPrice()
        {
            LoadedCheck();
            return product.GetPrice();
        }

        public int GetNrInStock()
        {
            LoadedCheck();
            return product.GetNrInStock();
        }

        public void PrintInfo()
        {
            Console.WriteLine(GetName() + ": " + GetPrice() + "kr, " + GetNrInStock() + " in stock.");
        }

        public bool InStock()
        {
            return GetNrInStock() > 0;
        }

        public void RemoveOneInStock() 
        {
            LoadedCheck();
            product.NrInStock--;
        }
    }
}
