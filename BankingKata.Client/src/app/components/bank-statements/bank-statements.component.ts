import { Component, OnInit } from '@angular/core';
import { Transaction } from 'src/app/models/transaction';
import { BankingService } from 'src/app/services/banking.service';

@Component({
  selector: 'app-bank-statements',
  templateUrl: './bank-statements.component.html',
  styleUrls: ['./bank-statements.component.scss']
})
export class BankStatementsComponent implements OnInit {

  transactions:Array<Transaction> = [];
  amount:number = 0;

  constructor(private bankingService:BankingService) { }

  ngOnInit(): void {
    this.refreshStatement();
  }

  deposit ():void {
    this.bankingService.deposit(this.amount).subscribe(() => this.refreshStatement());
  }

  withdraw ():void {
    this.bankingService.withdraw(this.amount).subscribe(() => this.refreshStatement());
  }

  private refreshStatement () {
    this.bankingService.getStatement()
      .subscribe((result) => this.transactions = result);
  }

}
