namespace WebShopCleanCode
{
    public class WebShop
    {
        private const string SeeWares = "See Wares";
        private const string CustomerInfo = "Customer Info";
        private const string Login = "Login";
        private const string LikeToDo = "What would you like to do?";
        bool running = true;
        Database database = new Database();
        List<ProxyProduct> products = new List<ProxyProduct>();
        List<Customer> customers = new List<Customer>();


        string currentMenu = "main menu";
        int currentChoice = 1;
        int amountOfOptions = 3;
        string option1 = SeeWares;
        string option2 = CustomerInfo;
        string option3 = Login;
        string option4 = "";
        string info = LikeToDo;


        string username = null;
        string password = null;
        Customer currentCustomer;

        public WebShop()
        {
            products = database.GetProducts();
            customers = database.GetCustomers();
        }

        public void Run()
        {

            Console.WriteLine("Welcome to the WebShop!");
            while (running)
            {
                PrintMenu();
                // User Input
                string choice = Console.ReadLine().ToLower();
                switch (choice)
                {
                    case "left":
                    case "l":
                        if (currentChoice > 1)
                        {
                            currentChoice--;
                        }
                        break;
                    case "right":
                    case "r":
                        if (currentChoice < amountOfOptions)
                        {
                            currentChoice++;
                        }
                        break;
                    case "ok":
                    case "k":
                        if (currentMenu.Equals("main menu"))
                            MainMenu();

                        else if (currentMenu.Equals("customer menu"))
                        {
                            CustomerMenu();
                        }
                        else if (currentMenu.Equals("sort menu"))
                        {
                            SortMenu();
                        }
                        else if (currentMenu.Equals("wares menu"))
                        {
                            WaresMenu();
                        }
                        else if (currentMenu.Equals("login menu"))
                        {
                            choice = LoginMenu(choice);
                        }
                        else if (currentMenu.Equals("purchase menu"))
                        {
                            PurchaseMenu();
                        }
                        break;
                    case "back":
                    case "b":
                        Back();
                        break;
                    case "quit":
                    case "q":
                        Console.WriteLine("The console powers down. You are free to leave.");
                        return;
                    default:
                        Console.WriteLine("That is not an applicable option.");
                        break;
                }
            }
        }

        private void PrintMenu()
        {
            Console.WriteLine(info);

            if (currentMenu.Equals("purchase menu"))
            {
                for (int i = 0; i < amountOfOptions; i++)
                {
                    Console.WriteLine(i + 1 + ": " + products[i].GetName() + ", " + products[i].GetPrice() + "kr");
                }
                Console.WriteLine("Your funds: " + currentCustomer.Funds);
            }
            else
            {
                Console.WriteLine("1: " + option1);
                Console.WriteLine("2: " + option2);
                if (amountOfOptions > 2)
                {
                    Console.WriteLine("3: " + option3);
                }
                if (amountOfOptions > 3)
                {
                    Console.WriteLine("4: " + option4);
                }
            }

            for (int i = 0; i < amountOfOptions; i++)
            {
                Console.Write(i + 1 + "\t");
            }
            Console.WriteLine();
            for (int i = 1; i < currentChoice; i++)
            {
                Console.Write("\t");
            }
            Console.WriteLine("|");

            Console.WriteLine("Your buttons are Left, Right, OK, Back and Quit.");
            if (currentCustomer != null)
            {
                Console.WriteLine("Current user: " + currentCustomer.Username);
            }
            else
            {
                Console.WriteLine("Nobody logged in.");
            }
        }

        private void Back()
        {
            if (currentMenu.Equals("main menu"))
            {
                PrintText("You're already on the main menu.");
            }
            else if (currentMenu.Equals("purchase menu"))
            {
                option1 = "See all wares";
                option2 = "Purchase a ware";
                option3 = "Sort wares";
                if (currentCustomer == null)
                {
                    option4 = Login;
                }
                else
                {
                    option4 = "Logout";
                }
                amountOfOptions = 4;
                currentChoice = 1;
                currentMenu = "wares menu";
                info = LikeToDo;
            }
            else
            {
                option1 = SeeWares;
                option2 = CustomerInfo;
                if (currentCustomer == null)
                {
                    option3 = Login;
                }
                else
                {
                    option3 = "Logout";
                }
                info = LikeToDo;
                currentMenu = "main menu";
                currentChoice = 1;
                amountOfOptions = 3;
            }
        }

