using BankingKata.Core;

namespace BankingKata.Tests;

public class BankTests
{
    private Bank _bank;
    private DateTime _date = DateTime.Now;


    [SetUp]
    public void Setup()
    {
        _bank = new Bank(new InMemoryBankDataService());
    }

    [Test]
    public void LedgerStartsAtZero()
    {
        var expected = $"{ExpectedDate}, 0, 0";

        Assert.That(_bank.PrintStatement(), Is.EqualTo(expected));
    }

    [Test]
    public void DepositAddsToLedger()
    {
        _bank.Deposit(1);

        var expected =
            $"{ExpectedDate}, 0, 0\n" + 
            $"{ExpectedDate}, 1, 1";

        Assert.That(_bank.PrintStatement(), Is.EqualTo(expected));
    }

    [Test]
    public void DepositsTallyRunningTotal ()
    {
        _bank.Deposit(1);
        _bank.Deposit(1);
        _bank.Deposit(1);

        var expected = 
            $"{ExpectedDate}, 0, 0\n" +
            $"{ExpectedDate}, 1, 1\n" +
            $"{ExpectedDate}, 1, 2\n" +
            $"{ExpectedDate}, 1, 3";

        Assert.That(_bank.PrintStatement(), Is.EqualTo(expected));
    }

    [Test]
    public void OverdrawnGoesNegative ()
    {
        _bank.Withdraw(1);

        var expected =
            $"{ExpectedDate}, 0, 0\n" + 
            $"{ExpectedDate}, -1, -1";
        Assert.That(_bank.PrintStatement(), Is.EqualTo(expected));
    }

    [Test]
    public void WidthdrawalSubtractsFromLedger ()
    {
        _bank.Deposit(2);
        _bank.Deposit(2);
        _bank.Withdraw(1);

        var expected =
            $"{ExpectedDate}, 0, 0\n" +
            $"{ExpectedDate}, 2, 2\n" +
            $"{ExpectedDate}, 2, 4\n" +
            $"{ExpectedDate}, -1, 3";

        Assert.That(_bank.PrintStatement(), Is.EqualTo(expected));
    }

    [Test]
    public void StatementContainsAllLedgerItems ()
    {
        _bank.Deposit(2);
        _bank.Deposit(2);
        _bank.Withdraw(1);

        var expected =
            $"{ExpectedDate}, 0, 0\n" +
            $"{ExpectedDate}, 2, 2\n" +
            $"{ExpectedDate}, 2, 4\n" +
            $"{ExpectedDate}, -1, 3";

        Assert.That(_bank.PrintStatement(), Is.EqualTo(expected));
    }


    private string ExpectedDate => $"{_date:dd-MM-yyyy}";

}