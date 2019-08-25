using Microsoft.EntityFrameworkCore;
using Model;
using Moq;
using POC.Common;
using POC.DbService;
using System;
using Xunit;

namespace UnitTests.Service
{
    public class RepositoryTests
    {
        #region Fields

        private readonly Mock<ILogRepository> logRepository;
        private readonly IRepository repository;

        #endregion

        #region Constructors

        public RepositoryTests()
        {
            logRepository = new Mock<ILogRepository>();
            repository = ConfigureRepository();
        }

        #endregion
               
        #region Private Methods

        private IRepository ConfigureRepository()
        {
            // test using our own InMemory implementation, OR 
            // bypass the DbContext using an out-of-the-box InMemoryDatabase implementation

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase();
            var context = new ApplicationDbContext(optionsBuilder.Options);
            return new DbRepository(context, logRepository.Object);

            //return new POC.InMemoryService.InMemoryRepository();
        }

        #endregion

        #region Public Methods

        [Theory]
        [InlineData(1, 100)]
        [InlineData(2, 20)]
        [InlineData(3, 5)]
        public void DepositFunds_EmptyAccount(Int32 customerId, Decimal depositableFunds)
        {
            repository.DepositFunds(customerId, depositableFunds);

            var result = repository.GetAvailableFunds(customerId);

            Assert.Equal(depositableFunds, result);
        }

        [Theory]
        [InlineData(4, 10, 100)]
        [InlineData(5, 10, 20)]
        [InlineData(6, 10, 5)]
        public void DepositFunds_ExistingAccountWithFunds(Int32 customerId, Decimal currentBalance, Decimal depositableFunds)
        {
            repository.DepositFunds(customerId, currentBalance);
            repository.DepositFunds(customerId, depositableFunds);

            var result = repository.GetAvailableFunds(customerId);

            Assert.Equal(currentBalance + depositableFunds, result);
        }


        [Theory]
        [InlineData(7, 8, 100, 0, 50)]
        [InlineData(9, 10, 10, 10, 10)]
        public void TransferFunds_AvailableFunds(
            Int32 sourceCustomerId,
            Int32 destinationCustomerId,
            Decimal sourceInitialBalance,
            Decimal destinationInitialBalance,
            Decimal fundsToBeTransfered)
        {
            repository.DepositFunds(sourceCustomerId, sourceInitialBalance);
            repository.DepositFunds(destinationCustomerId, destinationInitialBalance);

            var transferDetails = new POC.DataTransferObjects.TransferDetails
            {
                From = sourceCustomerId,
                To = destinationCustomerId,
                Funds = fundsToBeTransfered
            };

            repository.TransferFunds(transferDetails);

            var resultSource = repository.GetAvailableFunds(sourceCustomerId);
            var resultDestination = repository.GetAvailableFunds(destinationCustomerId);

            Assert.Equal(sourceInitialBalance - fundsToBeTransfered, resultSource);
            Assert.Equal(destinationInitialBalance + fundsToBeTransfered, resultDestination);
        }

        [Theory]
        [InlineData(11, 12, 100, 0, 101)]
        [InlineData(13, 14, 10, 100, 10)]
        public void TransferFunds_NotEnoughFunds(
            Int32 sourceCustomerId,
            Int32 destinationCustomerId,
            Decimal sourceInitialBalance,
            Decimal destinationInitialBalance,
            Decimal fundsToBeTransfered)
        {
            repository.DepositFunds(sourceCustomerId, sourceInitialBalance);
            repository.DepositFunds(destinationCustomerId, destinationInitialBalance);

            var transferDetails = new POC.DataTransferObjects.TransferDetails
            {
                From = sourceCustomerId,
                To = destinationCustomerId,
                Funds = fundsToBeTransfered
            };

            repository.TransferFunds(transferDetails);

            var resultSource = repository.GetAvailableFunds(sourceCustomerId);
            var resultDestination = repository.GetAvailableFunds(destinationCustomerId);

            Assert.Equal(0, resultSource);
            Assert.Equal(destinationInitialBalance + sourceInitialBalance, resultDestination);
        }

        [Theory]
        [InlineData(15, 100, 100)]
        [InlineData(16, 100, 20)]
        [InlineData(17, 100, 5)]
        public void WithdrawFunds_ExistingAccountWithEnoughFunds(Int32 customerId, Decimal currentBalance, Decimal withdrawableFunds)
        {
            repository.DepositFunds(customerId, currentBalance);
            repository.WithdrawFunds(customerId, withdrawableFunds);

            var result = repository.GetAvailableFunds(customerId);

            Assert.Equal(currentBalance - withdrawableFunds, result);
        }
        
        [Theory]
        [InlineData(18, 100, 101)]
        [InlineData(19, 100, 150)]
        [InlineData(20, 100, 200)]
        public void WithdrawFunds_ExistingAccountWithNotEnoughFunds(Int32 customerId, Decimal currentBalance, Decimal withdrawableFunds)
        {
            repository.DepositFunds(customerId, currentBalance);
            repository.WithdrawFunds(customerId, withdrawableFunds);

            var result = repository.GetAvailableFunds(customerId);

            Assert.Equal(0, result);
        }

        #endregion
    }
}
