﻿namespace Repository
{
    using System;
    using System.Collections.Generic;
    using DataTransferObjects;

    public interface IRepository
    {
        #region Methods

        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteCustomer(Int32 id);

        /// <summary>
        /// Deposits the funds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="funds">The funds.</param>
        void DepositFunds(Int32 customerId,
                          Decimal funds);

        /// <summary>
        /// Gets the available funds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        Decimal GetAvailableFunds(Int32 customerId);

        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Customer GetCustomer(Int32 id);

        /// <summary>
        /// Gets all customers.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Customer> GetCustomers();

        /// <summary>
        /// Saves the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        void SaveCustomer(Customer customer);

        /// <summary>
        /// Withdraws the funds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="funds">The funds.</param>
        void WithdrawFunds(Int32 customerId,
                           Decimal funds);

        /// <summary>
        /// Transfers funds between two customers
        /// </summary>
        /// <param name="transferDetails">Details about the transfer</param>
        void TransferFunds(TransferDetails transferDetails);

        #endregion
    }
}