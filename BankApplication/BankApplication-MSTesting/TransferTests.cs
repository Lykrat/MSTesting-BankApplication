using BankApplication;
using System;
using System.Reflection.Metadata;
using System.Security.Principal;

namespace BankApplication_MSTesting
{
    [TestClass]
    public class TransferTests
    {
        [TestMethod]
        public void TransferbetweenAccounts_Test()
        {
            //Arrange
            var customer1Dict = new Dictionary<string, List<string>>() {
                { "Sparkonto", new List<string>() { 1000.0f.ToString(), "kr", "Personkonto" } },
                { "Lönekonto", new List<string>() { 2000.0f.ToString(), "$", "Personkonto" } },
            };
            Customer customer1 = new Customer("Anas", "111", customer1Dict);

            Customer customer2 = new Customer("Anas", "111", customer1Dict);


            //Act
            BankSystem.TransferbetweenAccounts(customer1,"Sparkonto","Lönekonto",59.6f);

            //Assert
            Assert.AreNotEqual(customer1,customer2);
        }
        [TestMethod]
        public void TransferbetweenAccounts_NameTest()
        {
            //Arrange
            var customer1Dict = new Dictionary<string, List<string>>() {
                { "Sparkonto", new List<string>() { 1000.0f.ToString(), "kr", "Personkonto" } },
                { "Lönekonto", new List<string>() { 2000.0f.ToString(), "$", "Personkonto" } },
            };
            Customer customer1 = new Customer("Anas", "111", customer1Dict);
            string transferFrom = "Sparkonto";

            //Act
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            BankSystem.TransferbetweenAccounts(customer1, transferFrom, "Lönekonto", 69.5f);
            var output = consoleOutput.ToString();

            //Assert
            Assert.IsTrue(output.Contains("Account not found of the name: " + transferFrom));
        }
        [TestMethod]
        public void TransferBetweenCustomers_Test()
        {
            //Arrange
            var customer1Dict = new Dictionary<string, List<string>>() {
                { "Sparkonto", new List<string>() { 1000.0f.ToString(), "kr", "Personkonto" } },
                { "Lönekonto", new List<string>() { 2000.0f.ToString(), "$", "Personkonto" } },
            };
            Customer customer1 = new Customer("Tobias", "111", customer1Dict);

            var customer2Dict = new Dictionary<string, List<string>>() {
                { "Sparkonto", new List<string>() { 1000.0f.ToString(), "kr", "Personkonto" } },
                { "Lönekonto", new List<string>() { 2000.0f.ToString(), "$", "Personkonto" } },
            };
            Customer customer2 = new Customer("Anas", "222", customer2Dict);
            Customer customer3 = new Customer("Anas", "222", customer2Dict);

            Users.customerList.Add(customer1);
            Users.customerList.Add(customer2);
            Users.customerList.Add(customer3);

            //Act
            BankSystem.TransferBetweenCustomers(customer1, "Sparkonto", "Lönekonto", "Anas",579.56f);

            //Assert
            Assert.AreNotEqual(customer2,customer3);
        }
        [TestMethod]
        public void TransferbetweenCustomer_NameTest()
        {
            //Arrange
            var customer1Dict = new Dictionary<string, List<string>>() {
                { "Sparkonto", new List<string>() { 1000.0f.ToString(), "kr", "Personkonto" } },
                { "Lönekonto", new List<string>() { 2000.0f.ToString(), "$", "Personkonto" } },
            };
            Customer customer1 = new Customer("Tobias", "111", customer1Dict);

            var customer2Dict = new Dictionary<string, List<string>>() {
                { "Sparkonto", new List<string>() { 1000.0f.ToString(), "kr", "Personkonto" } },
                { "Lönekonto", new List<string>() { 2000.0f.ToString(), "$", "Personkonto" } },
            };
            Customer customer2 = new Customer("Anas", "222", customer2Dict);

            Users.customerList.Add(customer1);
            Users.customerList.Add(customer2);

            string choice = "Sparkonto";
            string choice2 = "Lönekonto";
            string accountName = "Anas";
            float amount = 1060.1f;

            //Act
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            BankSystem.TransferBetweenCustomers(customer1, choice, choice2,accountName, amount);
            var output = consoleOutput.ToString();

            //Assert
            Assert.IsTrue(output.Contains($"\nTransfered {amount}{customer1.accounts[choice][1]} from {customer1.Name} to {customer2.Name}"));
        }
        [TestMethod]
        public void CustomerCreation()
        {
            //Arrange
            string name = "Emil";
            string password = "hsaufhaas";
            //Act
            BankSystem.CustomerCreation(name, password);

            //Assert
            Assert.IsFalse(Users.customerList == null);
        }
    }
}