namespace POC.BusinessLogic.Interfaces
{
    using DataTransferObjects;
    using System;

    public interface IAccountsManager
    {
        /// <summary>
        /// Gets the available funds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        Decimal GetAvailableFunds(Int32 customerId);

        /// <summary>
        /// Deposits the funds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="funds">The funds.</param>
        void DepositFunds(Int32 customerId, Decimal funds);


        /// <summary>
        /// Withdraws the funds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="funds">The funds.</param>
        void WithdrawFunds(Int32 customerId, Decimal funds);

        /// <summary>
        /// Transfers funds between two customers
        /// </summary>
        /// <param name="transferDetails">Details about the transfer</param>
        void TransferFunds(TransferDetails transferDetails);
    }
}
