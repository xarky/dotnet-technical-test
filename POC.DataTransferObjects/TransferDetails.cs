using System;
using System.Collections.Generic;
using System.Text;

namespace POC.DataTransferObjects
{
    public class TransferDetails
    {
        /// <summary>
        /// The customer sending the transfer
        /// </summary>
        public Int32 From { get; set; }

        /// <summary>
        /// The customer receiving the transfer
        /// </summary>
        public Int32 To { get; set; }


        /// <summary>
        /// The fund value to be transferred between the two accounts
        /// </summary>
        public decimal Funds { get; set; }
    }
}
