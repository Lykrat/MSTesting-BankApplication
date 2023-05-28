using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace BankApplication {

    internal class NavigationHandler {

        //Navigation system for admins
        public static void AdminNavigationMenu(Admin admin) {

            //Prints out the logged in admin name
            Console.WriteLine($"\nWelcome ADMIN: {admin.Name}");

            bool run = true;
            while (run) {

                Console.WriteLine("\n" +
                    "1. Admin information\r\n" +
                    "2. Create a new customer\r\n" +
                    "3. Logout"
                );

                byte choice;
                if (!byte.TryParse(Console.ReadLine(), out choice))
                    Console.WriteLine("\nNumber 1-3.");

                switch (choice) {

                    default: //If not a valid choice
                        Console.WriteLine("Not a valid choice.");
                        break;
                    case 1: //Admin information
                        admin.AdminInfo();
                        BankSystem.PressEnter();
                        break;
                    case 2: //Create new customers
                        while (true)
                        {
                            Console.WriteLine("\nName:");
                            string name = Console.ReadLine();

                            //A character limit between 1-20
                            if (name.Length > 20 || name.Length < 1)
                                Console.WriteLine("The account name needs to be between 1 and 20 characters");
                            else if (Users.customerList.Exists(x => x.Name == name))
                                Console.WriteLine("This account already exists for this user");
                            else
                            {
                                Console.WriteLine("\nPassword:");
                                string password = Console.ReadLine();
                                BankSystem.CustomerCreation(name,password);
                                BankSystem.PressEnter();
                                break;
                            }
                        }
                        break;
                    case 3: //Change exchange rate in USD to SEK
                        admin.AdminUpdateRates();
                        BankSystem.PressEnter();
                        break;
                    case 4: //Log out of Admin
                        Console.WriteLine($"\nLogged out of: {admin.Name}");
                        run = false;
                        LoginHandler.LogIn();
                        break;

                }

            }

        }

        //Navigation system for the users
        public static void NavigationMenu(Customer account) {

            //Prints out the logged in account name
            Console.WriteLine($"\nWelcome: {account.Name}");

            bool run = true;
            while (run) {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(@"  __  __              
 |  \/  |___ _ _ _  _ 
 | |\/| / -_) ' \ || |
 |_|  |_\___|_||_\_,_|
                ");
                Console.ResetColor();

                Console.WriteLine(
                    "\n1. Check account balance\r\n" +
                    "2. Open new account\r\n" +
                    "3. Transfer between accounts\r\n" +
                    "4. Transfer funds to another costumer\r\n" +
                    "5. Take a loan\r\n" +
                    "6. Open a savings account\r\n" +
                    "7. See my activity log\r\n" +
                    "8. Logout"
                );

                //Choice input
                byte choice;
                if (!byte.TryParse(Console.ReadLine(), out choice))
                    Console.WriteLine("\nNumber 1-7.");

                switch (choice) {

                    default: //If not a valid choice
                        Console.WriteLine("Not a valid choice.");
                        break;
                    case 1: //Check account balance
                        Console.WriteLine($"All accounts for {account.Name}");
                        account.CustomerInfo();
                        BankSystem.PressEnter();
                        break;
                    case 2: //Open new account
                        BankSystem.OpenAccount(account);
                        BankSystem.PressEnter();
                        break;
                    case 3: //Transfer between accounts
                        while (true)
                        {
                            account.AccountName();
                            Console.WriteLine("Which account do you want to transfer from: Name of the account");
                            string transferFrom = Console.ReadLine();

                            account.AccountName();
                            Console.WriteLine("Which of the accounts above do you want to transfer To:");
                            string tranferTo=Console.ReadLine();

                            float transfer;
                            account.AccountName();
                            Console.WriteLine("How much do you want to transfer");
                            if (!float.TryParse(Console.ReadLine(), out transfer))
                            {
                                Console.WriteLine("The value was not a float");
                                break;
                            }
                            Console.WriteLine();
                            BankSystem.TransferbetweenAccounts(account, transferFrom, tranferTo, transfer);
                            break;
                        }
                        BankSystem.PressEnter();
                        break;
                    case 4: //Transfer between customers
                        while (true)
                        {
                            Customer account2;
                            float amount;
                            account.CustomerInfo();
                            Console.WriteLine($"\nAccount name in {account.Name} to transfer from:");
                            string choiceAccount = Console.ReadLine();

                            Console.WriteLine("\nCustomer name to transfer to:");
                            string customerName = Console.ReadLine();

                            account2 = Users.customerList.Find(x => x.Name == customerName);
                            account2.AccountName();
                            Console.WriteLine($"\nAccount name in {account2.Name} to transfer to:");
                            string choiceAccount2 = Console.ReadLine();

                            Console.WriteLine($"\nYour account: {account.Name} : {account.accounts[choiceAccount][0] + account.accounts[choiceAccount][1]}\n" +
                              $"Transfer to: {account2.Name} : {account2.accounts[choiceAccount2][0] + account2.accounts[choiceAccount2][1]}\n" +
                              $"How much do you want to transfer?:");
                            if (!float.TryParse(Console.ReadLine(), out amount))
                            {
                                Console.WriteLine("The value was not a float");
                                break;
                            }
                            Console.WriteLine();
                            BankSystem.TransferBetweenCustomers(account,choiceAccount,choiceAccount2,customerName,amount);
                            break;
                        }
                        //BankSystem.TransferBetweenCustomers(account);
                        BankSystem.PressEnter();
                        break;
                    case 5: //Take a loan
                        BankSystem.Loan(account);
                        BankSystem.PressEnter();
                        break;
                    case 6:
                        BankSystem.SavingsAccount(account);
                        BankSystem.PressEnter();
                        break;
                    case 7: //See the logged activites of the user
                        BankSystem.SeeLog(account);
                        BankSystem.PressEnter();
                        break;
                    case 8: //Log out of customer
                        Console.WriteLine($"\nLogged out of: {account.Name}");
                        string sendlog = $"{DateTime.Now}: {account.Name} logged out";
                        BankSystem.Log(account, sendlog);
                        run = false;
                        LoginHandler.LogIn();
                        break;

                }

            }

        }

    }

}
