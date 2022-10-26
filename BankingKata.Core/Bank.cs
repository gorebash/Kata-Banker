using BankingKata.Data;
using BankingKata.Models;

namespace BankingKata.Core;

public class Bank
{
    private readonly IBankDataService _bankDb;

    public Bank(IBankDataService bankDb)
    {
        _bankDb = bankDb;
    }

    public void Deposit(int amount)
    {
        var bal = GetBalance();
        var newbal = bal + amount;
        var tran = new AccountTransaction(amount, newbal, DateTime.Now);

        _bankDb.Save(tran);
    }

    public void Withdraw(int amount)
    {
        var bal = GetBalance();
        var newbal = bal - amount;
        var tran = new AccountTransaction(-amount, newbal, DateTime.Now);

        _bankDb.Save(tran);
    }


    public string PrintStatement()
    {
        var ledger = _bankDb.GetTransactions();
        return string.Join("\n",
            ledger.Select(l =>
                $"{l.Date:dd-MM-yyyy}, {l.Amount}, {l.Balance}"));
    }

    public List<AccountTransaction> GetLedger() =>
        _bankDb.GetTransactions() ?? new List<AccountTransaction>();
    

    private int GetBalance() => _bankDb.GetTransactions().Last()?.Balance ?? 0;
}