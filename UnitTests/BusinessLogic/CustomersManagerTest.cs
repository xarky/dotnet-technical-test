using FizzWare.NBuilder;
using Moq;
using POC.BusinessLogic.Managers;
using POC.Common;
using POC.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using Newtonsoft.Json;

namespace UnitTests.BusinessLogic
{
    public class CustomersManagerTest
    {
        #region Fields

        private readonly Mock<IRepository> repository;

        #endregion

        #region Constructors

        public CustomersManagerTest()
        {
            repository = new Mock<IRepository>();
        }

        #endregion

        #region Methods

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(100)]
        [InlineData(1000)]
        public void GetCustomers_CustomerExist(Int32 customerCount)
        {
            var testData = Builder<Customer>.CreateListOfSize(customerCount).Build();

            repository
                .Setup(a => a.GetCustomers())
                .Returns(testData);

            var customersManager = new CustomersManager(repository.Object);

            var result = customersManager.GetCustomers();

            Assert.Equal(customerCount, result.Count());
        }

        [Fact]
        public void GetCustomers_NoData()
        {
            repository
                  .Setup(a => a.GetCustomers())
                  .Returns(new List<Customer>());

            var customersManager = new CustomersManager(repository.Object);

            var result = customersManager.GetCustomers();

            Assert.Empty(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void GetCustomer_Success(Int32 customerId)
        {
            var testData = Builder<Customer>.CreateNew().Build();

            repository
               .Setup(a => a.GetCustomer(customerId))
               .Returns(testData);

            var customersManager = new CustomersManager(repository.Object);

            var result = customersManager.GetCustomer(customerId);

            var obj1Str = JsonConvert.SerializeObject(testData);
            var obj2Str = JsonConvert.SerializeObject(result);

            Assert.Equal(obj1Str, obj1Str);
        }

        [Fact]
        public void GetCustomer_NotExisting()
        {
            repository
               .Setup(a => a.GetCustomer(-1))
               .Returns((Customer)null);

            var customersManager = new CustomersManager(repository.Object);

            var result = customersManager.GetCustomer(-1);
            
            Assert.Null(result);
        }

        #endregion
    }
}
