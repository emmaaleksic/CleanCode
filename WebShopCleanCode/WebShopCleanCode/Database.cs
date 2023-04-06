namespace WebShopCleanCode
{
    public class Database
    {
        // We just pretend this accesses a real database.
        private List<ProxyProduct> productsInDatabase;
        private List<Customer> customersInDatabase;

        public Product GetProduct(string name, int price, int nrInStock)
        {
            return new Product(name, price, nrInStock);
        }
        public Database()
        {

            productsInDatabase = new List<ProxyProduct>();
            productsInDatabase.Add(new ProxyProduct("Mirror", 300, 2, this));
            productsInDatabase.Add(new ProxyProduct("Car", 2000000, 2, this));
            productsInDatabase.Add(new ProxyProduct("Candle", 50, 2, this));
            productsInDatabase.Add(new ProxyProduct("Computer", 100000, 2, this));
            productsInDatabase.Add(new ProxyProduct("Game", 599, 2, this));
            productsInDatabase.Add(new ProxyProduct("Painting", 399, 2, this));
            productsInDatabase.Add(new ProxyProduct("Chair", 500, 2, this));
            productsInDatabase.Add(new ProxyProduct("Table", 1000, 2, this));
            productsInDatabase.Add(new ProxyProduct("Bed", 20000, 2, this));


            customersInDatabase = new List<Customer>();
            CustomerBuilder customerBuilder = new CustomerBuilder();
            Customer Jimmy = customerBuilder.SetUsername("jimmy")
                           .SetPassword("jimisthebest")
                           .SetFirstname("Jimmy")
                           .SetEmail("jj@mail.com")
                           .SetAge(22)
                           .SetAddress("Big Street 5")
                           .SetPhoneNumber("123456789")
                           .Build();

            Customer Jake = customerBuilder.SetUsername("jake")
                           .SetPassword("jake123")
                           .SetFirstname("Jake")
                           .SetAge(0)
                           .Build();
            customersInDatabase.Add(Jimmy);
            customersInDatabase.Add(Jake);
        }

        public List<ProxyProduct> GetProducts()
        {
            return productsInDatabase;
        }

        public List<Customer> GetCustomers()
        {
            return customersInDatabase;
        }
    }
}
