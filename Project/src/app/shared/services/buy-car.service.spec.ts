import { TestBed } from '@angular/core/testing';

import { BuyCarService } from './buy-car.service';

describe('BuyCarService', () => {
  let service: BuyCarService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BuyCarService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
