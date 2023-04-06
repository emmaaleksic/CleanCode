namespace WebShopCleanCode
{
    public class CustomerBuilder
    {
        public string Username { get; set; }
        private string password;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int Funds { get; set; }
        private List<Order> Orders { get; set; }

        public CustomerBuilder SetUsername(string username) 
        {
            this.Username = username;
            return this;
        }

        public CustomerBuilder SetPassword(string password)
        {
            this.password = password;
            return this;
        }
        public CustomerBuilder SetFirstname(string firstName)
        {
            this.FirstName = firstName;
            return this;
        }
        public CustomerBuilder SetLastname(string lastName)
        {
            this.LastName = lastName;
            return this;
        }
        public CustomerBuilder SetEmail(string email)
        {
            this.Email = email;
            return this;
        }
        public CustomerBuilder SetAge(int age)
        {
            this.Age = age;
            return this;
        }
        public CustomerBuilder SetAddress(string address)
        {
            this.Address = address;
            return this;
        }
        public CustomerBuilder SetPhoneNumber(string phoneNumber)
        {
            this.PhoneNumber = phoneNumber;
            return this;
        }

        public CustomerBuilder funds(int Funds)
        {
            this.Funds = Funds;
            return this;
        }

        public Customer Build()
        {
            return new Customer(Username, password, FirstName, LastName, Email, Age, Address, PhoneNumber);
        }
    }
}
