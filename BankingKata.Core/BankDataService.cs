using BankingKata.Data;
using BankingKata.Models;

namespace BankingKata.Core;


public interface IBankDataService
{
    List<AccountTransaction> GetTransactions();
    void Save(AccountTransaction tran);
}



public class EfBankDataService : IBankDataService
{
	private readonly BankDbContext _db;

	public EfBankDataService(BankDbContext db)
	{
		_db = db;
	}

    public void Save(AccountTransaction tran)
    {
        _db.Add(tran);
        _db.SaveChanges();
    }

    public List<AccountTransaction> GetTransactions()
    {
        var transactions = _db.Transactions;
        if (transactions?.Count() < 1)
            Save(new AccountTransaction
            {
                Amount = 0,
                Balance = 0,
                Date = DateTime.Now
            });

        return _db.Transactions.ToList();
    }
	
}



public class InMemoryBankDataService : IBankDataService
{
    private readonly List<AccountTransaction> _ledger;

	public InMemoryBankDataService()
	{
        _ledger = new List<AccountTransaction>
        {
            new AccountTransaction(0, 0, DateTime.Now)
        };
    }

    public void Save(AccountTransaction tran)
	{
        _ledger.Add(tran);
    }

    public List<AccountTransaction> GetTransactions() => 
        _ledger;
}
