﻿namespace POC.BusinessLogic.Interfaces
{
    using System;
    using System.Collections.Generic;
    using DataTransferObjects;

    public interface ICustomersManager
    {
        #region Methods

        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteCustomer(Int32 id);

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
        /// Adds the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        void SaveCustomer(Customer customer);

        #endregion
    }
}