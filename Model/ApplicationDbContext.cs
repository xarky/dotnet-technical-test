using Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Balance> Balances { get; set; }
        public DbSet<TransactionAudit> TransactionAudits { get; set; }
    }
}
