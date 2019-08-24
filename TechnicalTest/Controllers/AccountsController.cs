namespace TechnicalTest.Controllers
{
    using BusinessLogic.Interfaces;
    using DataTransferObjects;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        #region Fields

        /// <summary>
        /// The accounts manager
        /// </summary>
        private readonly IAccountsManager AccountsManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountsController" /> class.
        /// </summary>
        /// <param name="accountsManager">The accounts manager.</param>
        public AccountsController(IAccountsManager accountsManager)
        {
            this.AccountsManager = accountsManager;
        }

        #endregion

        #region Methods

        [HttpGet("{customerId}")]
        public ActionResult<decimal> Get(Int32 customerId)
        {
            var availableFunds = this.AccountsManager.GetAvailableFunds(customerId);
            return this.Ok(availableFunds);
        }

        [HttpPost("{customerId}/deposit")]
        public void DepositFunds(Int32 customerId, [FromBody] DepositDetails depositDetails)
        {
            this.AccountsManager.DepositFunds(customerId, depositDetails.Funds);
        }

        [HttpPost("{id}/withdraw")]
        public void WithdrawFunds(Int32 customerId, [FromBody] WithrdawalDetails withrdawalDetails)
        {
            this.AccountsManager.WithdrawFunds(customerId, withrdawalDetails.Funds);
        }

        [HttpPost("{id}/transfer")]
        public void TransferFunds([FromBody] TransferDetails transferDetails)
        {
            this.AccountsManager.TransferFunds(transferDetails);
        }

        #endregion
    }
}