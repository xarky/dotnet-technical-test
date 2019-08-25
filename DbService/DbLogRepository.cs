using Model;
using POC.Common;
using System;

namespace DbService
{
    public class DbLogRepository : ILogRepository
    {
        #region Fields

        private readonly ApplicationDbContext dbContext;

        #endregion

        #region Constructors

        public DbLogRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #endregion

        #region Methods

        public void LogDeposit(int customerId, decimal funds)
        {
            dbContext.TransactionAudits.Add(new Model.Entities.TransactionAudit
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                Timestamp = DateTime.Now,
                Type = Model.Entities.TransactionType.Deposit,
                Funds = funds
            });

            dbContext.SaveChanges();
        }

        public void LogTransfer(int sourceCustomerId, int destinationCustomerId, decimal funds)
        {
            dbContext.TransactionAudits.Add(new Model.Entities.TransactionAudit
            {
                Id = Guid.NewGuid(),
                CustomerId = destinationCustomerId,
                Timestamp = DateTime.Now,
                Type = Model.Entities.TransactionType.Transfer,
                Funds = funds,
                FromCustomerId = sourceCustomerId
            });

            dbContext.SaveChanges();
        }

        public void LogWithdrawal(int customerId, decimal funds)
        {
            dbContext.TransactionAudits.Add(new Model.Entities.TransactionAudit
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                Timestamp = DateTime.Now,
                Type = Model.Entities.TransactionType.Withdrawal,
                Funds = funds
            });

            dbContext.SaveChanges();
        }

        #endregion
    }
}
