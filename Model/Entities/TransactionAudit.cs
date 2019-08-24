using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    public enum TransactionType
    {
        Deposit = 0,
        Withdrawal,
        Transfer
    }

    public class TransactionAudit
    {
        [Key]
        public Guid Id { get; set; }

        public TransactionType Type { get; set; }

        public Decimal Funds { get; set; }

        public Int32 CustomerId { get; set; }

        public Int32? FromCustomerId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
