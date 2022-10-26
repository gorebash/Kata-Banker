import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Transaction } from '../models/transaction';

@Injectable({
  providedIn: 'root'
})
export class BankingService {

  constructor(private client:HttpClient) {}

  getStatement ():Observable<Array<Transaction>> {
    return this.client.get<Array<Transaction>>("https://localhost:7243/bank/statement");
  }

  deposit (amount:number):Observable<any> {
    return this.client.post(`https://localhost:7243/bank/deposit?amount=${amount}`, { });
  }

  withdraw (amount:number):Observable<any> {
    return this.client.post(`https://localhost:7243/bank/withdraw?amount=${amount}`, { });
  }

}