        private void PurchaseMenu()
        {
            int index = currentChoice - 1;
            ProxyProduct product = products[index];
            if (product.InStock())
            {
                if (currentCustomer.CanAfford(product.GetPrice()))
                {
                    currentCustomer.Funds -= product.GetPrice();
                    product.RemoveOneInStock();
                    currentCustomer.Orders.Add(new Order(product.GetName(), product.GetPrice(), DateTime.Now));
                    PrintText("Successfully bought " + product.GetName());
                }
                else
                {
                    PrintText("You cannot afford.");
                }
            }
            else
            {
                PrintText("Not in stock.");
            }
        }

        private string LoginMenu(string choice)
        {
            switch (currentChoice)
            {
                case 1:
                    LogIn();
                    break;
                case 2:
                    Register();
                    break;
                default:
                    PrintText("Not an option.");
                    break;
            }
            return choice;
        }

        private void LogIn()
        {
            Console.WriteLine("A keyboard appears.");
            Console.WriteLine("Please input your username.");
            username = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("A keyboard appears.");
            Console.WriteLine("Please input your password.");
            password = Console.ReadLine();
            Console.WriteLine();
            bool found = false;

            foreach (Customer customer in customers)
            {
                if (username.Equals(customer.Username) && customer.CheckPassword(password))
                {
                    currentCustomer = customer;
                    found = true;
                    option1 = SeeWares;
                    option2 = CustomerInfo;
                    if (currentCustomer == null)
                    {
                        option3 = Login;
                    }
                    else
                    {
                        option3 = "Logout";
                    }
                    info = LikeToDo;
                    currentMenu = "main menu";
                    currentChoice = 1;
                    amountOfOptions = 3;
                    break;
                }
            }
            if (found == false)
            {
                PrintText("Invalid credentials.");
            }
        }

        private void Register()
        {
            Console.WriteLine("Please write your username.");
            string newUsername = Console.ReadLine();
            foreach (Customer customer in customers)
            {
                if (customer.Username.Equals(newUsername))
                {
                    PrintText("Username already exists.");
                    return;
                }

            }
            // Would have liked to be able to quit at any time in here.
            CustomerBuilder customerBuilder = new CustomerBuilder();

            customerBuilder.SetUsername(newUsername);

            string newPassword = FillForm("password");
            if(!newPassword.Equals(""))
            {
                customerBuilder.SetPassword(newPassword);
            }

            string firstName = FillForm("first name");
            if (!firstName.Equals("")) 
            {
                customerBuilder.SetFirstname(firstName);
            }

            string lastName = FillForm("Last name");
            if (!lastName.Equals("")) 
            {
                customerBuilder.SetLastname(lastName);
            }

            string email = FillForm("email");
            if (!email.Equals("")) 
            {
                customerBuilder.SetEmail(email);
            }

            string address = FillForm("address");
            if (!address.Equals("")) 
            {
                customerBuilder.SetAddress(address);
            }

            string phoneNumber = FillForm("phone number");
            if (!phoneNumber.Equals("")) 
            {
                customerBuilder.SetPhoneNumber(phoneNumber);  
            }

            int age = FillFormAge();
            if (age != -1)
            {
                customerBuilder.SetAge(age);
            }

            Customer newCustomer = customerBuilder.Build();
            customers.Add(newCustomer);
            currentCustomer = newCustomer;
            PrintText(newCustomer.Username + " successfully added and is now logged in.");
            option1 = SeeWares;
            option2 = CustomerInfo;
            if (currentCustomer == null)
            {
                option3 = Login;
            }
            else
            {
                option3 = "Logout";
            }
            info = LikeToDo;
            currentMenu = "main menu";
            currentChoice = 1;
            amountOfOptions = 3;
        }

