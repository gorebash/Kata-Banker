namespace BankingKata.Models;

public class AccountTransaction
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public int Balance { get; set; }
    public DateTime Date { get; set; }

    public AccountTransaction()
    {
    }

    public AccountTransaction(int amount, int balance, DateTime date)
    {
        Amount = amount;
        Balance = balance;
        Date = date;
    }
}