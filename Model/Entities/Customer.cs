using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    public class Customer
    {
        [Key]
        public Int32 Id { get; set; }

        public String IdCard { get; set; }

        public String Name { get; set; }

        public String Surname { get; set; }

        public virtual Balance Balance { get; set; }
    }
}
