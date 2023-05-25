using BankApplication;

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
    }
}