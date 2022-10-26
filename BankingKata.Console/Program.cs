using BankingKata.Core;




var bank = new Bank(new InMemoryBankDataService());
bank.Deposit(1);

Console.WriteLine(bank.PrintStatement());