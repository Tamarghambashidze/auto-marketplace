import { TestBed } from '@angular/core/testing';

import { CarDetailsService } from './car-details.service';

describe('CarDetailsServiceService', () => {
  let service: CarDetailsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CarDetailsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
