using BankingKata.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingKata.Data;

public class BankDbContext : DbContext
{
    public DbSet<AccountTransaction> Transactions { get; set; }

    public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
    {
    }
}
