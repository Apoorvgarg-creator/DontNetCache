using ALEHA_API.Models;
using ALEHA_API.Repository;
using ALEHA_API.Services;
using Moq; // You will need to install the Moq NuGet package
using NUnit.Framework;

namespace ALEHA_API.Tests
{
    [TestFixture]
    public class AccountServiceTests
    {
        private AccountService _accountService;
        private Mock<IAccountDataProvider<Account>> _mockRepo;

        [SetUp]
        public void Setup()
        {
            // Create a mock repository for testing
            _mockRepo = new Mock<IAccountDataProvider<Account>>();
            _accountService = new AccountService(_mockRepo.Object);
        }

        [Test]
        public void AddAccountDetails_ValidAccount_ReturnsAccountNumber()
        {
            // Arrange
            var account = new Account { /* Initialize with valid account data */ };
            _mockRepo.Setup(repo => repo.AddAccountDetails(It.IsAny<Account>())).Returns(123); // Simulate a successful addition

            // Act
            string result = _accountService.AddAccountDetails(account);

            // Assert
            Assert.AreEqual("123", result); // Check if it returns the account number as a string
        }

        [Test]
        public void AddAccountDetails_FailedToAddAccount_ReturnsErrorMessage()
        {
            // Arrange
            var account = new Account { /* Initialize with valid account data */ };
            _mockRepo.Setup(repo => repo.AddAccountDetails(It.IsAny<Account>())).Returns(0); // Simulate failure to add

            // Act
            string result = _accountService.AddAccountDetails(account);

            // Assert
            Assert.AreEqual("Failed to add Account", result);
        }

        [Test]
        public void GetAccountBalance_ValidAccountNumber_ReturnsBalanceResponseModel()
        {
            // Arrange
            int accountNumber = 123; // Replace with a valid account number
            var balanceResponse = new BalanceResponseModel { /* Initialize with valid balance data */ };
            _mockRepo.Setup(repo => repo.GetBalance(accountNumber)).Returns(balanceResponse);

            // Act
            var result = _accountService.GetAccountBalance(accountNumber);

            // Assert
            Assert.AreEqual(balanceResponse, result);
        }

        // Add similar tests for other methods like PinValidation, PinChange, and DeleteAccount.
    }
}
