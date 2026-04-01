using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAcess
{
    public class CashFlowDbContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "";

            var version = new Version(); 
            var serverVersion = new MySqlServerVersion(version);

            optionsBuilder.UseMySql(connectionString, serverVersion);
        }

    }
}