        private void PrintText(string Text)
        {
            Console.WriteLine();
            Console.WriteLine(Text);
            Console.WriteLine();
        }

        private void WaresMenu()
        {
            switch (currentChoice)
            {
                case 1:
                    foreach (ProxyProduct product in products)
                    {
                        product.PrintInfo();
                    }
                    break;
                case 2:
                    if (currentCustomer != null)
                    {
                        currentMenu = "purchase menu";
                        info = "What would you like to purchase?";
                        currentChoice = 1;
                        amountOfOptions = products.Count;
                    }
                    else
                    {
                        PrintText("You must be logged in to purchase wares.");
                        currentChoice = 1;
                    }
                    break;
                case 3:
                    option1 = "Sort by name, descending";
                    option2 = "Sort by name, ascending";
                    option3 = "Sort by price, descending";
                    option4 = "Sort by price, ascending";
                    info = "How would you like to sort them?";
                    currentMenu = "sort menu";
                    currentChoice = 1;
                    amountOfOptions = 4;
                    break;
                case 4:
                    if (currentCustomer == null)
                    {
                        option1 = Login;
                        option2 = "Register";
                        amountOfOptions = 2;
                        info = "Please submit username and password.";
                        currentChoice = 1;
                        currentMenu = "login menu";
                    }
                    else
                    {
                        option4 = Login;
                        PrintText(currentCustomer.Username + " logged out.");
                        currentCustomer = null;
                        currentChoice = 1;
                    }
                    break;
                case 5:
                    break;
                default:
                    PrintText("Not an option.");
                    break;
            }
        }

        private void SortMenu()
        {
            bool back = true;
            switch (currentChoice)
            {
                case 1:
                    bubbleSort("name", false);
                    PrintText("Wares sorted.");
                    break;
                case 2:
                    bubbleSort("name", true);
                    PrintText("Wares sorted.");
                    break;
                case 3:
                    bubbleSort("price", false);
                    PrintText("Wares sorted.");
                    break;
                case 4:
                    bubbleSort("price", true);
                    PrintText("Wares sorted.");
                    break;
                default:
                    back = false;
                    PrintText("Not an option.");
                    break;
            }
            if (back)
            {
                option1 = "See all wares";
                option2 = "Purchase a ware";
                option3 = "Sort wares";
                if (currentCustomer == null)
                {
                    option4 = Login;
                }
                else
                {
                    option4 = "Logout";
                }
                amountOfOptions = 4;
                currentChoice = 1;
                currentMenu = "wares menu";
                info = LikeToDo;
            }
        }

        private void CustomerMenu()
        {
            switch (currentChoice)
            {
                case 1:
                    currentCustomer.PrintOrders();
                    break;
                case 2:
                    currentCustomer.PrintInfo();
                    break;
                case 3:
                    AddFunds();
                    break;
                default:
                    PrintText("Not an option.");
                    break;
            }
        }

        private void AddFunds()
        {
            Console.WriteLine("How many funds would you like to add?");
            string amountString = Console.ReadLine();
            try
            {
                int amount = int.Parse(amountString);
                if (amount < 0)
                {
                    PrintText("Don't add negative amounts.");
                }
                else
                {
                    currentCustomer.Funds += amount;
                    PrintText(amount + " added to your profile.");
                }
            }
            catch (FormatException e)
            {
                PrintText("Please write a number next time.");
            }
        }

