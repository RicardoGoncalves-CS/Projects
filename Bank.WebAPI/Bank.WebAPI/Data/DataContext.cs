using Bank.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank.WebAPI.Data;

public class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Address> Addresses { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Account> Accounts { get; set; }
    //public DbSet<Transaction> Transactions { get; set; }
}
