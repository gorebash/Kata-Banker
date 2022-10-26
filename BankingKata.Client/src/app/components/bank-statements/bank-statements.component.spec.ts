import { HttpClientModule } from '@angular/common/http';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { Transaction } from 'src/app/models/transaction';
import { BankingService } from 'src/app/services/banking.service';

import { BankStatementsComponent } from './bank-statements.component';

describe('BankStatementsComponent', () => {
  let component: BankStatementsComponent;
  let fixture: ComponentFixture<BankStatementsComponent>;
  let mockBankingService:any;
  const DATA:Array<Transaction> = [];


  beforeEach(async () => {
    mockBankingService = jasmine.createSpyObj(["deposit", "withdraw", "getStatement"]);
    mockBankingService.getStatement.and.returnValue(of(DATA));
    mockBankingService.withdraw.and.returnValue(of());
    mockBankingService.deposit.and.returnValue(of());

    await TestBed.configureTestingModule({
      declarations: [ BankStatementsComponent ],
      imports: [HttpClientModule],
      providers: [
        { provide:BankingService, useValue:mockBankingService },
      ],
    })
    .compileComponents();

    fixture = TestBed.createComponent(BankStatementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('depositing funds', () => {
    it('should call deposit service with provided amount', () => {
      component.amount = 5;
      component.deposit();

      expect(mockBankingService.deposit).toHaveBeenCalledWith(5);
    });

    it('should refresh statements after deposits are made', () => {
      component.amount = 5;
      component.deposit();

      expect(mockBankingService.getStatement).toHaveBeenCalled();
    });
  });
});
