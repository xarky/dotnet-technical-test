using Model;
using POC.Common;
using POC.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;

namespace DbService
{
    public class DbRepository : IRepository
    {
        #region Private Fields

        private readonly ApplicationDbContext dbContext;
        private readonly ILogRepository logRepository;

        #endregion

        #region Constructors

        public DbRepository(ApplicationDbContext dbContext, ILogRepository logRepository)
        {
            this.dbContext = dbContext;
            this.logRepository = logRepository;
        }

        #endregion

        #region Methods

        public void DeleteCustomer(int id)
        {
            dbContext.Customers.Remove(dbContext.Customers.Single(c => c.Id == id));
            dbContext.SaveChanges();
        }

        public void DepositFunds(int customerId, decimal funds)
        {
            var customerBalance = dbContext.Balances.SingleOrDefault(b => b.CustomerId == customerId);

            if (customerBalance == null)
            {
                dbContext.Balances.Add(new Model.Entities.Balance
                {
                    CustomerId = customerId,
                    Funds = funds
                });
            }
            else
            {
                customerBalance.Funds += funds;
            }

            dbContext.SaveChanges();

            logRepository.LogDeposit(customerId, funds);
        }

        public decimal GetAvailableFunds(int customerId)
        {
            var customerBalance = dbContext.Balances.SingleOrDefault(b => b.CustomerId == customerId);

            return customerBalance != null ? customerBalance.Funds : 0;
        }

        public Customer GetCustomer(int id)
        {
            var dbCustomer = dbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (dbCustomer != null)
                return new Customer
                {
                    Id = dbCustomer.Id,
                    IdCard = dbCustomer.IdCard,
                    Name = dbCustomer.Name,
                    Surname = dbCustomer.Surname
                };
            else
            {
                return null;
            }
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return dbContext.Customers.Select(
                c => new Customer
                {
                    Id = c.Id,
                    IdCard = c.IdCard,
                    Name = c.Name,
                    Surname = c.Surname
                });
        }

        public void SaveCustomer(Customer customer)
        {
            dbContext.Customers.Add(new Model.Entities.Customer
            {
                //Id = customer.Id,
                IdCard = customer.IdCard,
                Name = customer.Name,
                Surname = customer.Surname
            });
            
            dbContext.SaveChanges();
        }

        public void TransferFunds(TransferDetails transferDetails)
        {
            var sourceAccount = dbContext.Balances.SingleOrDefault(b => b.CustomerId == transferDetails.From);
            var destinationAccount = dbContext.Balances.SingleOrDefault(b => b.CustomerId == transferDetails.To);

            if (sourceAccount != null && destinationAccount != null)
            {
                var transferableFunds = sourceAccount.Funds > transferDetails.Funds ? transferDetails.Funds : sourceAccount.Funds;
                sourceAccount.Funds -= transferableFunds;
                destinationAccount.Funds += transferableFunds;
                
                dbContext.SaveChanges();

                logRepository.LogTransfer(transferDetails.From, transferDetails.To, transferableFunds);
            }
        }

        public void WithdrawFunds(int customerId, decimal funds)
        {
            var sourceAccount = dbContext.Balances.SingleOrDefault(b => b.CustomerId == customerId);

            if (sourceAccount != null)
            {
                var withrdawableFunds = sourceAccount.Funds > funds ? funds : sourceAccount.Funds;

                sourceAccount.Funds -= withrdawableFunds;
                
                dbContext.SaveChanges();

                logRepository.LogWithdrawal(customerId, withrdawableFunds);
            }
        }

        #endregion
    }
}
