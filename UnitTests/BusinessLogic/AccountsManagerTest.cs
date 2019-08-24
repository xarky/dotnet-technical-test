using Moq;
using POC.BusinessLogic.Managers;
using POC.Common;
using System;
using Xunit;

namespace UnitTests.BusinessLogic
{
    public class AccountsManagerTest
    {
        #region Fields

        private readonly Mock<IRepository> repository;

        #endregion

        #region Constructors

        public AccountsManagerTest()
        {
            repository = new Mock<IRepository>();
        }

        #endregion

        #region Methods

        [Theory]
        [InlineData(1, 100d)]
        [InlineData(2, 0d)]
        [InlineData(3, 50.2d)]
        [InlineData(4, 45.1d)]
        public void GetAvailableFunds_Success(Int32 customerId, Decimal funds)
        {
            repository
                .Setup(a => a.GetAvailableFunds(customerId))
                .Returns(funds);

            var accountsManager = new AccountsManager(repository.Object);

            var availableFunds = accountsManager.GetAvailableFunds(customerId);

            Assert.Equal(funds, availableFunds);
        }

        #endregion
    }
}
