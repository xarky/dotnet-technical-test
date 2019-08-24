namespace POC.BusinessLogic.Managers
{
    using BusinessLogic.Interfaces;
    using DataTransferObjects;
    using POC.Common;
    using System;

    public class AccountsManager : IAccountsManager
    {
        #region Fields

        /// <summary>
        /// The repository
        /// </summary>
        private readonly IRepository Repository;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountsManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public AccountsManager(IRepository repository)
        {
            this.Repository = repository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the available funds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public Decimal GetAvailableFunds(Int32 customerId)
        {
            return Repository.GetAvailableFunds(customerId);
        }

        /// <summary>
        /// Deposits the funds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="funds">The funds.</param>
        public void DepositFunds(Int32 customerId, Decimal funds)
        {
            Repository.DepositFunds(customerId, funds);
        }
        
        /// <summary>
        /// Withdraws the funds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="funds">The funds.</param>
        public void WithdrawFunds(Int32 customerId, Decimal funds)
        {
            Repository.WithdrawFunds(customerId, funds);
        }

        /// <summary>
        /// Transfers funds between two customers
        /// </summary>
        /// <param name="transferDetails">Details about the transfer</param>
        public void TransferFunds(TransferDetails transferDetails)
        {
            Repository.TransferFunds(transferDetails);
        }

        #endregion
    }
}