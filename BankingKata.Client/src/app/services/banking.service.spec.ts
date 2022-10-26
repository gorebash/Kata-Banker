import { HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';

import { BankingService } from './banking.service';

describe('BankingService', () => {
  let service: BankingService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientModule,
      ],
    });
    service = TestBed.inject(BankingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
