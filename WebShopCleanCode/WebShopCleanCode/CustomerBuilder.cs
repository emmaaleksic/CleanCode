namespace WebShopCleanCode
{
    public class CustomerBuilder
    {
        private string username;
        private string password;
        private string firstName;
        private string lastName;
        private string email;
        private int age;
        private string address;
        private string phoneNumber;

        public CustomerBuilder SetUsername(string username) 
        {
            this.username = username;
            return this;
        }

        public CustomerBuilder SetPassword(string password)
        {
            this.password = password;
            return this;
        }
        public CustomerBuilder SetFirstname(string firstName)
        {
            this.firstName = firstName;
            return this;
        }
        public CustomerBuilder SetLastname(string lastName)
        {
            this.lastName = lastName;
            return this;
        }
        public CustomerBuilder SetEmail(string email)
        {
            this.email = email;
            return this;
        }
        public CustomerBuilder SetAge(int age)
        {
            this.age = age;
            return this;
        }
        public CustomerBuilder SetAdress(string address)
        {
            this.address = address;
            return this;
        }
        public CustomerBuilder SetPhoneNumber(string phoneNumber)
        {
            this.phoneNumber = phoneNumber;
            return this;
        }

        public Customer Build()
        {
            return new Customer(username, password, firstName, lastName, email, age, address, phoneNumber);
        }
    }
}