        private void MainMenu()
        {
            switch (currentChoice)
            {
                case 1:
                    option1 = "See all wares";
                    option2 = "Purchase a ware";
                    option3 = "Sort wares";
                    if (currentCustomer == null)
                    {
                        option4 = Login;
                    }
                    else
                    {
                        option4 = "Logout";
                    }
                    amountOfOptions = 4;
                    currentChoice = 1;
                    currentMenu = "wares menu";
                    info = LikeToDo;
                    break;
                case 2:
                    if (currentCustomer != null)
                    {
                        option1 = "See your orders";
                        option2 = "See your info";
                        option3 = "Add funds";
                        option4 = "";
                        amountOfOptions = 3;
                        currentChoice = 1;
                        info = LikeToDo;
                        currentMenu = "customer menu";
                    }
                    else
                    {
                        PrintText("Nobody is logged in.");
                    }
                    break;
                case 3:
                    if (currentCustomer == null)
                    {
                        option1 = Login;
                        option2 = "Register";
                        amountOfOptions = 2;
                        currentChoice = 1;
                        info = "Please submit username and password.";
                        username = null;
                        password = null;
                        currentMenu = "login menu";
                    }
                    else
                    {
                        option3 = Login;
                        PrintText(currentCustomer.Username + " logged out.");
                        currentChoice = 1;
                        currentCustomer = null;
                    }
                    break;
                default:
                    PrintText("Not an option.");
                    break;
            }
        }

        private string FillForm(string text) 
        {
            while (true)
            {   
                Console.WriteLine("Do you want a " + text + "? y/n");
                string choice = Console.ReadLine();
                if (choice.Equals("y"))
                {
                    while (true)
                    {
                        Console.WriteLine("Please write your" + text );
                        string userInput = Console.ReadLine();
                        if (userInput.Equals(""))
                        {
                            PrintText("Please actually write something.");
                            continue;
                        }
                        else
                        {
                            return userInput;
                        }
                    }
                }   
                if (choice.Equals("n"))
                {
                    return "";
                }
                PrintText("y or n, please.");
            }
        }

        private int FillFormAge() 
        {
            while (true)
            {
                int age = -1;
                Console.WriteLine("Do you want an age? y/n");
                string choice = Console.ReadLine();
                if (choice.Equals("y"))
                {
                    while (true)
                    {
                        Console.WriteLine("Please write your age.");
                        string ageString = Console.ReadLine();
                        try
                        {
                            age = int.Parse(ageString);
                        }
                        catch (FormatException e)
                        {
                            PrintText("Please write a number.");
                            continue;
                        }
                        return age;
                    }
                }
                if (choice.Equals("n"))
                {
                    return age;
                }
                PrintText("y or n, please.");
            }
        }

        private void bubbleSort(string variable, bool ascending)
        {
            if (variable.Equals("name")) {
                int length = products.Count;
                for(int i = 0; i < length - 1; i++)
                {
                    bool sorted = true;
                    int length2 = length - i;
                    for (int j = 0; j < length2 - 1; j++)
                    {
                        if (ascending)
                        {
                            if (products[j].GetName().CompareTo(products[j + 1].GetName()) < 0)
                            {
                                ProxyProduct temp = products[j];
                                products[j] = products[j + 1];
                                products[j + 1] = temp;
                                sorted = false;
                            }
                        }
                        else
                        {
                            if (products[j].GetName().CompareTo(products[j + 1].GetName()) > 0)
                            {
                                ProxyProduct temp = products[j];
                                products[j] = products[j + 1];
                                products[j + 1] = temp;
                                sorted = false;
                            }
                        }
                    }
                    if (sorted == true)
                    {
                        break;
                    }
                }
            }
            else if (variable.Equals("price"))
            {
                int length = products.Count;
                for (int i = 0; i < length - 1; i++)
                {
                    bool sorted = true;
                    int length2 = length - i;
                    for (int j = 0; j < length2 - 1; j++)
                    {
                        if (ascending)
                        {
                            if (products[j].GetPrice() > products[j + 1].GetPrice())
                            {
                                ProxyProduct temp = products[j];
                                products[j] = products[j + 1];
                                products[j + 1] = temp;
                                sorted = false;
                            }
                        }
                        else
                        {
                            if (products[j].GetPrice() < products[j + 1].GetPrice())
                            {
                                ProxyProduct temp = products[j];
                                products[j] = products[j + 1];
                                products[j + 1] = temp;
                                sorted = false;
                            }
                        }
                    }
                    if (sorted == true)
                    {
                        break;
                    }
                }
            }
        }
    }
}