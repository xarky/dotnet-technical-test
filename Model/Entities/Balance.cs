using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    public class Balance
    {
        [Key]
        public Guid Id { get; set; }
        public Int32 CustomerId { get; set; }

        public Decimal Funds { get; set; }
    }
}
